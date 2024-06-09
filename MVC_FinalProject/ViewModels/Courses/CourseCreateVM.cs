using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Courses
{
    public class CourseCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
