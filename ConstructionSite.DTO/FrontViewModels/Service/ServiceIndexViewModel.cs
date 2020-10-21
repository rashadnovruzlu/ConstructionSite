using System.Collections.Generic;

namespace ConstructionSite.DTO.FrontViewModels.Service
{
    public class ServiceIndexViewModel
    {
        public List<ServiceViewModel> serviceViewModels { get; set; }
        public List<SubServiceViewModel> subServiceViewModels { get; set; }
        public List<DescriptionViewModel> descriptionViewModels { get; set; }
    }
}