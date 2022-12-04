﻿using System;
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
                if (chiTietKho.SoLuong <= 100 && chiTietKho.SoLuong > 0)
                {
                    chiTietKho.TinhTrang = "Sắp hết hàng";
                }
                else if (chiTietKho.SoLuong > 100 && chiTietKho.SoLuong < 1000)
                {
                    chiTietKho.TinhTrang = "Còn hàng";
                }
                //var ktra = (System.DateTime.Now - chiTietKho.NgayXuat);
                else if(chiTietKho.SoLuong >= 1000)
                {
                    chiTietKho.TinhTrang = "Tồn kho";
                }
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
                if (chiTietKho.SoLuong <= 100 && chiTietKho.SoLuong > 0)
                {
                    chiTietKho.TinhTrang = "Sắp hết hàng";
                }
                else if (chiTietKho.SoLuong > 100 && chiTietKho.SoLuong < 1000)
                {
                    chiTietKho.TinhTrang = "Còn hàng";
                }
                //var ktra = (System.DateTime.Now - chiTietKho.NgayXuat);
                else if (chiTietKho.SoLuong >= 1000)
                {
                    chiTietKho.TinhTrang = "Tồn kho";
                }
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
        //-----------------------------------------------------------------------
        public ActionResult NhapKho()
        {
            return View(database.PhieuNhapXuats.ToList().OrderByDescending(s=> s.NgayLap));
        }
        //----------------------------------------------------------------------
        // GET: PhieuNhapXuats/Create
        public ActionResult TaoPhieuNhapKho()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        // POST: PhieuNhapXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoPhieuNhapKho([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var maphieu = "PN" + rd.Next(1, 1000);
                phieuNhapXuat.MaPhieu = maphieu;
                Session["phieunhap"] = maphieu;
                phieuNhapXuat.NgayLap = System.DateTime.Now;
                phieuNhapXuat.LoaiPhieu = "Phiếu nhập";
                Session["KinhGui"] = " ";
                Session["HoTen"] = " ";
                database.PhieuNhapXuats.Add(phieuNhapXuat);
                database.SaveChanges();
                return RedirectToAction("QuanLyDL");
            }

            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Details/5
        public ActionResult ChiTietPhieuNhapKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["maPhieuNhap"] = id;
            Session["KinhGui"] = "Trưởng phòng kinh doanh";
            Session["HoTen"] = "Nguyễn Thị Diễm Khang";
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }
        // GET: PhieuNhapXuats/Delete/5
        public ActionResult XoaPhieuNhapKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("XoaPhieuNhapKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaPhieuNhapKhoConfirmed(string id)
        {
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            database.PhieuNhapXuats.Remove(phieuNhapXuat);
            database.SaveChanges();
            return RedirectToAction("NhapKho");
        }
        //-------------------------------------Hết phần Phiếu Nhập----------------------
        public ActionResult XuatKho()
        {
            return View();
        }
        public ActionResult TaoPhieuXuatKho()
        {
            return View();
        }
        //-----------------------------Bắt đầu phiếu xuất - tồn kho------------------------------------
        public ActionResult TonKho()
        {
            return View();
        }
        
        // GET: PhieuNhapXuats/Create
        public ActionResult GiaiQuyetTonKho()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }
        
        // POST: PhieuNhapXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiaiQuyetTonKho([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var maphieux = "PX" + rd.Next(1, 1000);
                phieuNhapXuat.MaPhieu = maphieux;
                Session["phieuXuat"] = maphieux;
                phieuNhapXuat.NgayLap = System.DateTime.Now;
                phieuNhapXuat.LoaiPhieu = "Phiếu xuất";
                Session["KinhGui"] = " ";
                Session["HoTen"] = " ";
                database.PhieuNhapXuats.Add(phieuNhapXuat);
                database.SaveChanges();
                return RedirectToAction("TonKho");
            }

            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Details/5
        public ActionResult ChiTietPhieuXuatKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["maPhieuXuat"] = id;
            Session["KinhGui"] = "Trưởng phòng kinh doanh";
            Session["HoTen"] = "Nguyễn Thị Diễm Khang";
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }
        // GET: PhieuNhapXuats/Delete/5
        public ActionResult XoaPhieuXuatKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("XoaPhieuXuatKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaPhieuXuatKhoConfirmed(string id)
        {
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            database.PhieuNhapXuats.Remove(phieuNhapXuat);
            database.SaveChanges();
            return RedirectToAction("NhapKho");
        }
        //------------------------------------------Hết phần tồn kho - phiếu xuất---------------------------
        public ActionResult BaoCao()
        {
            //var bienBangKiemKes = database.BienBangKiemKes.Include(b => b.NhanVien).Include(b => b.SanPham);
            return View(database.BienBangKiemKes.ToList().OrderByDescending(s => s.NgayLap));
        }

        //----------------------------------------------------------
        // GET: BienBangKiemKes/Create
        public ActionResult CreateBBKK()
        {
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaChucVu");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: BienBangKiemKes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBBKK([Bind(Include = "MaKK,MaNVLap,MaSP,TenNV,TenBienBang,NgayLap")] BienBangKiemKe bienBangKiemKe)
        {
            if (ModelState.IsValid)
            {
                DataTable bb = new DataTable();
                if (Session["BBKK"] == null)
                {
                    bb.Columns.Add("MaKK");
                    bb.Columns.Add("MaSP");
                    bb.Columns.Add("MaNVLap");
                    bb.Columns.Add("NgayLap");
                    bb.Columns.Add("TenBB");
                    bb.Columns.Add("TenNV");
                    //Tạo xong lưu lại Session
                    Session["BBKK"] = bb;
                }
                else
                {
                    //Lấy thông tin từ Session
                    bb = Session["BBKK"] as DataTable;
                }
                Random rd = new Random();
                var mabb = "BB" + rd.Next(1, 1000);
                bienBangKiemKe.MaKK = mabb;
                Session["tempdata"] = mabb;
                bienBangKiemKe.NgayLap = System.DateTime.Now;
                DataRow dr = bb.NewRow();
                dr["MaKK"] = bienBangKiemKe.MaKK;
                dr["MaNVLap"] = bienBangKiemKe.MaNVLap;
                dr["MaSP"] = bienBangKiemKe.MaSP;
                dr["TenNV"] = bienBangKiemKe.TenNV;
                dr["TenBB"] = bienBangKiemKe.TenBienBang;
                dr["NgayLap"] = bienBangKiemKe.NgayLap;
                bb.Rows.Add(dr);
                //database.BienBangKiemKes.Add(bienBangKiemKe);
                //database.SaveChanges();
                return RedirectToAction("CreateCTBBKK");
            }

            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "TenNV", bienBangKiemKe.MaNVLap);
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", bienBangKiemKe.MaSP);
            return View(bienBangKiemKe);
        }




        // GET: ChiTietBienBangs/Create
        public ActionResult CreateCTBBKK()
        {
            ViewBag.MaKK = new SelectList(database.BienBangKiemKes, "MaKK", "MaNVLap");
            return View();
        }

        // POST: ChiTietBienBangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCTBBKK([Bind(Include = "MaCTBB,MaKK,SLTonKho,SLThucTe,ChenhLech,LyDo")] ChiTietBienBang chiTietBienBang)
        {
            if (ModelState.IsValid)
            {
                chiTietBienBang.MaKK = (string)Session["tempdata"];
                Random rd = new Random();
                var mactbb = "CTBB" + rd.Next(1, 1000);
                chiTietBienBang.MaCTBB = mactbb;
                chiTietBienBang.ChenhLech = (int)chiTietBienBang.SLTonKho - (int)chiTietBienBang.SLThucTe;
                BienBangKiemKe bienBangKiemKe = new BienBangKiemKe();
                
                DataTable bb = Session["BBKK"] as DataTable;
                
                foreach (DataRow dr in bb.Rows)
                {
                    bienBangKiemKe.MaKK = dr["MaKK"].ToString();
                    bienBangKiemKe.MaSP = dr["MaSP"].ToString();
                    bienBangKiemKe.MaNVLap = dr["MaNVLap"].ToString();
                    var date = Convert.ToDateTime(dr["NgayLap"]);
                    bienBangKiemKe.NgayLap = date;
                    bienBangKiemKe.TenBienBang = dr["TenBB"].ToString();
                    bienBangKiemKe.TenNV = dr["TenNV"].ToString();
                }
                database.BienBangKiemKes.Add(bienBangKiemKe);
                database.SaveChanges();
                database.ChiTietBienBangs.Add(chiTietBienBang);
                database.SaveChanges();
                return RedirectToAction("BaoCao");
            }
            ViewBag.MaKK = new SelectList(database.BienBangKiemKes, "MaKK", "MaNVLap", chiTietBienBang.MaKK);
            return View(chiTietBienBang);
        }

        // GET: BienBangKiemKes/Delete/5
        public ActionResult DeleteBBKK(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            return View(bienBangKiemKe);
        }

        // POST: BienBangKiemKes/Delete/5
        [HttpPost, ActionName("DeleteBBKK")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBBKKConfirmed(string id)
        {
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            database.BienBangKiemKes.Remove(bienBangKiemKe);
            database.SaveChanges();
            return RedirectToAction("BaoCao");
        }

        // GET: ChiTietBienBangs/Details/5
        public ActionResult DetailsBB(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["makk"] = id;
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            return View(bienBangKiemKe);
            //ChiTietBienBang chiTietBienBang = database.ChiTietBienBangs.Find(id);
            //if (chiTietBienBang == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(chiTietBienBang);
        }
    }
}