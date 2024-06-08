using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderTableVM> model = await _sliderService.GetAllSlidersTableVM();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
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
            
            if (await _sliderService.ExistingSlider(request.Heading))
            {
                ModelState.AddModelError("Heading", "Slider with this heading already exists");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            Slider slider = new()
            {
                Heading = request.Heading,
                Description = request.Description,
                Image = fileName,
                CreatedDate = DateTime.Now,
            };

            await _sliderService.Create(slider);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            var slider = await _sliderService.GetById((int)id);
            
            if(slider is null)
            {
                return NotFound();
            }

            SliderDetailVM model = new()
            {
                Heading = slider.Heading,
                Description = slider.Description,
                Image = slider.Image,
                CreatedDate = slider.CreatedDate.ToString("dd.MM.yyyy"),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var slider = await _sliderService.GetById((int)id);

            if (slider is null)
            {
                return NotFound();
            }

            await _sliderService.Delete(slider);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var slider = await _sliderService.GetById((int)id);

            if (slider is null)
            {
                return NotFound();
            }

            SliderEditVM model = new()
            {
                Heading = slider.Heading,
                Description = slider.Description,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var slider = await _sliderService.GetById((int)id);

            if (slider is null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View();
            }

            await _sliderService.Edit(slider, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
