using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Users;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            List<UserTableVM> userWithRoles = new List<UserTableVM>();

            foreach (var item in users)
            {
                var roles = await _userManager.GetRolesAsync(item);
                string userRoles = String.Join(",", roles.ToArray());

                userWithRoles.Add(new UserTableVM
                {
                    FullName = item.FullName,
                    Roles = userRoles,
                    Email = item.Email,
                });

            }


            return View(userWithRoles);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole()
        {
            var users = _userManager.Users.ToList();

            ViewBag.Usernames = users.Select(m => new SelectListItem { Text = m.UserName, Value = m.UserName })
                            .ToList();
            ViewBag.Roles = _roleManager.Roles.Select(m => new SelectListItem { Text = m.Name, Value = m.Name })
                                .ToList();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(UserRoleVM request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return View();
            }
            var result = await _userManager.AddToRolesAsync(user, new List<string> { request.Role });

            return RedirectToAction(nameof(Index));
        }
    }
}
