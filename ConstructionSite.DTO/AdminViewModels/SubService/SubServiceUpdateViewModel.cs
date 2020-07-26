using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.SubService
{
    public  class SubServiceUpdateViewModel
    {
        public int Id { get; set; }
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }

       // public int SubServiceId { get; set; }
        public int ServerId { get; set; }
        public int imageId { get; set; }
        public string ImagePath { get; set; }
    }
}
