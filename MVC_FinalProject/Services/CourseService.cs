using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Courses;

namespace MVC_FinalProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task Create(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Course course)
        {
            course.SoftDeleted = true;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(CourseImage image)
        {
            _context.CourseImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistCourse(string name)
        {
            return await _context.Courses.AnyAsync(c => c.Name == name);
        }

        public async Task<CourseDetailVM> GetCourseForDetail(int id)
        {
            var data = await _context.Courses.Include(m => m.CourseImages)
                                 .Include(m => m.Instructor)
                                 .Include(m => m.Category)
                                 .Include(m => m.CourseStudents)
                                 .FirstOrDefaultAsync(m=>m.Id == id);
            CourseDetailVM vm = new()
            {
                Name = data.Name,
                MainImage = data.CourseImages.FirstOrDefault(m => m.IsMain)?.Name,
                Images = data.CourseImages.Where(m => !m.IsMain).Select(m => m.Name).ToList(),
                CategoryName = data.Category.Name,
                Price = data.Price.ToString(data.Price % 1 == 0 ? "0" : "0.00"),
                Rating = data.Rating,
                Duration = ((data.EndDate.Year - data.StartDate.Year) * 12) + data.EndDate.Month - data.StartDate.Month,
                InstructorName = data.Instructor.FullName,
                StudentCount = data.CourseStudents.Count,
            };

            return vm;
        }

        public async Task<List<CourseTableVM>> GetAllForTable()
        {
            var data = await _context.Courses.Include(m=>m.CourseImages)
                                             .Include(m=>m.Instructor)
                                             .Include(m=>m.Category)
                                             .Include(m=>m.CourseStudents)
                                             .ToListAsync();
            List<CourseTableVM> vm = data.Select(m => new CourseTableVM
            {
                Id = m.Id,
                Name = m.Name,
                Image = m.CourseImages.FirstOrDefault(m => m.IsMain)?.Name,
                CategoryName = m.Category.Name,
                Price = m.Price,
                Duration = ((m.EndDate.Year - m.StartDate.Year) * 12) + m.EndDate.Month - m.StartDate.Month,
                InstructorName = m.Instructor.FullName,
                StudentCount = m.CourseStudents.Count(),
            }).ToList();

            return vm;
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.Include(m=>m.Category).Include(m=>m.Instructor).Include(m=>m.CourseImages).FirstOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<CourseImage> GetImageByIdAsync(int id)
        {
            return await _context.CourseImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Course> GetByIdWithImagesAsync(int id)
        {
            return await _context.Courses.Include(m => m.CourseImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateImages(Course course, CourseImage image)
        {
            foreach (var item in course.CourseImages)
            {
                item.IsMain = false;
                _context.CourseImages.Update(item);
            }

            image.IsMain = true;
            _context.CourseImages.Update(image);

            await _context.SaveChangesAsync();
        }

        public async Task Edit(Course course, CourseEditVM request)
        {
            if (request.NewImages is not null)
            {
                List<CourseImage> images = new();

                foreach (var item in request.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                    using (FileStream stream = new(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    images.Add(new CourseImage { Name = fileName });
                }
                course.CourseImages = images;
            }



            course.Name = request.Name;
            course.CategoryId = request.CategoryId;
            course.InstructorId = request.InstructorId;
            course.Price = decimal.Parse(request.Price.Replace(".", ","));
            course.Rating = request.Rating;
            course.StartDate = request.StartDate;
            course.EndDate = request.EndDate;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CourseVM>> GetAllSortedVM()
        {
            var data = await _context.Courses.Include(m => m.Instructor)
                                             .Include(m => m.CourseImages)
                                             .Include(m => m.CourseStudents)
                                             .ThenInclude(m => m.Student).ToListAsync();

            List<CourseVM> result = data.Select(m => new CourseVM
            {
                Name = m.Name,
                Image = m.CourseImages.Where(m => m.IsMain).FirstOrDefault().Name,
                Price = m.Price.ToString(m.Price % 1 == 0 ? "0" : "0.00"),
                Rating = m.Rating,
                Instructor = m.Instructor.FullName,
                Duration = ((m.EndDate.Year - m.StartDate.Year) * 12) + m.EndDate.Month - m.StartDate.Month,
                StudentCount = m.CourseStudents.Count
            }).ToList();

            var sorted = result.OrderByDescending(m => m.Rating).Take(3).ToList();
            return sorted;
        }
    }
}
