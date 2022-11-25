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
    public class DonHangsController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: DonHangs
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.DaiLy).Include(d => d.NhanVien);
            return View(donHangs.ToList());
        }

        // GET: DonHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDL = new SelectList(db.DaiLies, "MaDL", "MaLoaiDL");
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaDL,MaNVLap,NgayLap,TrangThai,TinhTrangThanhToan,DiemGiao")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDL = new SelectList(db.DaiLies, "MaDL", "MaLoaiDL", donHang.MaDL);
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", donHang.MaNVLap);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDL = new SelectList(db.DaiLies, "MaDL", "MaLoaiDL", donHang.MaDL);
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", donHang.MaNVLap);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaDL,MaNVLap,NgayLap,TrangThai,TinhTrangThanhToan,DiemGiao")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDL = new SelectList(db.DaiLies, "MaDL", "MaLoaiDL", donHang.MaDL);
            ViewBag.MaNVLap = new SelectList(db.NhanViens, "MaNV", "MaChucVu", donHang.MaNVLap);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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
