﻿using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class NewsImageRepository : GenericRepository<NewsImage>, INewsImageRepository
    {
        public NewsImageRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}