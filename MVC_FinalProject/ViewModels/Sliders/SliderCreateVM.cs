using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
