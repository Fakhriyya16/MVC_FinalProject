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
        public HomeController(ISliderService sliderService,
                              IInformationService informationService,
                              IAboutService aboutService,
                              ICategoryService categoryService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
            _aboutService = aboutService;
            _categoryService = categoryService;

        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllSlidersVM(),
                Information = await _informationService.GetAllInfoVM(),
                About = await _aboutService.GetAllVM(),
                Categories = await _categoryService.GetAllVM(),
            };
            return View(model);
        }
    }
}
