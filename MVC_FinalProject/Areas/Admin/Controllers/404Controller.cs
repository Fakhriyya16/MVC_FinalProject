using Microsoft.AspNetCore.Mvc;

namespace MVC_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class _404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
