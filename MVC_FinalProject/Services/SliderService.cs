using Microsoft.AspNetCore.Hosting;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task Create(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Slider slider)
        {
            slider.SoftDeleted = true;
            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Slider slider, SliderEditVM request)
        {
            slider.Heading = request.Heading;
            slider.Description = request.Description;
            var existImage = slider.Image;

            if (request.NewImage is not null)
            {
                string path = Path.Combine(_env.WebRootPath, "assets/img", existImage);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                string fileName = Guid.NewGuid().ToString() + request.NewImage.FileName;

                path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }
                slider.Image = fileName;
            }

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistingSlider(string heading)
        {
            return await _context.Sliders.AnyAsync(m=>m.Heading.Trim() == heading.Trim());
        }

        public async Task<List<SliderTableVM>> GetAllSlidersTableVM()
        {
            var result = await _context.Sliders.Where(m=>!m.SoftDeleted).ToListAsync();

            List<SliderTableVM> sliders = result.Select(m => new SliderTableVM
            {
                Heading = m.Heading,
                Description = m.Description,
                Image = m.Image,
                Id = m.Id,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
            }).ToList();

            return sliders;
        }

        public async Task<List<SliderVM>> GetAllSlidersVM()
        {
            var result = await _context.Sliders.Where(m=>!m.SoftDeleted).ToListAsync();
           
            List<SliderVM> sliders = result.Select(m => new SliderVM
            {
                Heading = m.Heading,
                Description = m.Description,
                Image = m.Image,
            }).ToList();

            return sliders;
        }

        public async Task<Slider> GetById(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
