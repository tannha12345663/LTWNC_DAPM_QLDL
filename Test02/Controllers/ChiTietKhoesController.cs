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
    public class ChiTietKhoesController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: ChiTietKhoes
        public ActionResult ChiTietKho()
        {
            var chiTietKhoes = db.ChiTietKhoes.Include(c => c.Kho).Include(c => c.SanPham);
            return View(chiTietKhoes.ToList());
        }

        // GET: ChiTietKhoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKho chiTietKho = db.ChiTietKhoes.Find(id);
            if (chiTietKho == null)
            {
                return HttpNotFound();
            }
            return View(chiTietKho);
        }

        // GET: ChiTietKhoes/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietKhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietKhoes.Add(chiTietKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietKho.MaKho);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietKho.MaSP);
            return View(chiTietKho);
        }

        // GET: ChiTietKhoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKho chiTietKho = db.ChiTietKhoes.Find(id);
            if (chiTietKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietKho.MaKho);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietKho.MaSP);
            return View(chiTietKho);
        }

        // POST: ChiTietKhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietKho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietKho.MaKho);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietKho.MaSP);
            return View(chiTietKho);
        }

        // GET: ChiTietKhoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKho chiTietKho = db.ChiTietKhoes.Find(id);
            if (chiTietKho == null)
            {
                return HttpNotFound();
            }
            return View(chiTietKho);
        }

        // POST: ChiTietKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietKho chiTietKho = db.ChiTietKhoes.Find(id);
            db.ChiTietKhoes.Remove(chiTietKho);
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
