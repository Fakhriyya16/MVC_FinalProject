﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Courses;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _appDbContext;
        public CourseController(ICourseService courseService, IWebHostEnvironment env, AppDbContext appDbContext)
        {
            _courseService = courseService;
            _env = env;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetAllForTable());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var instructors = await _appDbContext.Instructors.ToListAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var instructors = await _appDbContext.Instructors.ToListAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "FullName");

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (await _courseService.ExistCourse(request.Name))
            {
                ModelState.AddModelError("Name", "Course with this name already exists");
                return View();
            }

            List<CourseImage> images = new();

            foreach (var item in request.Images)
            {
                if (!(item.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 500KB");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Course course = new()
            {
                Name = request.Name,
                CourseImages = images,
                CategoryId = request.CategoryId,
                Price = decimal.Parse(request.Price.Replace(".", ",")),
                Rating = request.Rating,
                InstructorId  = request.InstructorId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.Now,
            };

            await _courseService.Create(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var course = await _courseService.GetById((int)id);

            if(course is null)
            {
                return RedirectToAction("Index", "_404");
            }

            return View(await _courseService.GetCourseForDetail((int)id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var course = await _courseService.GetById((int)id);

            if (course is null)
            {
                return RedirectToAction("Index", "_404");
            }

            await _courseService.Delete(course);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var instructors = await _appDbContext.Instructors.ToListAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "FullName");
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var course = await _courseService.GetById((int)id);

            if (course is null)
            {
                return RedirectToAction("Index", "_404");
            }

            CourseEditVM vm = new()
            {
                Name = course.Name,
                CategoryId = course.CategoryId,
                InstructorId = (int)course.InstructorId,
                Images = course.CourseImages.ToList(),
                Price = course.Price.ToString(course.Price % 1 == 0 ? "0" : "0,00"),
                Rating = course.Rating,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var instructors = await _appDbContext.Instructors.ToListAsync();
            ViewBag.Instructors = new SelectList(instructors, "Id", "FullName");
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var course = await _courseService.GetById((int)id);

            if (course is null)
            {
                return RedirectToAction("Index", "_404");
            }

            if(!ModelState.IsValid)
            {
                return View();
            }
            request.Images = course.CourseImages.ToList();
            await _courseService.Edit(course, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id is null) return RedirectToAction("Index", "_404");

            var image = await _courseService.GetImageByIdAsync((int)id);
            
            if (image is null) return RedirectToAction("Index", "_404");

            string oldPath = Path.Combine(_env.WebRootPath, "assets/img", image.Name);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            await _courseService.DeleteImage(image);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MakeMain(int? id)
        {
            if (id is null) return RedirectToAction("Index", "_404");

            var image = await _courseService.GetImageByIdAsync((int)id);

            var course = await _courseService.GetByIdWithImagesAsync(image.CourseId);

            if (image is null) return RedirectToAction("Index", "_404");

            if (course.CourseImages.Count == 0) return NotFound();

            await _courseService.UpdateImages(course, image);
            return Ok();
        }
    }
}
