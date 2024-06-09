using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels;
using MVC_FinalProject.ViewModels.Sliders;


namespace MVC_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IInformationService _informationService;
        public readonly IAboutService _aboutService;
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IInstructorService _instructorService;
        public HomeController(ISliderService sliderService,
                              IInformationService informationService,
                              IAboutService aboutService,
                              ICategoryService categoryService,
                              ICourseService courseService,
                              IStudentService studentService,
                              IInstructorService instructorService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
            _aboutService = aboutService;
            _categoryService = categoryService;
            _courseService = courseService;
            _studentService = studentService;
            _instructorService = instructorService;

        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllSlidersVM(),
                Information = await _informationService.GetAllInfoVM(),
                About = await _aboutService.GetAllVM(),
                Categories = await _categoryService.GetAllVM(),
                Courses = await _courseService.GetAllVM(),
                Instructors = await _instructorService.GetAllVM(),
                Students = await _studentService.GetAllVM()
            };
            return View(model);
        }
    }
}
