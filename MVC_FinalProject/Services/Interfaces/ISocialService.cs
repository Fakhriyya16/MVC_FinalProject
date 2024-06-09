using MVC_FinalProject.Models;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ISocialService
    {
        Task AddSocialLink(Instructor instructor, string socialName, string socialUrl);
        Task<Social> GetOrCreateSocialByName(string name);
        Task UpdateSocialLink(Instructor instructor, string socialName, string socialUrl);
    }
}
