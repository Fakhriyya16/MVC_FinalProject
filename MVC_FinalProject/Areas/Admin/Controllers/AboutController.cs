using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.About;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllForDetailVM());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var about = await _aboutService.GetById((int)id);

            if (about is null)
            {
                return NotFound();
            }

            AboutEditVM model = new()
            {
                Heading = about.Heading,
                Description = about.Description,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var about = await _aboutService.GetById((int)id);

            if (about is null)
            {
                return NotFound();
            }

            await _aboutService.Edit(about, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
