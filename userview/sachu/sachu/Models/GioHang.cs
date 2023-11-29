using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sachu.Models;

namespace sachu.Models
{   
    
    public class GioHang
    {
        SachDB db = new SachDB();
        public int GMaSach { get; set; }
        public string GTenSach { get; set; }
        public string GHinhAnh { get; set; }
        public int GSoLuong { get; set; }
        public double GDonGia { get; set; }
        public double ThanhTien
        {
            get { return GSoLuong * GDonGia; } 
        }
        public GioHang(int masach)
        {
            GMaSach = masach;
            Sach sach = db.Saches.Single(n => n.MaSach == masach);
            GTenSach = sach.TenSach;
            GSoLuong = 1;
            GDonGia = double.Parse(sach.GiaBan.ToString());
            GHinhAnh = sach.AnhBia;


        }
        public GioHang()
        {

        }
       
    }
}