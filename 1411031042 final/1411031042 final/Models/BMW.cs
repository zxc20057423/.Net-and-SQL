using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1411031042_final.Models
{
    public class BMW
    {
        public int ID { get; set; }
        [DisplayName("車輛圖片")]
        public string Image { get; set; }
        [DisplayName("型號")]
        public string Model { get; set; }
        [DisplayName("價錢")]
        public int Money { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}