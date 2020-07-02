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

        int Add(T entity);

        Task<int> AddAsync(T entity);

        Task<RESULT<T>> AddRangeAsync(ICollection<T> entity);

        RESULT<T> AddRange(ICollection<T> entity);

        #endregion --Added--

        #region --Delete--

        RESULT<T> Delete(T entity);

        Task<RESULT<T>> DeleteAsync(T entity);

        RESULT<T> DeleteRange(ICollection<T> entity);

        Task<RESULT<T>> DeleteRangeAsync(ICollection<T> entity);

        #endregion --Delete--

        #region --Update--

        RESULT<T> Update(T entity);

        RESULT<T> UpdateRange(ICollection<T> entity);

        Task<RESULT<T>> UpdateAsync(T entity);

        Task<RESULT<T>> UpdateRangeAsync(ICollection<T> entity);

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