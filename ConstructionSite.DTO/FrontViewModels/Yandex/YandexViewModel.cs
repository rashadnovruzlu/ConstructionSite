using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.ViwModel.FrontViewModels.Yandex
{
    public class YandexViewModel
    {
        [UIHint("email")]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
