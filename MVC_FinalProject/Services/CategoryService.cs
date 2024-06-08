using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Categories;

namespace MVC_FinalProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Category category, CategoryEditVM request)
        {
            category.Name = request.Name;
            var existImage = category.Image;

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
                category.Image = fileName;
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistCategory(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<List<CategoryTableVM>> GetAllCategoriesVM()
        {
            var data = await _context.Categories.ToListAsync();
            List<CategoryTableVM> result = data.Select(m => new CategoryTableVM
            {
                Id = m.Id,
                Name = m.Name,
                Image = m.Image,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
            }).ToList();
            return result;
        }

        public async Task<List<Category>> GetAllCategoriesWithCourses()
        {
            return await _context.Categories.Include(m => m.Courses).ToListAsync();
        }

        public async Task<List<CategoryVM>> GetAllVM()
        {
            var data = await _context.Categories.Include(m => m.Courses).ToListAsync();
            List<CategoryVM> vm = data.Select(m=> new CategoryVM
            {
                Name = m.Name,
                Image = m.Image,
                CourseCount = m.Courses.Count,
            }).ToList();
            return vm;
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.Include(m=>m.Courses).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
