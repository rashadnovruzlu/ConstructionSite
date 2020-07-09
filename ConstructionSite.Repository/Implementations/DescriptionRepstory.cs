using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Repository.Implementations
{
   public class DescriptionRepstory: GenericRepository<Description>, IDescriptionRepstory
    {
        public DescriptionRepstory(ConstructionDbContext context) : base(context)
        {

        }
    }
}
