using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [Authentication (MaChucVu ="QTHT")]
    public class QTVController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: QTV
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QLNhanVien()
        {
            return View(database.NhanViens.ToList().OrderByDescending(s => s.MaNV));
        }

        public ActionResult ThemNV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNV(NhanVien nhanVien)
        {
            Random rd = new Random();
            var manv = "NV" + rd.Next(1, 1000);
            nhanVien.MaNV = manv;
            
            database.NhanViens.Add(nhanVien);
            database.SaveChanges();
            return RedirectToAction("QLNhanVien");
        }

        public ActionResult ChinhsuaNV(String id)
        {
            return View(database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhsuaNV(String id,NhanVien nhanVien)
        {
            database.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QLNhanVien");
        }

        public ActionResult XoaNV(String id)
        {
            return View(database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaNV(String id,NhanVien nhanvien)
        {
            nhanvien = database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault();
            database.NhanViens.Remove(nhanvien);
            database.SaveChanges();
            return RedirectToAction("QLNhanVien");
        }

    }
}