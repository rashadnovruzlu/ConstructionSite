using ConstructionSite.Helpers.Core;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task<Result<int>> Commit()
        {
            throw new NotImplementedException();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
