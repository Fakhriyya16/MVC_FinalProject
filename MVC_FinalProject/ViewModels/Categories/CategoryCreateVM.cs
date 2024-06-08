using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
