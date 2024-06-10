using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Contacts;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactTableVM>> GetAllForIndex();
        Task Delete(Contact contact);
        Task<Contact> GetById(int id);
        Task Create(Contact contact);
    }
}
