using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sacha.Models;
using System.IO;
using PagedList;

namespace sacha.Controllers
{
    public class SachesController : Controller
    {
        private SachDB db = new SachDB();

        // GET: Saches
        public ActionResult Index(string searchstring,string khoangdau,string khoangcuoi, string currentFilter,string ckd,string ckc, int? page)
        {
            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentFilter;
            }
            ViewBag.CurrentFilter = searchstring;

            if (khoangdau!=null && khoangcuoi != null)
            {
                page = 1;
            }
            else
            {
                khoangdau = ckd;
                khoangcuoi = ckc;
            }
            ViewBag.ckd = khoangdau;
            ViewBag.ckc = khoangcuoi;
            var saches = db.Saches.Include(s => s.DanhMuc).Include(s => s.NhaXuatBan);
            
            if(!String.IsNullOrEmpty(searchstring))
            {
                saches = db.Saches.Where(s => s.TenSach.Contains(searchstring));
            } 
            if(!String.IsNullOrEmpty(khoangdau) && !String.IsNullOrEmpty(khoangcuoi))
            {
                decimal kd = decimal.Parse(khoangdau);
                decimal kc = decimal.Parse(khoangcuoi);
                saches = db.Saches.Where(s => s.GiaBan >= kd && s.GiaBan <= kc);

            }
            saches = saches.OrderBy(m => m.MaSach);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(saches.ToPagedList(pageNumber,pageSize));
        }

        // GET: Saches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,AnhBia,NgayCapNhat,SoLuongTon,MaNXB,MaDM")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                sach.AnhBia = "";
                var f = Request.Files["ImageFile"];
               if(f!=null && f.ContentLength>0)
                        {
                    string fileName = System.IO.Path.GetFileName(f.FileName);
                    string uploadpath = Server.MapPath("~/assets/img/" + fileName);
                    f.SaveAs(uploadpath);
                    sach.AnhBia = fileName;
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sach.MaDM);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sach.MaDM);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,AnhBia,NgayCapNhat,SoLuongTon,MaNXB,MaDM")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                
                sach.AnhBia = "";
                var f = Request.Files["imagefile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string Uploadpath = Server.MapPath("~/assets/img/" + FileName);
                    f.SaveAs(Uploadpath);
                    sach.AnhBia = FileName;
                }

                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sach.MaDM);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
