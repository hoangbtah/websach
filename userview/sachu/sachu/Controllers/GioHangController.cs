using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachu.Models;

namespace sachu.Controllers
{
    public class GioHangController : Controller
    {
        private SachDB db = new SachDB();
        // GET: GioHang
      public List<GioHang> LayGioHang()
        {
            List<GioHang> dsmua = Session["giohang"] as List<GioHang>;
            if(dsmua == null)
            {
                dsmua = new List<GioHang>();
                Session["giohang"] = dsmua;
            }
            return dsmua;
        }
        public ActionResult ThemVaoGioHang( int masach,string url)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == masach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay session gio hang
            List<GioHang> dsmua = LayGioHang();
            //kiểm tra đã thêm sản phẩm này vào giỏ hàng lần nào chưa
            GioHang gh = dsmua.Find(n => n.GMaSach == masach);

            if (gh == null)
            {
                gh = new GioHang(masach);
                dsmua.Add(gh);
            }
           
            else
            {
                gh.GSoLuong++;
            }
            Session["giohang"] = dsmua;
            return Redirect(url);
        }
        //cập nhật giỏ hàng
        public ActionResult CapNhatGH(int masach)
        {
            //kiểm tra xem có sản phẩm này hay ko 
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //nêu ko thực hiện cái này
            List <GioHang> dsmua = LayGioHang();
            GioHang sanpham = dsmua.SingleOrDefault(n => n.GMaSach == masach);
            if(sanpham != null)
            {
                sanpham.GSoLuong++;
            }
            return RedirectToAction("XemGioHang");

        }
        public ActionResult XoaSanPham(int masach)
        {
            //kiểm tra xem có sản phẩm này hay ko 
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //nêu ko thực hiện cái này
            List<GioHang> dsmua = LayGioHang();
            GioHang sanpham = dsmua.SingleOrDefault(n => n.GMaSach == masach);
            if (sanpham != null)
            {
                dsmua.Remove(sanpham);
            }
            Session["giohang"] = dsmua;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XemGioHang()
        {
            //kiểm tra người dùng đăng nhập chưa nếu chưa bắt đăng nhập
            if (Session["Hoten"] == null)
            {
                return RedirectToAction("Login", "Access");
            }
            
            List<GioHang> dsmua = LayGioHang();
            return View(dsmua);
        }
        public int TongSoLuong()
        {
            int tongsoluong = 0;
            List<GioHang> dsmua = Session["giohang"] as List<GioHang>;
            if(dsmua != null)
            {
                tongsoluong = dsmua.Sum(n => n.GSoLuong);
            }
            return tongsoluong;
        }
        public double TongTien()
        {
            double tongtien = 0;
            List<GioHang> dsmua = Session["giohang"] as List<GioHang>;
            if (dsmua != null)
            {
                tongtien = dsmua.Sum(n => n.ThanhTien);
            }
            return tongtien;
        }
        public ActionResult DatHang()
        {
            DonHang dh = new DonHang();
            List<GioHang> gh = LayGioHang();
            KhachHang kh = (KhachHang)Session["taikhoan"];
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            //thêm đon hàng vào db
            db.DonHangs.Add(dh);
            db.SaveChanges();
            //thêm chi tiêt don hang
            foreach(var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = dh.MaDH;
                ctdh.MaSach = item.GMaSach;
                ctdh.SoLuong = item.GSoLuong;
                ctdh.DonGia = decimal.Parse(item.GDonGia.ToString());
                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            
            return RedirectToAction("Index","Home");

        }
    }
}