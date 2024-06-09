using MVC_FinalProject.Models;

namespace MVC_FinalProject.ViewModels.Instructors
{
    public class InstructorTableVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Courses { get; set; }
    }
}
