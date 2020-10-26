using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.News
{
   public interface INewsFacade
    {

        Task<bool> Delete(int id);
    }
}
