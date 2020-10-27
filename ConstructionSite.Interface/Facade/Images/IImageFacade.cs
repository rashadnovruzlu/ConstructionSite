using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Images
{
    public interface IImageFacade
    {
        Task<RESULT<Image>> Update(Image image);

    }
}
