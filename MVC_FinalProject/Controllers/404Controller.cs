﻿using Microsoft.AspNetCore.Mvc;

namespace MVC_FinalProject.Controllers
{
    public class _404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
