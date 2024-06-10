using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Settings;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ISettingService
    {
        Task<List<Setting>> GetAllSettings();
        Task<Setting> GetById(int id);
        Task Edit(Setting setting, SettingEditVM request);
    }
}
