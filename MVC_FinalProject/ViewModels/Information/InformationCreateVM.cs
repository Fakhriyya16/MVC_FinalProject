using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Information
{
    public class InformationCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IconName { get; set; }
    }
}
