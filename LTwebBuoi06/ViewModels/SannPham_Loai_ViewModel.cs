using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTwebBuoi06.Models;

namespace LTwebBuoi06.ViewModels
{
    public class SannPham_Loai_ViewModel
    {
        public List<SanPham> sanphams {  get; set; }
        public List<Loai> loais { get; set; }
    }
}