using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Contacts;

namespace MVC_FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(IContactService contactService,
                                 UserManager<AppUser> userManager)
        {
            _contactService = contactService;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                ContactCreateVM vm = new()
                {
                    FullName = user.Result.FullName,
                    Email = user.Result.Email,
                };
                return View(vm);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "404");
            }

            Contact contact = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                Subject = request.Subject,
                Message = request.Message,
                CreatedDate = DateTime.Now,
            };

            await _contactService.Create(contact);
            return RedirectToAction("Index", "Home");
        }
    }
}
