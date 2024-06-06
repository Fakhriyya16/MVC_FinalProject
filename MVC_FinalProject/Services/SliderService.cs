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
        public SliderService(AppDbContext context)
        {
            _context = context;
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
            var result = await _context.Sliders.ToListAsync();
           
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
