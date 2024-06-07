using MVC_FinalProject.ViewModels.Information;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<InformationVM> Information { get; set; }
    }
}
