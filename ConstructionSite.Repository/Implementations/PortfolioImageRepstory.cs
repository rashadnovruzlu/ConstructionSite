using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Repository.Implementations
{
    public class PortfolioImageRepstory : GenericRepository<PortfolioImage>, IPortfolioImageRepostory
    {
        public PortfolioImageRepstory(ConstructionDbContext context) : base(context)
        {

        }
    }
}
