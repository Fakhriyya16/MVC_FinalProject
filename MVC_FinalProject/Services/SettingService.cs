using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Settings;

namespace MVC_FinalProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _appDbContext;
        public SettingService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Edit(Setting setting, SettingEditVM request)
        {
            setting.Value = request.Value;
            _appDbContext.Settings.Update(setting);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Setting>> GetAllSettings()
        {
            return await _appDbContext.Settings.ToListAsync();
        }

        public async Task<Setting> GetById(int id)
        {
            return await _appDbContext.Settings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
