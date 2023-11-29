using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachu.Models;

namespace sachu.Controllers
{
    public class HomeController : Controller
    {
        private SachDB db = new SachDB();
        public ActionResult Index()
        {
            var sp = db.Saches.Select(m=>m);
         
            return View(sp);
        }

        public ActionResult ProductDetail(int ma)
        {
            var sach = db.Saches.Where(m => m.MaSach == ma);
            return View(sach);
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavPartial()
        {
            return PartialView();
        }
        public PartialViewResult DanhMuc()
        {
            var danhmuc = db.DanhMucs.Select(m => m);
            return PartialView(danhmuc);
        }
        public ActionResult XemSPTDM(int madm)
        {
            var sach = db.Saches.Where(m => m.MaDM == madm).ToList();
            return View(sach);
        }
    }
}