using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Categories;
using MVC_FinalProject.ViewModels.Instructors;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;
        public InstructorController(IInstructorService instructorService,
                                    AppDbContext appDbContext,
                                    IWebHostEnvironment env)
        {
            _instructorService = instructorService;
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _instructorService.GetAllForTable());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }
            var instructor = await _instructorService.GetInstructorDetailVM((int) id);
            if (instructor is null)
            {
                return NotFound();
            }
            return View(await _instructorService.GetInstructorDetailVM((int)id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorCreateVM request)
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

            if (await _instructorService.ExistEmail(request.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            Instructor instructor = new()
            {
                FullName = request.FullName,
                Image = fileName,
                CreatedDate = DateTime.Now,
                Email = request.Email,
                Position = request.Position,
            };

            await _instructorService.Create(instructor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var instructor = await _instructorService.GetById((int)id);

            if (instructor == null)
            {
                return NotFound();
            }

            InstructorEditVM vm = new()
            {
                FullName=instructor.FullName,
                Email=instructor.Email,
                Position = instructor.Position,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InstructorEditVM request)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var instructor = await _instructorService.GetById((int)id);

            if (instructor == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _instructorService.Edit(instructor, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var instructor = await _instructorService.GetById((int)id);

            if (instructor == null)
            {
                return NotFound();
            }

            await _instructorService.Delete(instructor);
            return Ok();
        }
    }
}
