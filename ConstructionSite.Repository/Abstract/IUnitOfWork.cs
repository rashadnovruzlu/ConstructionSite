using ConstructionSite.Helpers.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Abstract
{
   public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<Result<int>> Commit();

        void Rollback();
    }
}
