using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.ViewCompanents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IWebSiteSocialService _webSiteSocialService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;
        public HeaderViewComponent(ILayoutService layoutService,
                                    IWebSiteSocialService webSiteSocialService,
                                    ICartService cartService,
                                    IWishlistService wishlistService)
        {
            _layoutService = layoutService;
            _webSiteSocialService = webSiteSocialService;
            _cartService = cartService;
            _wishlistService = wishlistService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                Settings = _layoutService.GetSettingsData(),
                Socials  = await _webSiteSocialService.GetAllAsync(),
                BasketCount = _cartService.GetDatasFromCookie().Count,
                WishlistCount = _wishlistService.GetDatasFromCookie().Count,
            };
            return await Task.FromResult(View(model));
        }
    }
}
