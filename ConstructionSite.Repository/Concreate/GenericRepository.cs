using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
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

        public ICollection<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<T>> GetAllAsync()
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
    }
}
