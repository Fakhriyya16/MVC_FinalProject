namespace MVC_FinalProject.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
