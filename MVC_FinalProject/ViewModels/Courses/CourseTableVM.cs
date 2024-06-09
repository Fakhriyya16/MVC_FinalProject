namespace MVC_FinalProject.ViewModels.Courses
{
    public class CourseTableVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string InstructorName { get; set; }
        public int StudentCount { get; set; }
    }
}
