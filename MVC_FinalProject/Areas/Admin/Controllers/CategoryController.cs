using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Categories;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<CategoryTableVM> vm = await _categoryService.GetAllCategoriesVM();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
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

            if (await _categoryService.ExistCategory(request.Name))
            {
                ModelState.AddModelError("Name", "Category with this name already exists");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            Category category = new()
            {
                Name = request.Name,
                Image = fileName,
                CreatedDate = DateTime.Now,
            };

            await _categoryService.Create(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var category = await _categoryService.GetById((int) id);

            if(category == null)
            {
                return RedirectToAction("Index", "_404");
            }

            CategoryDetailVM vm = new()
            {
                Name = category.Name,
                Image = category.Image,
                CreatedDate = category.CreatedDate.ToString("dd.MM.yyyy"),
                Courses = category.Courses.ToList(),
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var category = await _categoryService.GetById((int)id);

            if (category == null)
            {
                return RedirectToAction("Index", "_404");
            }

            CategoryEditVM vm = new()
            {
                Name = category.Name,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var category = await _categoryService.GetById((int)id);

            if (category == null)
            {
                return RedirectToAction("Index", "_404");
            }

            await _categoryService.Edit(category, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var category = await _categoryService.GetById((int)id);

            if (category == null)
            {
                return RedirectToAction("Index", "_404");
            }

            await _categoryService.Delete(category);
            return Ok();
        }
    }
}
