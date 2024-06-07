using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_FinalProject.Models
{
    public class Information : BaseEntity
    {
        [ForeignKey(nameof(Icon))]
        public int IconId { get; set; }
        public Icon Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
