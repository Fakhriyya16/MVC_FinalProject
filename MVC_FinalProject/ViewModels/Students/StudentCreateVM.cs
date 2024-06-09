using MVC_FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Students
{
    public class StudentCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public List<int> CourseIds { get; set; }
    }
}
