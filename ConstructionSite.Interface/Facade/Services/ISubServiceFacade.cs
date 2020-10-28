﻿using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Services
{
    public interface ISubServiceFacade
    {
        List<SubServiceViewModel> GetAll(string _lang);
        Task<RESULT<SubService>> Add(SubServiceAddModel subServiceAddModel);
        

    }
}
