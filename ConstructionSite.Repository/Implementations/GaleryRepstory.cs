using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Repository.Implementations
{
   public class GaleryRepstory: GenericRepository<Galery>, IGaleryRepstory
    {
        public GaleryRepstory(ConstructionDbContext context) : base(context)
        {
        }
    }
}
