using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Students
{
    public class StudentEditVM
    {
        [Required]
        public string FullName { get; set; }
        public IFormFile NewImage { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public List<int> CourseIds { get; set; }
    }
}
