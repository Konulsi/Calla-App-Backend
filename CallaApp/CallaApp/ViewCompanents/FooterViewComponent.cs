using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.ViewCompanents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IWebSiteSocialService _webSiteSocialService;
        private readonly IMiniImageService _miniImageService;

        public FooterViewComponent(ILayoutService layoutService,
                                    IWebSiteSocialService webSiteSocialService,
                                    IMiniImageService miniImageService)
        {
            _layoutService = layoutService;
            _webSiteSocialService = webSiteSocialService;
            _miniImageService = miniImageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                Settings = _layoutService.GetSettingsData(),
                Socials = await _webSiteSocialService.GetAllAsync(),
                MiniImages = await _miniImageService.GetAllAsync(),
            };
            return await Task.FromResult(View(model));
        }
    }
}
