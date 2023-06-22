using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.ViewCompanents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IWebSiteSocialService _webSiteSocialService;
        public HeaderViewComponent(ILayoutService layoutService,
                                    IWebSiteSocialService webSiteSocialService)
        {
            _layoutService = layoutService;
            _webSiteSocialService = webSiteSocialService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                Settings = _layoutService.GetSettingsData(),
                Socials  = await _webSiteSocialService.GetAllAsync(),
            };
            return await Task.FromResult(View(model));
        }
    }
}
