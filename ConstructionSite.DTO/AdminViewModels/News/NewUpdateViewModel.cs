﻿using System;

namespace ConstructionSite.DTO.AdminViewModels.News
{
    public class NewUpdateViewModel
    {
        public int Id { get; set; }
        public string TittleAz { get; set; }

        public string TittleEn { get; set; }

        public string TittleRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public DateTime CreateDate { get; set; }
    }
}