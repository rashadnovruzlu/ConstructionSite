﻿using ConstructionSite.Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.ViwModel.AdminViewModels.Slider
{
    public class SliderUpdateViewModel
    {
        public int Id { get; set; }
        public string TittleAz { get; set; }
        public string TittleRu { get; set; }
        public string TittleEn { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public int ImageID { get; set; }
        public IFormFile files { get; set; }
        public string pathImage { get; set; }
    }
}
