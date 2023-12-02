using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sacha.Models;

namespace sacha.Controllers
{
    public class HomeController : Controller
    {
        private SachDB db = new SachDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhMucPartial()
        {
            var danhmuc = db.DanhMucs.ToList();
            return PartialView(danhmuc);

        }
        public ActionResult XemSachByDM(int madm)
        {
            //var saches = db.Saches.Include(s => s.DanhMuc).Include(s => s.NhaXuatBan);
            var sach = db.Saches.Where(m => m.MaDM == madm);
            return View(sach.ToList());
        }


    }
}