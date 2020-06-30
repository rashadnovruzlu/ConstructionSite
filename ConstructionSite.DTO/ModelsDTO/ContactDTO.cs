﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.ModelsDTO
{
    class ContactDTO
    {
        public int Id { get; set; }
        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
