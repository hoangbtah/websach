using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sacha.Models;
using PagedList;

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
        [Route("sach/sachbyca/{madm}")]
        public ActionResult XemSachByDM(int madm,int? page)
        {
            
          
            var sach = db.Saches.OrderBy(m=>m.MaDM).Where(m => m.MaDM == madm);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(sach.ToPagedList(pageNumber,pageSize));
        }


    }
}