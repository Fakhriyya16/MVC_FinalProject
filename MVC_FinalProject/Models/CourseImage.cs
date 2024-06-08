namespace MVC_FinalProject.Models
{
    public class CourseImage
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
    }
}
