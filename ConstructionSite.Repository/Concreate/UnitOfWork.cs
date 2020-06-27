using ConstructionSite.Helpers.Core;
using ConstructionSite.Helpers.Exceptions;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {   
        private readonly DbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private string _errorMessage = string.Empty;

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<RESULT<int>> Commit()
        {
            RESULT<int> result = new RESULT<int> { IsDone = false };
            try
            {
                result.Data = await _dbContext.SaveChangesAsync();
                result.IsDone = true;
            }
            catch (Exception ex)
            {
               // _errorMessage += Tools.WriteExeptions(ex);
                throw new DbContextCommitException(_errorMessage, ex);
            }
            return result;
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}