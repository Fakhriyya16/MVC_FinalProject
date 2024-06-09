using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Courses;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseTableVM>> GetAllForTable();
        Task<bool> ExistCourse(string name);
        Task Create(Course course);
        Task<CourseDetailVM> GetCourseForDetail(int id);
        Task<Course> GetById(int id);
        Task Delete(Course course);
        Task<CourseImage> GetImageByIdAsync(int id);
        Task DeleteImage(CourseImage image);
        Task<Course> GetByIdWithImagesAsync(int id);
        Task UpdateImages(Course course, CourseImage image);
        Task Edit(Course course, CourseEditVM request);
    }
}
