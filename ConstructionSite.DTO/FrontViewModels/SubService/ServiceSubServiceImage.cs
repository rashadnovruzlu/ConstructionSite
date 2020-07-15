using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels.SubService
{
  public  class ServiceSubServiceImage
    {
        public int id { get; set; }
        public int SubServiceID { get; set; }

        public string       SubName { get; set; }
        public string       Content { get; set; }
       
       
    }
}
