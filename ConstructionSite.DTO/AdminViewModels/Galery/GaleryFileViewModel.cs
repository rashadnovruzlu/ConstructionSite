using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.ViwModel.AdminViewModels.Galery
{
    public class GaleryFileViewModel
    {
        public int Id { get; set; }
        //public Galery Galery { get; set; }
        //public int GaleryId { get; set; }

        public Image Image { get; set; }
        public int ImageId { get; set; }
    }
}
