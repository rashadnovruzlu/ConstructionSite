using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Projects
{
    public interface IProjectFacade
    {
        Task<RESULT<Project>> Add(Project project);
    }
}
