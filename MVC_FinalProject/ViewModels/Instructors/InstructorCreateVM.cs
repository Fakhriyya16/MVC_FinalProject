namespace MVC_FinalProject.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public IFormFile Image { get; set; }
    }
}
