using System.Collections.Generic;

namespace ConstructionSite.DTO.FrontViewModels.SubService
{
    public class ServiceSubServiceImage
    {
        public int id { get; set; }
        public int SubServiceID { get; set; }
        public int ServiceID { get; set; }
        public List<string> Images { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}