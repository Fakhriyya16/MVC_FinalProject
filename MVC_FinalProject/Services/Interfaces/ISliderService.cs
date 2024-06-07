using MVC_FinalProject.Models;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllSlidersVM();
        Task<List<SliderTableVM>> GetAllSlidersTableVM();
        Task Create(Slider slider);
        Task<bool> ExistingSlider(string heading);
        Task<Slider> GetById(int id);
        Task Delete(Slider slider);
        Task Edit(Slider slider, SliderEditVM editedSlider);
    }
}
