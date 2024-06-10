using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.About;

namespace MVC_FinalProject.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;
        public AboutService(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }

        public async Task Create(About about)
        {
            await _appDbContext.Abouts.AddAsync(about);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(About about)
        {
            _appDbContext.Abouts.Remove(about);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Edit(About about, AboutEditVM edited)
        {
            about.Heading = edited.Heading;
            about.Description = edited.Description;
            var existImage = about.Image;

            if (edited.NewImage is not null)
            {
                string path = Path.Combine(_env.WebRootPath, "assets/img", existImage);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                string fileName = Guid.NewGuid().ToString() + edited.NewImage.FileName;

                path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await edited.NewImage.CopyToAsync(stream);
                }
                about.Image = fileName;
            }

            _appDbContext.Abouts.Update(about);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<AboutDetailVM> GetAllForDetailVM()
        {
            var data = await _appDbContext.Abouts.FirstOrDefaultAsync();

            if(data is not null)
            {
                AboutDetailVM model = new()
                {
                    Id = data.Id,
                    Heading = data.Heading,
                    Description = data.Description,
                    Image = data.Image,
                    CreatedDate = data.CreatedDate.ToString("dd.MM.yyyy"),
                };
                return model;
            }
            return null;
        }

        public async Task<AboutVM> GetAllVM()
        {
            var data = await _appDbContext.Abouts.FirstOrDefaultAsync();

            if(data is not null)
            {
                AboutVM model = new()
                {
                    Heading = data.Heading,
                    Description = data.Description,
                    Image = data.Image,
                };
                return model;
            }
            return null;
        }

        public async Task<About> GetById(int id)
        {
            return await _appDbContext.Abouts.FirstOrDefaultAsync();
        }

        public async Task<int> GetCount()
        {
            return await _appDbContext.Abouts.CountAsync();
        }
    }
}
