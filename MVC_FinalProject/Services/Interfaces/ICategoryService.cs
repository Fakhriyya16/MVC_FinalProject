using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Categories;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesWithCourses();
        Task<List<CategoryTableVM>> GetAllCategoriesVM();
        Task Create(Category category);
        Task Edit(Category category, CategoryEditVM request);
        Task Delete(Category category);
        Task<bool> ExistCategory(string name);
        Task<Category> GetById(int id);
        Task<List<CategoryVM>> GetAllVM();
    }
}
