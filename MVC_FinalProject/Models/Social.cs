namespace MVC_FinalProject.Models
{
    public class Social : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<InstructorSocial> InstructorSocials { get; set; }
    }
}
