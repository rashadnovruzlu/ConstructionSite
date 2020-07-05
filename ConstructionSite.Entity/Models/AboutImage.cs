﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class AboutImage
    {
       
        public int Id { get; set; }

        public virtual About About { get; set; }
       
        public int AboutId { get; set; }
        
        public virtual Image Image { get; set; }
       
        public int ImageId { get; set; }
    }
}
