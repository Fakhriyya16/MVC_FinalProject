namespace MVC_FinalProject.Models
{
    public class Instructor : BaseEntity
    {
        public string FullName { get; set; }
        public string Image  { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
