using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Sliders
{
    public class SliderEditVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
