using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Students;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentTableVM>> GetAllForTable();
        Task<Student> GetById(int id);
        Task Create(Student student, List<int> courses);
        Task Edit(Student student, StudentEditVM request);
        Task Delete(Student student);
        Task<List<StudentVM>> GetAllVM();
    }
}
