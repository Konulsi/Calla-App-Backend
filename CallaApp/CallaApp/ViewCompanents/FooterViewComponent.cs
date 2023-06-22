using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
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
            FooterVM model = new FooterVM()
            {
                Settings = _layoutService.GetSettingsData(),
                Socials = await _webSiteSocialService.GetAllAsync(),
                MiniImages = await _miniImageService.GetAllAsync(),
            };
            return await Task.FromResult(View(model));
        }
    }
}
