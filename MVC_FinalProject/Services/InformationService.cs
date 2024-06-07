using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Information;

namespace MVC_FinalProject.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        public InformationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Information information)
        {
            await _context.Information.AddAsync(information);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Information information, InformationEditVM edited)
        {
            information.Title = edited.Title;
            information.Description = edited.Description;
            information.Icon.ClassName = edited.IconName;

            _context.Information.Update(information);
            await _context.SaveChangesAsync();
        }

        public async Task<List<InformationTableVM>> GetAllInfoForTableVM()
        {
            var infos = await _context.Information.Include(m => m.Icon).ToListAsync();
            List<InformationTableVM> infosVM = infos.Select(m => new InformationTableVM
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IconName = m.Icon.ClassName,
            }).ToList();

            return infosVM;
        }

        public async Task<List<InformationVM>> GetAllInfoVM()
        {
           var infos = await _context.Information.Include(m=>m.Icon).ToListAsync();
           List<InformationVM> infosVM = infos.Select(m => new InformationVM
           {
               Title = m.Title,
               Description = m.Description,
               IconName = m.Icon.ClassName,
           }).ToList();

            return infosVM;
        }

        public async Task<Information> GetById(int id)
        {
            return await _context.Information.Include(m => m.Icon).FirstOrDefaultAsync(m => m.Id == id);
        }
       
        public async Task Delete(Information information)
        {
            _context.Remove(information);
            await _context.SaveChangesAsync();
        }
    }
}
