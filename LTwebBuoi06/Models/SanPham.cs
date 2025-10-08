using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTwebBuoi06.Models
{
    public class SanPham
    {
        public int MaSanPham { get; set; }
        public string TenSP { get; set; }
        public int MaL
        {
            get; set;
        }
        public int MaSX { get; set; }
        public float Gia { get; set; }
        public string GhiChu { get; set; }
        public string Hinh { get; set; }
    }
}