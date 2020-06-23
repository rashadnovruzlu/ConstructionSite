using ConstructionSite.Helpers.Core;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region --Added--
        public Result<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Result<T> AddRange(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> AddRangeAsync(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region --Delete--
        public Result<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Result<T> DeleteRange(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> DeleteRangeAsync(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region --Update--
        public Result<T> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Result<T> UpdateRange(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> UpdateRangeAsync(ICollection<T> entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region --Search--
        public T Find(Expression<Func<T, bool>> predecat)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> predecat)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predecat)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> predecat)
        {
            throw new NotImplementedException();
        }
        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region --GetAll--
        public ICollection<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
       




    }
}