using System.ComponentModel.DataAnnotations;

namespace MVC_FinalProject.ViewModels.Settings
{
    public class SettingEditVM
    {
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
