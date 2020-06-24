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

        #endregion ---List---
        #region --Added--

        Result<T> Add(T entity);

        Task<Result<T>> AddAsync(T entity);

        Task<Result<T>> AddRangeAsync(ICollection<T> entity);

        Result<T> AddRange(ICollection<T> entity);

        #endregion --Added--
        #region --Delete--

        Result<T> Delete(T entity);

        Task<Result<T>> DeleteAsync(T entity);

        Result<T> DeleteRange(ICollection<T> entity);

        Task<Result<T>> DeleteRangeAsync(ICollection<T> entity);

        #endregion --Delete--
        #region --Update--

        Result<T> Update(T entity);

        Result<T> UpdateRange(ICollection<T> entity);

        Task<Result<T>> UpdateAsync(T entity);

        Task<Result<T>> UpdateRangeAsync(ICollection<T> entity);

        #endregion --Update--
        #region --Seach--

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        T Find(Expression<Func<T, bool>> predecat);

        Task<T> FindAsync(Expression<Func<T, bool>> predecat);

        ICollection<T> FindAll(Expression<Func<T, bool>> predecat);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predecat);

        #endregion --Seach--

       

       
      
    }
}