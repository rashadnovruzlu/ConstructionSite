using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Abstract
{
   public interface IGenericRepository<T> where T:class
    {
        ICollection<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        T GetById(int id);

        Task<T> GetByIdAsync(int id);
    }
}
