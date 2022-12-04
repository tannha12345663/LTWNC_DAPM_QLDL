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
    public class BienBangKiemKesController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: BienBangKiemKes
        public ActionResult Index()
        {
            var bienBangKiemKes = db.BienBangKiemKes.Include(b => b.NhanVien).Include(b => b.SanPham);
            return View(bienBangKiemKes.ToList());
        }

        // GET: BienBangKiemKes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienBangKiemKe bienBangKiemKe = db.BienBangKiemKes.Find(id);
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            return View(bienBangKiemKe);
        }

        // GET: BienBangKiemKes/Create
        public ActionResult Create()
        {
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: BienBangKiemKes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKK,MaNVLap,MaSP,TenNV,TenBienBang,NgayLap")] BienBangKiemKe bienBangKiemKe)
        {
            if (ModelState.IsValid)
            {
                db.BienBangKiemKes.Add(bienBangKiemKe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", bienBangKiemKe.MaNVLap);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienBangKiemKe.MaSP);
            return View(bienBangKiemKe);
        }

        // GET: BienBangKiemKes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienBangKiemKe bienBangKiemKe = db.BienBangKiemKes.Find(id);
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", bienBangKiemKe.MaNVLap);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienBangKiemKe.MaSP);
            return View(bienBangKiemKe);
        }

        // POST: BienBangKiemKes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKK,MaNVLap,MaSP,TenNV,TenBienBang,NgayLap")] BienBangKiemKe bienBangKiemKe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bienBangKiemKe).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", bienBangKiemKe.MaNVLap);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienBangKiemKe.MaSP);
            return View(bienBangKiemKe);
        }

        // GET: BienBangKiemKes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienBangKiemKe bienBangKiemKe = db.BienBangKiemKes.Find(id);
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            return View(bienBangKiemKe);
        }

        // POST: BienBangKiemKes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BienBangKiemKe bienBangKiemKe = db.BienBangKiemKes.Find(id);
            db.BienBangKiemKes.Remove(bienBangKiemKe);
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
