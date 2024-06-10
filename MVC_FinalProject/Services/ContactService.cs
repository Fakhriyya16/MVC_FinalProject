using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Contacts;

namespace MVC_FinalProject.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactTableVM>> GetAllForIndex()
        {
            var data = await _context.Contacts.ToListAsync();
            if(data is not null)
            {
                List<ContactTableVM> vm = data.Select(m=> new ContactTableVM
                {
                    Id = m.Id,
                    Email = m.Email,
                    FullName = m.FullName,
                    Subject = m.Subject,
                    Message = m.Message,
                    SendDate = m.CreatedDate.ToString("dd.MM.yyyy")
                }).ToList();

                return vm;
            }
            return null;
        }

        public async Task<Contact> GetById(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
