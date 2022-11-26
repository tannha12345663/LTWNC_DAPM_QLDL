using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.Models;
using Test02.App_Start;

namespace Test02.Controllers
{
    [Authentication]
    public class KinhDoanhController : Controller
    {

        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: KinhDoanh
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult QuanLyDL()
        {

            return View(database.DaiLies.ToList().OrderByDescending(s=> s.NgayTao));
        }
        public ActionResult ThemDL()
        {
            ViewBag.MaLoaiDL = new SelectList(database.LoaiDLs, "MaLoaiDL", "TenDaiLy");
            return View();
        }
        public ActionResult ChinhSuaDL(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = database.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiDL = new SelectList(database.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaDL([Bind(Include = "MaDL,MaLoaiDL,UserName,Password,TenDL,SDT,DiaChi,Email,NgayTao")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                database.Entry(daiLy).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDL");
            }
            ViewBag.MaLoaiDL = new SelectList(database.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDL([Bind(Include = "MaLoaiDL,UserName,Password,TenDL,SDT,DiaChi,Email")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var madl = "DL" + rd.Next(1, 1000);
                daiLy.MaDL = madl;
                daiLy.NgayTao = System.DateTime.Now;
                database.DaiLies.Add(daiLy);
                database.SaveChanges();
                return RedirectToAction("QuanLyDL");
            }

            ViewBag.MaLoaiDL = new SelectList(database.LoaiDLs, "MaLoaiDL", "TenDaiLy", daiLy.MaLoaiDL);
            return View(daiLy);
        }
        // GET: DaiLies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = database.DaiLies.Find(id);
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
            DaiLy daiLy = database.DaiLies.Find(id);
            database.DaiLies.Remove(daiLy);
            database.SaveChanges();
            return RedirectToAction("QuanLyDL");
        }
        //Quản lý đơn hàng
        public ActionResult QuanLyDH()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.NgayLap));
        }
        //Thêm mới đơn hàng
        public ActionResult ThemDH()
        {
            ViewBag.MaDL = new SelectList(database.DaiLies, "MaDL", "MaDL");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDH([Bind(Include = "MaDH,MaDL,NgayLap,DiemGiao")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                var user = (Test02.Models.NhanVien)Session["user"];
                Random rd = new Random();
                var madh = "DH" + rd.Next(1, 1000);
                donHang.MaDH = madh;
                Session["tempdata"] = madh;
                Session["diemgiao"] = donHang.DiemGiao;
                donHang.NgayLap = System.DateTime.Now;
                donHang.MaNVLap = user.MaNV;
                donHang.TinhTrangThanhToan = "Chưa thanh toán";
                donHang.TrangThai = "Chưa xử lý";
                database.DonHangs.Add(donHang);
                database.SaveChanges();
                return RedirectToAction("ThemCTHD");
            }

            return View(donHang);
        }
        //Thêm mới chi tiết đơn hàng bán tự động
        public ActionResult ThemCTHD()
        {

            ViewBag.MaDH = new SelectList(database.DonHangs, "MaDH", "MaDL");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemCTHD([Bind(Include = "MaSP,SoLuong,DonGia,DiemGiao,DonViTinh")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                chiTietDonHang.MaDH = (string)Session["tempdata"];
                if (chiTietDonHang.ChietKhau ==null)
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (chiTietDonHang.DonGia);
                }
                else
                {
                    
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (chiTietDonHang.DonGia) * (chiTietDonHang.ChietKhau);
                }
                chiTietDonHang.DiemGiao = (string)Session["diemgiao"];
                database.ChiTietDonHangs.Add(chiTietDonHang);
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
                
            }

            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }
        //Chỉnh sửa chi tiết đơn hàng
        // GET: ChiTietDonHangs/Edit/5
        public ActionResult ChinhSuaCTDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var madh = database.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault();
            if (madh == null)
            {
                return HttpNotFound();
            }
            Session["MaCTDH"] = madh.MaCTDH;
            Session["madh"] = madh.MaDH;
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View(madh);
        }
        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaCTDH([Bind(Include = "MaSP,SoLuong,DonGia,DiemGiao,DonViTinh")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                chiTietDonHang.MaCTDH = (int)Session["MaCTDH"];
                if (chiTietDonHang.ChietKhau == null)
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (chiTietDonHang.DonGia);
                }
                else
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (chiTietDonHang.DonGia) * (chiTietDonHang.ChietKhau);
                }
                chiTietDonHang.MaDH = (string)Session["madh"];
                database.Entry(chiTietDonHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
            }
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }
        //Chỉnh sửa thông tin đơn hàng
        public ActionResult ChinhSuaDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonHang donHang = database.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaDH(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                database.Entry(donHang).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
            }
            return View(donHang);
        }
        //Xóa đơn hàng
        public ActionResult XoaDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonHang donHang = database.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }
        [HttpPost, ActionName("XoaDH")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaDHConfirm(string id)
        {
            DonHang donHang = database.DonHangs.Find(id);
            database.DonHangs.Remove(donHang);
            database.SaveChanges();
            return RedirectToAction("QuanLyDH");
        }


        public ActionResult QuanLySP()
        {
            return View(database.SanPhams.ToList().OrderByDescending(s=> s.NgaySX));
        }

        public ActionResult ThongTinSP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sanPham = database.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult ThemSP()
        {
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSP([Bind(Include = "TenSP,DonViTinh,Gia,HanSD,NgaySX")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var themSP = "SP" + rd.Next(1, 100);
                sanPham.MaSP = themSP;
                database.SanPhams.Add(sanPham);
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult ChinhSuaSP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sanPham = database.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaSP(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                database.Entry(sanPham).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult XoaSP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sanPham = database.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult xoasp(string id)
        {
            try
            {
                SanPham sanPham = database.SanPhams.Find(id);
                database.SanPhams.Remove(sanPham);
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }
            catch
            {
                return Content("Không xóa được dữ liệu");
            };

        }
        public ActionResult QuanLyHD()
        {
            return View(database.HoaDons.ToList().OrderByDescending(s=>s.TongTien));
        }
        public ActionResult QuanLyKho()
        {
            return View();
        }
        public ActionResult BaoCao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BaoCao(BaoCao baocao)
        {
            if (ModelState.IsValid)
            {
                database.BaoCaos.Add(baocao);
                database.SaveChanges();
                return RedirectToAction("TrangChu");
            }

            return View(baocao);
        }
        public void Hamrandom( )
        {

        }
    }
}