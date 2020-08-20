﻿using System;

namespace ConstructionSite.DTO.AdminViewModels.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Imagepath { get; set; }
        public DateTime CreateDate { get; set; }
    }
}