using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.SubService
{
   public class SubServiceViewUpdateModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceID { get; set; }
        public string ImagePath { get; set; }
        public int ImageId { get; set; }
    }
}
