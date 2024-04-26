using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1411031042_final.ViewModels
{
    public class BMWViewModel
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public int Money { get; set; }

        // 新圖片的屬性
        public HttpPostedFileBase NewImage { get; set; }
    }
}