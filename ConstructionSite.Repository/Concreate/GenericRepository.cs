using ConstructionSite.Helpers.Core;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
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
        #endregion
        #region --Ctor--
        public GenericRepository(DbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }
        #endregion

        #region --GetAll--
        public ICollection<T> GetAll()
        {
            return  _context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion
        #region --Added--
        public Result<T> Add(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                result.Data=entity;
               
            }
            catch 
            {
               result.IsResult=false;
                
            }
            return result;
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

                result.IsResult = false;
            }
            return result;
        }

        public Result<T> AddRange(ICollection<T> entity)
        {
            Result<T> result=new Result<T> { IsResult=true};
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

                result.IsResult = false;
            }
            return result;
        }

        public async Task<Result<T>> AddRangeAsync(ICollection<T> entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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
              result.IsResult=false;
            }
            return result;
        }
        #endregion
        #region --Update--
        public Result<T> Update(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
            if (entity==null)
            {
                result.IsResult=false;
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

                result.IsResult=false;
            }
            return result;
        }

        public async Task<Result<T>> UpdateAsync(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            if (entity==null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State=EntityState.Modified;
                await _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {

                _errorMessage+=Tools.WriteExeptions(ex);
                result.IsResult=false;
            }
            return result;
        }

        public Result<T> UpdateRange(ICollection<T> entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
            if (entity==null)
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

               result.IsResult=false;
            }
            return result;
        }

        public async Task<Result<T>> UpdateRangeAsync(ICollection<T> entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

                result.IsResult = false;
            }
            return result;
        }
        #endregion
        #region --Delete--
        public Result<T> Delete(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

               result.IsResult=false;
            }
            return result;
        }

        public async Task<Result<T>> DeleteAsync(T entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

               result.IsResult=false;
            }
            return result;
        }

        public Result<T> DeleteRange(ICollection<T> entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

               result.IsResult=false;
            }
            return result;
        }

        public async Task<Result<T>> DeleteRangeAsync(ICollection<T> entity)
        {
            Result<T> result = new Result<T> { IsResult = true };
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

                result.IsResult=false;
            }
            return result;
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
       
       




    }
}