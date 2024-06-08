using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.About
{
    public class AboutCreateVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
