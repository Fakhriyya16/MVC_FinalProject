using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.About
{
    public class AboutEditVM
    {
        public IFormFile NewImage { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
