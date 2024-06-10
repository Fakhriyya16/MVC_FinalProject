using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Instructors;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ISocialService
    {
        Task AddSocialLink(Instructor instructor, string socialName, string socialUrl);
        Task UpdateSocialLinks(Instructor instructor, InstructorEditVM request);
        Task UpdateOrAddSocialLink(Instructor instructor, string socialName, string socialUrl);
        Task<Social> GetOrCreateSocialByName(string name);
    }
}
