﻿using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Projects
{
    public interface IProjectFacade
    {
        Task<RESULT<Project>> Add(ProjectAddModel projectAddModel);

        List<ProjectViewModel> GetAll(string _lang);

        bool Update(ProjectUpdateViewModel projectUpdateViewModel);

        ProjectUpdateViewModel GetForUpdate(int id);

        public bool Delete(int id);
    }
}