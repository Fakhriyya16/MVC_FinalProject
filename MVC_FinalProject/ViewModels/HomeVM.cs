﻿using MVC_FinalProject.ViewModels.About;
using MVC_FinalProject.ViewModels.Categories;
using MVC_FinalProject.ViewModels.Information;
using MVC_FinalProject.ViewModels.Sliders;

namespace MVC_FinalProject.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<InformationVM> Information { get; set; }
        public AboutVM About { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
