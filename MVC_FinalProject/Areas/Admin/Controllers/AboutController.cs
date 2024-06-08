using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.About;
using MVC_FinalProject.ViewModels.Information;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(IAboutService aboutService, IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AboutCount = await _aboutService.GetCount();
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

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _aboutService.Edit(about, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!(request.Image.Length / 1024 < 500))
            {
                ModelState.AddModelError("Image", "Image max size is 500KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            About about = new()
            {
                Heading = request.Heading,
                Description = request.Description,
                Image = fileName,
                CreatedDate = DateTime.Now,
            };

            await _aboutService.Create(about);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
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

            await _aboutService.Delete(about);
            return Ok();
        }
    }
}
