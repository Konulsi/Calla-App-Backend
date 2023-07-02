using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = true;

    option.User.RequireUniqueEmail = true;

    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    option.Lockout.AllowedForNewUsers = true;

});

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IDecorService, DecorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IMiniImageService, MiniImageService>();
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();
builder.Services.AddScoped<IWebSiteSocialService, WebSiteSocialService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICartService, CartService>();



builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddScoped<EmailSetting>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
