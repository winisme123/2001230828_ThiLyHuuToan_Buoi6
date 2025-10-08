using LTwebBuoi06.Models;
using LTwebBuoi06.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace LTwebBuoi06.Controllers
{
    public class HomeController : Controller
    {
        DuLieu csdl = new DuLieu();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Detail()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login(string phone, string password)
        {
            KhachHang khachHang = new KhachHang();
            khachHang = csdl.Login(phone, password);
            int maKH = khachHang.MaKhachHang;
            if (khachHang.MaKhachHang != 0)
            {
                return Redirect("/Home/HienThiDanhSachSanPham?maLoai=-1");
            }
            else
            {
                ViewBag.Message = "FAILURE";
                return View();
            }
        }
        public ActionResult HienThiDanhSachSanPham(int maloai)
        {
            SannPham_Loai_ViewModel sannPham_Loai_ViewModel = new SannPham_Loai_ViewModel();

            // ✅ Sửa điều kiện: -1 = tất cả, còn lại lọc theo loại
            List<SanPham> sanpham = (maloai == -1) ? csdl.dssp : csdl.LoadSanPhamTheoMaLoai(maloai);
            List<Loai> loai = csdl.dsloai;

            sannPham_Loai_ViewModel.sanphams = sanpham;
            sannPham_Loai_ViewModel.loais = loai;

            return View(sannPham_Loai_ViewModel);
        }

        public ActionResult ChiTietSachSanPham(int maSanPham)
        {
            SanPham sanPham = csdl.ChiTietSanPham(maSanPham);
            return View(sanPham);
        }
    }
}