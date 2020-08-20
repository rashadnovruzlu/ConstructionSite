using System.Collections.Generic;

namespace ConstructionSite.DTO.FrontViewModels.Service
{
    public class ServiceMenuViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual IList<SingleSubServiceViewModel> SubServices { get; set; }
    }
}