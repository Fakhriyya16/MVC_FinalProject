using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Instructors;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<List<InstructorTableVM>> GetAllForTable();
        Task<InstructorDetailVM> GetInstructorDetailVM(int id);
        Task<bool> ExistEmail(string email);
        Task Create(Instructor instructor);
        Task<Instructor> GetById(int id);
        Task Edit(Instructor instructor, InstructorEditVM request);
        Task Delete(Instructor instructor);
    }
}
