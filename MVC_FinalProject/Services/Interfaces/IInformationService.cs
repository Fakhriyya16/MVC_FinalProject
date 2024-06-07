using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Information;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface IInformationService
    {
        Task<List<InformationVM>> GetAllInfoVM();
        Task<List<InformationTableVM>> GetAllInfoForTableVM();
        Task CreateAsync(Information information);
        Task Edit(Information information, InformationEditVM edited);
        Task<Information> GetById(int id);
        Task Delete(Information information);
    }
}
