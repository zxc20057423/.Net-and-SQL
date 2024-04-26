using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1411031042_final.Models;
using _1411031042_final.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1411031042_final.ViewModels
{
    public class CARViewModels
    {
        public List<Benz> BenzList { get; set; }
        public List<BMW> BMWList { get; set; }
    }
}