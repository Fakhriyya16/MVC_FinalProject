using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Settings;

namespace MVC_FinalProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingService.GetAllSettings();

            List<SettingVM> model = settings.Select(x => new SettingVM
            {
                Key = x.Key,
                Value = x.Value,
            }).ToList();

            return await Task.FromResult(View(model));
        }
    }
}
