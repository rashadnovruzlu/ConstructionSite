﻿using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Galery
{
    public interface IGaleryFileFacde
    {
        Task<RESULT<GaleryFile>> Add(GaleryFileAddViewModel galeryFileAddViewModel);
        Task<RESULT<GaleryFile>> Delete(int id);
        Task<RESULT<GaleryFile>> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel);
        Task<IQueryable<GaleryFileViewModel>> GetAll(string _lang);




    }
}
