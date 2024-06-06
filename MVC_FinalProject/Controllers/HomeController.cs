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
        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllSlidersVM(),
            };
            return View(model);
        }
    }
}
