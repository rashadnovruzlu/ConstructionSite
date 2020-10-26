using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.ViwModel.AdminViewModels.Images
{
    public class ImageUpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Path { get; set; }

        public string VideoPath { get; set; }

        public IFormFile file { get; set; }
    }
}
