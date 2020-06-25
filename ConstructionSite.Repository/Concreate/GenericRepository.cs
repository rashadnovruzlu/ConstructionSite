﻿using ConstructionSite.Helpers.Core;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region --Fild--

        private readonly DbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private string _errorMessage = string.Empty;

        #endregion --Fild--

        #region --Ctor--

        public GenericRepository(DbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        #endregion --Ctor--

        #region --GetAll--

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        #endregion --GetAll--

        #region --Added--

        public RESULT<T> Add(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                result.Data = entity;
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> AddAsync(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _unitOfWork.Commit();
                result.Data = entity;
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public RESULT<T> AddRange(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().AddRange(entity);
                _context.SaveChanges();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> AddRangeAsync(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                await _context.Set<T>().AddRangeAsync(entity);
                await _unitOfWork.Commit();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        #endregion --Added--

        #region --Update--

        public RESULT<T> Update(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                result.IsDone = false;
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> UpdateAsync(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                _errorMessage += Tools.WriteExeptions(ex);
                result.IsDone = false;
            }
            return result;
        }

        public RESULT<T> UpdateRange(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                foreach (var item in entity)
                {
                    _context.Set<T>().Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChangesAsync();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> UpdateRangeAsync(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                foreach (var item in entity)
                {
                    _context.Set<T>().Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                }
                await _unitOfWork.Commit();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        #endregion --Update--

        #region --Delete--

        public RESULT<T> Delete(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> DeleteAsync(T entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Remove(entity);
                await _unitOfWork.Commit();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public RESULT<T> DeleteRange(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().RemoveRange(entity);
                _context.SaveChanges();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        public async Task<RESULT<T>> DeleteRangeAsync(ICollection<T> entity)
        {
            RESULT<T> result = new RESULT<T> { IsDone = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().RemoveRange(entity);

                await _unitOfWork.Commit();
            }
            catch
            {
                result.IsDone = false;
            }
            return result;
        }

        #endregion --Delete--

        #region --Search--

        public T Find(Expression<Func<T, bool>> predecat)
        {
            return _context.Set<T>().SingleOrDefault(predecat);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> predecat)
        {
            return _context.Set<T>().Where(predecat).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predecat)
        {
            return await _context.Set<T>().Where(predecat).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predecat)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predecat);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        #endregion --Search--
    }
}