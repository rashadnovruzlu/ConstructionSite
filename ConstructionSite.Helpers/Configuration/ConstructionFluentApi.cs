using System;
using System.Collections.Generic;
using System.Text;
using ConstructionSite.Entity;
using ConstructionSite.Entity.Models;
using DocumentFormat.OpenXml.Math;
using Microsoft.EntityFrameworkCore;
using ConstructionSite.Entity.Data;

namespace ConstructionSite.Helpers.Configuration
{
    class ConstructionFluentApi 
    {
        #region Istifade olunmur
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Service>()
                             .HasOne<Image>(i => i.Image)
                                 .WithOne(x => x.Service);

             base.OnModelCreating(modelBuilder);
         }
        */
        #endregion
    }
}
