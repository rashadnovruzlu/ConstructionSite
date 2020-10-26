﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.AdminViewModels.Blog
{
    public class BlogEditModel
    {
        public int Id { get; set; }
        public ICollection<IFormFile> file { get; set; }
        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public ICollection<IFormFile> files { get; set; }
        public List<data.Image> Images { get; set; }

        public DateTime DateTime { get; set; }

    }
}