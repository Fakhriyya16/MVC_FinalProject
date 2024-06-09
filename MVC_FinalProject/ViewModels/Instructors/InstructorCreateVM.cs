﻿using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
    }
}
