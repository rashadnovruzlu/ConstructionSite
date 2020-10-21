using System.Collections.Generic;

namespace ConstructionSite.DTO.FrontViewModels.Portfolio
{
    public class ProjectViewDetailsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public List<string> Images { get; set; }
    }
}