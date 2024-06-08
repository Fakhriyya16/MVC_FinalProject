using MVC_FinalProject.Models;

namespace MVC_FinalProject.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Course> Courses { get; set; }
        public string CreatedDate { get; set; }
    }
}
