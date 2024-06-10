using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
