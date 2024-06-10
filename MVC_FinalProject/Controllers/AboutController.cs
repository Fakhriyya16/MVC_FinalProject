using Microsoft.AspNetCore.Mvc;

namespace MVC_FinalProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
