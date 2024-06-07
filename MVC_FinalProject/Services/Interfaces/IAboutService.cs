using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.About;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface IAboutService
    {
        Task<AboutVM> GetAllVM();
        Task<AboutDetailVM> GetAllForDetailVM();
        Task Edit(About about, AboutEditVM edited);
        Task<About> GetById(int id);
    }
}
