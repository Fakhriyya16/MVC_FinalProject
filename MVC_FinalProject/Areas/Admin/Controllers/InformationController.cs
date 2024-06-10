using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Information;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly AppDbContext _appDbContext;
        public InformationController(IInformationService informationService,
                                     AppDbContext appDbContext)
        {
            _informationService = informationService;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _informationService.GetAllInfoForTableVM());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Icons = await _appDbContext.Icons.Select(m => new SelectListItem { Text = m.ClassName, Value = m.ClassName })
                                .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
        {
            ViewBag.Icons = await _appDbContext.Icons.Select(m => new SelectListItem { Text = m.ClassName, Value = m.ClassName })
                                .ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            Information info = new()
            {
                Title = request.Title,
                Description = request.Description,
                Icon = new Icon()
                {
                    ClassName = request.IconName
                },
                CreatedDate = DateTime.Now
            };

            await _informationService.CreateAsync(info);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Icons = await _appDbContext.Icons.Select(m => new SelectListItem { Text = m.ClassName, Value = m.ClassName })
                    .ToListAsync();
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var info = await _informationService.GetById((int)id);

            if (info is null)
            {
                return RedirectToAction("Index", "_404");
            }

            InformationEditVM model = new()
            {
                Title = info.Title,
                Description = info.Description,
                IconName = info.Icon.ClassName,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InformationEditVM request)
        {
            ViewBag.Icons = await _appDbContext.Icons.Select(m => new SelectListItem { Text = m.ClassName, Value = m.ClassName })
                    .ToListAsync();
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var info = await _informationService.GetById((int)id);

            if (info is null)
            {
                return RedirectToAction("Index", "_404");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _informationService.Edit(info, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var info = await _informationService.GetById((int)id);

            if (info is null)
            {
                return RedirectToAction("Index", "_404");
            }

            await _informationService.Delete(info);
            return Ok();
        }
    }
}

