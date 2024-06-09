using MVC_FinalProject.Models;

namespace MVC_FinalProject.ViewModels.Students
{
    public class StudentTableVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public string Courses { get; set; }
    }
}
