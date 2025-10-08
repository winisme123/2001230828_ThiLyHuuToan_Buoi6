using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Data;
namespace LTwebBuoi06.Models
{
    public class DuLieu
    {
        
        static string constr = ConfigurationManager.ConnectionStrings["ShoppingDB"].ConnectionString; 
        SqlConnection con = new SqlConnection(constr);
        public List<SanPham> dssp = new List<SanPham>();
        public List<Loai> dsloai = new List<Loai>();
        private string maLoai;

        public DuLieu()
        {
            ThietLapDSSP();
            ThietLap_LoaiSp();
            
        }
        public void ThietLapDSSP()
        {
            //cau truy van select
            //tao du lieu cho danh sach nhan vien
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_SanPham", con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            //ket qua thuc hien
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSanPham = int.Parse(dr["MaSanPham"].ToString());
                sp.TenSP = dr["TenSP"].ToString();
                sp.Gia = (float)Convert.ToDecimal(dr["Gia"]);
                sp.Hinh = dr["Hinh"].ToString();

                dssp.Add(sp);
            }
        }
        public void ThietLap_LoaiSp()
        {
            //cau truy van select
            //tao du lieu cho danh sach nhan vien
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Loai", con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            //ket qua thuc hien
            foreach (DataRow dr in dt.Rows)
            {
                var l = new Loai();
                l.MaLoai = int.Parse(dr["MaLoai"].ToString());
                l.TenLoai = dr["TenLoai"].ToString();

                dsloai.Add(l);
            }
        }

        public List<Loai> ThietLap_LoaiSp_WithProcedure()
        {
            List<Loai> LoaiSP = new List<Loai>();
            string sqlScript = "Pro_DanhSachLoaiSanPham";
            SqlCommand command = new SqlCommand(sqlScript, con);

            command.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sp =new Loai();
                        sp.MaLoai = Convert.ToInt32(reader["MaL"]);
                        sp.TenLoai = reader["TenLoai"].ToString();
                        LoaiSP.Add(sp);
                    }
                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return LoaiSP;
        }



        public List<SanPham> LoadSanPhamTheoMaLoai(int maloai)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_SanPham WHERE MaL = " + maLoai, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSanPham = Convert.ToInt32(dr["MaSanPham"]);
                sp.TenSP = dr["TenSP"].ToString();
                sp.Gia = (float)dr["Gia"];
                sp.Hinh = dr["Hinh"].ToString();

                sanPhams.Add(sp);
            }
            return sanPhams;
        }
        public SanPham ChiTietSanPham(int MaSP)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_SanPham WHERE MaSanPham =" + MaSP, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //ket qua thuc hien
           
                SanPham sanPham = new SanPham();
                sanPham.MaSanPham = int.Parse(dt.Rows[0]["MaSanPham"].ToString());
                sanPham.TenSP = dt.Rows[0]["TenSP"].ToString();              
                sanPham.Gia = (float)dt.Rows[0]["Gia"];
                sanPham.GhiChu = dt.Rows[0]["GhiChu"].ToString();
                sanPham.Hinh = dt.Rows[0]["Hinh"].ToString();
                return sanPham;
        }

        public List<SanPham> LocDanhSachSanPhamTheoLoai_WithProcedure(int maloai )
        {
            List<SanPham> dsSPTheoLoai = new List<SanPham>();

            string sqlScript = "Pro_LocDanhSachSanPhamTheoLoai";
            SqlCommand command = new SqlCommand(sqlScript, con);

            command.CommandType = CommandType.StoredProcedure;
            try
            {
                command.Parameters.Add(new SqlParameter("@MaLoai", maloai));
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sp = new SanPham();
                        sp.MaSanPham = Convert.ToInt32(reader["MaSanPham"]);
                        sp.TenSP = reader["TenSP"].ToString();
                        sp.GhiChu = reader["GhiChu"].ToString();
                        sp.Gia = (float)reader["Gia"];
                        sp.Hinh = reader["Hinh"].ToString();
                        sp.MaL = Convert.ToInt32(reader["MaL"]);
                        dsSPTheoLoai.Add(sp);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally {
                con.Close();
            }
            return dsSPTheoLoai;
        }
        public KhachHang Login(string phone, string password)
        {
            KhachHang khachHang = new KhachHang();
            string sqlString = String.Format("SELECT * FROM tbl_KhachHang WHERE SoDienThoai = '{0}' AND MatKhau = '{1}'", phone, password);
            SqlDataAdapter da = new SqlDataAdapter(sqlString, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                khachHang.MaKhachHang = Convert.ToInt32(dt.Rows[0]["MaKhachHang"]);
                khachHang.TenKhachHang = dt.Rows[0]["TenKhachHang"].ToString();
                khachHang.SoDienThoai = dt.Rows[0]["SoDienThoai"].ToString();
            }
            return khachHang;
        }
    }
}