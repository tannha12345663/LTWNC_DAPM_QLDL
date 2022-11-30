using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [Authentication]
    
    public class PhongKhoController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: PhongKho
        public ActionResult Index()
        {
            return View();
        }

    // Quản lý kho

        public ActionResult QuanLyKho()
        {
            return View(database.Khoes.ToList().OrderByDescending(s => s.TenKho));
        }
        // GET: Khoes/Details/5
        //----------------------------------------------------------------------------------
        // Chi tiết kho
        public ActionResult ChiTietKho(string id)
        {
            TempData["makho"] = id;
            return View(database.ChiTietKhoes.ToList().Where(s => s.MaKho == id));
        }
        // GET: ChiTietKhoes/Create
        public ActionResult CreateCTKho(string id)
        {
            Session["makho1"] = id;
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho" );
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        // POST: ChiTietKhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCTKho([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {
            
            if (ModelState.IsValid)
            {
                chiTietKho.MaKho = (string)Session["makho1"];
                Random rd = new Random();
                var mactk = "CTK" + rd.Next(1, 1000);
                chiTietKho.MaCTKho = mactk;
                database.ChiTietKhoes.Add(chiTietKho);
                database.SaveChanges();
                return RedirectToAction("QuanLyKho");
            }
            return View();
        }

        // GET: ChiTietKhoes/Edit/5
        public ActionResult EditCTKho(string id)
        {
            TempData["mactk"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mactk = database.ChiTietKhoes.Where(s => s.MaCTKho == id).FirstOrDefault();
            //ChiTietKho chiTietKho = database.ChiTietKhoes.Find(id);
            if (mactk == null)
            {
                return HttpNotFound();
            }
            Session["Mactkho"] = mactk.MaCTKho;
            Session["Makho"] = mactk.MaKho;

            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View(mactk);
        }

        // POST: ChiTietKhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCTKho([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {
            if (ModelState.IsValid)
            {
                chiTietKho.MaCTKho = (string)Session["Mactkho"];
                chiTietKho.MaKho = (string)Session["Makho"];
                database.Entry(chiTietKho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyKho");
            }
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", chiTietKho.MaKho);
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietKho.MaSP);
            return View(chiTietKho);
        }

        // GET: ChiTietKhoes/Delete/5
        public ActionResult DeleteCTKho(string id)
        {
            var mactk = database.ChiTietKhoes.Where(s => s.MaCTKho == id).FirstOrDefault();
            //ChiTietKho chiTietKho = database.ChiTietKhoes.Find(id);
            if (mactk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(mactk);
        }

        // POST: ChiTietKhoes/Delete/5
        [HttpPost, ActionName("DeleteCTKho")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCTKhoConfirmed(string id)
        {
            var mactk = database.ChiTietKhoes.Where(s => s.MaCTKho == id).FirstOrDefault();
            database.ChiTietKhoes.Remove(mactk);
            database.SaveChanges();
            return RedirectToAction("QuanLyKho");
        }

        //-----------------------------------------------------------------------------------
        // GET: Khoes/Create
        public ActionResult Themkho()
        {
            return View();
        }

        // POST: Khoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKho([Bind(Include = "MaKho,TenKho,DiaChi")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var makho = "KO" + rd.Next(1, 1000);
                kho.MaKho = makho;
                database.Khoes.Add(kho);
                database.SaveChanges();
                return RedirectToAction("QuanLyKho");
            }
            return View(kho);
        }

        // GET: Khoes/Edit/5
        
        public ActionResult ChinhSuaKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = database.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            return View(kho);
        }

        // POST: Khoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaKho([Bind(Include = "MaKho,TenKho,DiaChi")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                database.Entry(kho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyKho");
            }
            return View(kho);
        }

        // GET: Khoes/Delete/5
        public ActionResult XoaKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = database.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            return View(kho);
        }

        // POST: Khoes/Delete/5
        [HttpPost, ActionName("XoaKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaKhoConfirmed(string id)
        {
            Kho kho = database.Khoes.Find(id);
            database.Khoes.Remove(kho);
            database.SaveChanges();
            return RedirectToAction("QuanLyKho");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    


        public ActionResult Test02()
        {
            return View();
        }
        public ActionResult QuanLyDL()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        public ActionResult ThemSanPham()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }
        public ActionResult NhapKho()
        {
            return View();
        }
        public ActionResult TaoPhieuNhapKho()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        public ActionResult XuatKho()
        {
            return View();
        }
        public ActionResult TaoPhieuXuatKho()
        {
            return View();
        }
        public ActionResult TonKho()
        {
            return View();
        }
        public ActionResult GiaiQuyetTonKho()
        {
            return View();
        }
        public ActionResult BaoCao()
        {
            return View();
        }
    }
}