using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachu.Models;

namespace sachu.Controllers
{
    public class AccessController : Controller
    {
        private SachDB db = new SachDB();
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
       
        public ActionResult Register()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaKH,HoTen,DiaChi,Email,SoDT,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(khachHang);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string matkhau)
        {
            if (ModelState.IsValid)
            {
                KhachHang user = db.KhachHangs.SingleOrDefault(m => m.Email.Equals(email) &&
                m.MatKhau.Equals(matkhau));
                if (user != null)
                {
                    Session["Hoten"] = user.HoTen;
                    Session["Emial"] = user.Email;
                    Session["IdUser"] = user.MaKH;
                    Session["taikhoan"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu";
                }
                 
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }
}