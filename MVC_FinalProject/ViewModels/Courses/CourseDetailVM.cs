namespace MVC_FinalProject.ViewModels.Courses
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; }
        public string Price { get; set; }
        public int Rating { get; set; }
        public int Duration { get; set; }
        public string InstructorName { get; set; }
        public int StudentCount { get; set; }
    }
}
