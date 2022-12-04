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
    public class PhieuNhapXuatsController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: PhieuNhapXuats
        public ActionResult Index()
        {
            var phieuNhapXuats = db.PhieuNhapXuats.Include(p => p.Kho);
            return View(phieuNhapXuats.ToList());
        }

        // GET: PhieuNhapXuats/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = db.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            return View();
        }

        // POST: PhieuNhapXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                db.PhieuNhapXuats.Add(phieuNhapXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = db.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuNhapXuat).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = db.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuNhapXuat phieuNhapXuat = db.PhieuNhapXuats.Find(id);
            db.PhieuNhapXuats.Remove(phieuNhapXuat);
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
