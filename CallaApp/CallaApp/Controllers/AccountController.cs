using CallaApp.Data;
using CallaApp.Helpers.Enums;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using CallaApp.ViewModels.Account;
using CallaApp.ViewModels.Cart;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CallaApp.Controllers
{
    //SuperAdmin - Cavid_Bashirov  email: konulsi@code.edu.az / parol: Cavid123_
    //Admin - Konul_Ibrahimova  email: konul.osmanli96@gmail.com / parol: Cavid123_
    //Member - Alisa_Ibrahim  email: ibadovac734@gmail.com / parol: Cavid123_
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IEmailService emailService,
                                 RoleManager<IdentityRole> roleManager,
                                 ICartService cartService,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _cartService = cartService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser newUser = new()
                {
                    UserName = string.Join("_", model.FirstName, model.LastName),
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    TempData["errors"] = model.ErrorMessages;
                    return RedirectToAction("Index", model);
                }

                await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

                string subject = "Register confirmation";

                string html = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
                {
                    html = reader.ReadToEnd();
                }

                html = html.Replace("{{link}}", link);
                html = html.Replace("{{headerText}}", "Welcome");

                _emailService.Send(newUser.Email, subject, html);

                return RedirectToAction(nameof(VerifyEmail));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index", model);
            }
        }



        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            List<CartVM> cartVMs = new();
            Cart dbCart = await _cartService.GetByUserIdAsync(userId);

            if (dbCart is not null)
            {
                List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                foreach (var cartProduct in cartProducts)
                {
                    cartVMs.Add(new CartVM
                    {
                        ProductId = cartProduct.ProductId,
                        Count = cartProduct.Count
                    });
                }

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Login", model);

                var user = await _userManager.FindByEmailAsync(model.EmailOrUsername);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    RedirectToAction("Index", model);
                }

                var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

                if (!res.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }

                List<CartVM> cartVMs = new();

                Cart dbCart = await _cartService.GetByUserIdAsync(user.Id);

                if (dbCart is not null)
                {
                    List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);

                    foreach (var cartProduct in cartProducts)
                    {
                        cartVMs.Add(new CartVM
                        {
                            ProductId = cartProduct.ProductId,
                            Count = cartProduct.Count
                        });
                    }

                    Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
                }


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<CartVM> carts = _cartService.GetDatasFromCookie();

            if (carts.Count != 0)
            {
                Cart dbCart = await _cartService.GetByUserIdAsync(userId);
                if (dbCart == null)
                {
                    dbCart = new()
                    {
                        AppUserId = userId,
                        CartProducts = new List<CartProduct>()
                    };
                    foreach (var cart in carts)
                    {
                        dbCart.CartProducts.Add(new CartProduct()
                        {
                            ProductId = cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    await _context.Carts.AddAsync(dbCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<CartProduct> cartProducts = new List<CartProduct>();
                    foreach (var cart in carts)
                    {
                       // var product =  _context.Products.FirstOrDefaultAsync(p => p.Id == cart.ProductId);
                       //var basket = _context.Carts.FirstOrDefaultAsync(p => p.Id ==  dbCart.Id);
                        
                        cartProducts.Add(new CartProduct()
                        {
                            //Product = product.Result,
                            //Cart = basket.Result,
                            ProductId= cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    dbCart.CartProducts = cartProducts;
                    //await _context.Carts.AddAsync(dbCart);
                    await _context.SaveChangesAsync();

                }
                Response.Cookies.Delete("basket");
            }

            return RedirectToAction("Index", "Home");
        }


        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}




        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());


            string subject = "Verify password reset email";

            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Welcome");

            _emailService.Send(existUser.Email, subject, html);
            return RedirectToAction(nameof(VerifyEmail));
        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return NotFound();
            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));
        }

    }
}
