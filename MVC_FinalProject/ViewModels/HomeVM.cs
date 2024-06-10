using MVC_FinalProject.ViewModels.About;
using MVC_FinalProject.ViewModels.Categories;
using MVC_FinalProject.ViewModels.Courses;
using MVC_FinalProject.ViewModels.Information;
using MVC_FinalProject.ViewModels.Instructors;
using MVC_FinalProject.ViewModels.Sliders;
using MVC_FinalProject.ViewModels.Students;

namespace MVC_FinalProject.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<InformationVM> Information { get; set; }
        public AboutVM? About { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public List<CourseVM> Courses { get; set; }
        public List<InstructorVM> Instructors { get; set; }
        public List<StudentVM> Students { get; set; }
    }
}
