using MVC_FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Courses
{
    public class CourseEditVM
    {
        [Required]
        public string Name { get; set; }
        public List<IFormFile> NewImages { get; set; }
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
        public List<CourseImage> Images { get; set; }
    }
}
