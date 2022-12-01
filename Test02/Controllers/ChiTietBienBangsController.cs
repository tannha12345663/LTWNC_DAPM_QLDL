using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test02.Models;

namespace Test02.Controllers
{
    public class ChiTietBienBangsController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: ChiTietBienBangs
        public ActionResult Index()
        {
            var chiTietBienBangs = db.ChiTietBienBangs.Include(c => c.BienBangKiemKe);
            return View(chiTietBienBangs.ToList());
        }

        // GET: ChiTietBienBangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietBienBang chiTietBienBang = db.ChiTietBienBangs.Find(id);
            if (chiTietBienBang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietBienBang);
        }

        // GET: ChiTietBienBangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKK = new SelectList(db.BienBangKiemKes, "MaKK", "MaNVLap");
            return View();
        }

        // POST: ChiTietBienBangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTBB,MaKK,SLTonKho,SLThucTe,ChenhLech,LyDo")] ChiTietBienBang chiTietBienBang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietBienBangs.Add(chiTietBienBang);
                db.SaveChanges();
                return RedirectToAction("BaoCao");
            }

            ViewBag.MaKK = new SelectList(db.BienBangKiemKes, "MaKK", "MaNVLap", chiTietBienBang.MaKK);
            return View(chiTietBienBang);
        }

        // GET: ChiTietBienBangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietBienBang chiTietBienBang = db.ChiTietBienBangs.Find(id);
            if (chiTietBienBang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKK = new SelectList(db.BienBangKiemKes, "MaKK", "MaNVLap", chiTietBienBang.MaKK);
            return View(chiTietBienBang);
        }

        // POST: ChiTietBienBangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTBB,MaKK,SLTonKho,SLThucTe,ChenhLech,LyDo")] ChiTietBienBang chiTietBienBang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietBienBang).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKK = new SelectList(db.BienBangKiemKes, "MaKK", "MaNVLap", chiTietBienBang.MaKK);
            return View(chiTietBienBang);
        }

        // GET: ChiTietBienBangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietBienBang chiTietBienBang = db.ChiTietBienBangs.Find(id);
            if (chiTietBienBang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietBienBang);
        }

        // POST: ChiTietBienBangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietBienBang chiTietBienBang = db.ChiTietBienBangs.Find(id);
            db.ChiTietBienBangs.Remove(chiTietBienBang);
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
