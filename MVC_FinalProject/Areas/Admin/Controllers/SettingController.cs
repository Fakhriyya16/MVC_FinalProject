using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Settings;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _settingService.GetAllSettings());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "_404");
            }

            var setting = await _settingService.GetById((int)id);

            if(setting == null)
            {
                return RedirectToAction("Index", "_404");
            }
            SettingEditVM vm = new()
            {
                Key = setting.Key,
                Value = setting.Value,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM request)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "_404");
            }

            var setting = await _settingService.GetById((int)id);

            if (setting == null)
            {
                return RedirectToAction("Index", "_404");
            }

            if (request.Value == null)
            {
                SettingEditVM vm = new()
                {
                    Key = setting.Key,
                };
                ModelState.AddModelError("Value","You cannot leave the field empty");
                return View(vm);
            }

            await _settingService.Edit(setting, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
