using Microsoft.AspNetCore.Mvc;

namespace MVC_FinalProject.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
