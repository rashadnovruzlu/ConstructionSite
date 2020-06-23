using ConstructionSite.Helpers.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        #region ---List---
        ICollection<T> GetAll();

        Task<ICollection<T>> GetAllAsync();
        #endregion

        #region --GetByID--
        T GetById(int id);

        Task<T> GetByIdAsync(int id);
        #endregion


        T Find(Expression<Func<T, bool>> predecat);

        Task<T> FindAsync(Expression<Func<T, bool>> predecat);

        ICollection<T> FindAll(Expression<Func<T, bool>> predecat);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predecat);

        Result<T> Add(T entity);

        Task<Result<T>> AddAsync(T entity);

        Task<Result<T>> AddRangeAsync(ICollection<T> entities);

        Result<T> AddRange(ICollection<T> entities);

        Result<T> Update(T updated);

        Result<T> UpdateRange(ICollection<T> entities);

        Task<Result<T>> UpdateAsync(T updated);

        Task<Result<T>> UpdateRangeAsync(ICollection<T> entities);

        Result<T> Delete(T t);

        Result<T> DeleteUnCommitted(T t);

        Task<Result<T>> DeleteAsync(T t);

        Result<T> DeleteRange(ICollection<T> entities);

        Task<Result<T>> DeleteRangeAsync(ICollection<T> entities);
    }
}