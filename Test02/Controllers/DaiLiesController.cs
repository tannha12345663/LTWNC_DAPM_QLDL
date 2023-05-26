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
    public class DaiLiesController : Controller
    {
        private QuanLyDLEntities2 db = new QuanLyDLEntities2();

        // GET: DaiLies
        public ActionResult Index()
        {
            var daiLies = db.DaiLies.Include(d => d.LoaiDL);
            return View(daiLies.ToList());
        }

        // GET: DaiLies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // GET: DaiLies/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiDL = new SelectList(db.LoaiDLs, "MaLoaiDL", "TenDaiLy");
            return View();
        }

        // POST: DaiLies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDL,MaLoaiDL,UserName,Password,TenDL,SDT,DiaChi,Email,NgayTao")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                db.DaiLies.Add(daiLy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiDL = new SelectList(db.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }

        // GET: DaiLies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiDL = new SelectList(db.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }

        // POST: DaiLies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDL,MaLoaiDL,UserName,Password,TenDL,SDT,DiaChi,Email,NgayTao")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daiLy).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiDL = new SelectList(db.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }

        // GET: DaiLies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // POST: DaiLies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DaiLy daiLy = db.DaiLies.Find(id);
            db.DaiLies.Remove(daiLy);
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
