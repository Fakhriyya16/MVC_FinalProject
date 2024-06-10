using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Categories;
using MVC_FinalProject.ViewModels.Contacts;


namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _contactService.GetAllForIndex();
            if(data is null)
            {
                return RedirectToAction("Index", "_404");
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "_404");
            }

            var contact = await _contactService.GetById((int)id);

            if (contact == null)
            {
                return RedirectToAction("Index", "_404");
            }

            await _contactService.Delete(contact);
            return Ok();
        }

        
    }

}
