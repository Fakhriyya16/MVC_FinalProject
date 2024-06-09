using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Instructors;
using MVC_FinalProject.ViewModels.Students;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public StudentController(IStudentService studentService, IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _studentService = studentService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _studentService.GetAllForTable());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var student = await _studentService.GetById((int)id);
            if (student is null)
            {
                return NotFound();
            }

            StudentDetailVM model = new()
            {
                Image = student.Image,
                FullName = student.FullName,
                Bio = student.Bio,
                CreatedDate = student.CreatedDate.ToString("dd.MM.yyyy"),
                Courses = String.Join(",", student.CourseStudents.Select(m => m.Course.Name))
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateVM request)
        {
            var courses = await _context.Courses.Include(m=>m.CourseStudents).ToListAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
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

            Student student = new()
            {
                FullName = request.FullName,
                Image = fileName,
                CreatedDate = DateTime.Now,
                Bio = request.Bio,
            };

            await _studentService.Create(student,request.CourseIds);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            if (id is null)
            {
                return BadRequest();
            }

            var student = await _studentService.GetById((int)id);

            if (student == null)
            {
                return NotFound();
            }

            StudentEditVM vm = new()
            {
                FullName = student.FullName,
                Bio = student.Bio,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StudentEditVM request)
        {
            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            if (id is null)
            {
                return BadRequest();
            }

            var student = await _studentService.GetById((int)id);

            if (student == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _studentService.Edit(student, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var student = await _studentService.GetById((int)id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentService.Delete(student);
            return Ok();
        }
    }
}
