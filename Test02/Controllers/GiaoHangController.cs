using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.Models;
using Test02.App_Start;
using System.Data;
using System.Net;
using System.Web.Routing;

namespace Test02.Controllers
{

    [Authentication(MaChucVu = "NVGH")]
    public class GiaoHangController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: GiaoHang


        //------------------------------------------DANH SACH DON HANG MOI CAN XET DUYET GIAO-------------------------------------------------------------
        public ActionResult DonHangMoi()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }


        //Hien thi chi tiet don giao hang
        public ActionResult ChiTietDonGiaoHang(string id)
        {
             return View(database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));
        }

       //Hien thi danh sach don hang giao
        public ActionResult DonGiaoHang()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Hien thi danh sach don giao hang
        public ActionResult DonDangGiao()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }


        //Hien thi danh sach don da giao
        public ActionResult DonDaGiao()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Danh sach don hang hoan
        public ActionResult DonHangHoan()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //-------------------------------------VIEW IN PHIEU XUAT KHO--------------------------------------------------
        public ActionResult InPhieuXuatKho(string id)
        {
            return View(database.PhieuNhapXuats.Where(s => s.MaPhieu == id));
        }



        //--------------------------LAP DANH SACH DON HANG GIAO(gom nhieu don hang)------------------------------------
        public ActionResult DanhSachDonHangGiao()
        {
            TempData["ngaylapdgh"] = System.DateTime.Now;


            return View(database.DonHangGiaos.ToList().OrderByDescending(s => s.MaGH));
        }

        //Xet duyet don giao hang
        public ActionResult XetDuyetDonGiaoHang(string id)
        {
            //TempData["madhxd"] = id;
            TempData["ngayduyet"] = System.DateTime.Now;
            var maDH = id;
            foreach (var item in database.DonHangs)
            {
                if (maDH == item.MaDH)
                {
                    TempData["tenDL"] = item.DaiLy.TenDL;
                    TempData["diaDiemGiao"] = item.DiemGiao;
                }
            }
            ViewBag.MaKho = new SelectList(database.Khoes, "TenKho", "MaKho");
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaChucVu");
            ViewBag.MaHD = new SelectList(database.DonHangs, "MaDH", "MaDH");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XetDuyetDonGiaoHang([Bind(Include = "MaGH,MaDH,MaNV,TenNV,DiemGiao,TrangThai,NgayGiao,TienThu,XacNhan,MaPT,NgayXetDuyet,MaKho")] DonHangGiao dsxetduyetgiaohang, string tenkho, string shipper, string phuongtiengiao)
        {
            if (ModelState.IsValid)
            {

                Random rd = new Random();
                var maGH = "MGH" + rd.Next(1, 1000);
                dsxetduyetgiaohang.MaGH = maGH;
                dsxetduyetgiaohang.NgayLap = System.DateTime.Now;
                dsxetduyetgiaohang.MaNV = shipper;
                dsxetduyetgiaohang.MaPT = phuongtiengiao;
                dsxetduyetgiaohang.MaDH = (string)Session["madhxd"];
                var user = (Test02.Models.NhanVien)HttpContext.Session["user"];
                dsxetduyetgiaohang.MaNV = user.MaNV;

                if (shipper != "----Chọn nhân viên giao hàng----")
                {
                    TempData["shipper"] = shipper;
                    database.DonHangGiaos.Add(dsxetduyetgiaohang);
                    database.SaveChanges();
                    TempData["AlertMessage"] = "Duyệt thành công";
                    TempData["maGH"] = maGH;
                    return RedirectToAction("DonGiaoHang", new RouteValueDictionary(new { controller = "GiaoHang", action = "XetDuyetDonGiaoHang", Id = Session["madhxd"] }));
                }

                else if (phuongtiengiao != "----Chọn phương tiện----")
                {
                    TempData["phuongtien"] = phuongtiengiao;
                    database.DonHangGiaos.Add(dsxetduyetgiaohang);
                    database.SaveChanges();
                    TempData["AlertMessage"] = "Duyệt thành công";
                    TempData["maGH"] = maGH;
                    return RedirectToAction("DonGiaoHang", new RouteValueDictionary(new { controller = "GiaoHang", action = "XetDuyetDonGiaoHang", Id = Session["madhxd"] }));
                }
                else
                {
                    TempData["AlertMessage"] = "Chọn thiếu dữ kiện duyệt đơn!";
                    return View(dsxetduyetgiaohang);
                }
                
            }

            ViewBag.MaGH = new SelectList(database.Khoes, "MaGH", "MaNV", dsxetduyetgiaohang.MaGH);
            return View(dsxetduyetgiaohang);
        }

        //--------------------------------------------CAP NHAT TRANG THAI DON HANG---------------------------------------------------
        public ActionResult CapNhatTrangThaiDonHang(string id)
        {
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult CapNhatTrangThaiDonHang(String id, DonHang donHang)
        {
            database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DonGiaoHang");
        }



        //----------------------------------------------------DON HANG HOAN------------------------------------------------------------
        public ActionResult DanhSachDonHoan()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Bao cao ly do hoan don
        public ActionResult BaoCaoLyDoHoanDon()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult BaoCaoPhongGiaoHang()
        {
            return View();
        }


        //---------------------------------------------BAO CAO PHONG GIAO HANG-------------------------------------------------------
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BaoCaoPhongGiaoHang(BaoCao baocao)
        {
            if (ModelState.IsValid)
            {

                Random rd = new Random();
                var mabc = "BC" + rd.Next(1, 1000);
                baocao.MaBC = mabc;
                var manv = (Test02.Models.NhanVien)Session["user"];
                baocao.MaNV = manv.MaNV;
                database.BaoCaos.Add(baocao);
                database.SaveChanges();
                return RedirectToAction("BaoCaoPhongGiaoHang");
            }

            return View(baocao);
        }


        //------------------------------------------DANH SACH SHIPPER-----------------------------------------------------
        public ActionResult DanhSachShipper()
        {
            return View(database.NhanVienGHs.ToList().OrderByDescending(s => s.MaNV));
        }

        public ActionResult ThemShipper()
        {
            return View();
        }

        //Them Shipper
        [HttpPost]
        public ActionResult ThemShipper(NhanVienGH nhanVien)
        {
            Random rd = new Random();
            var manv = "SHIP" + rd.Next(1, 1000);
            nhanVien.MaNV = manv;

            database.NhanVienGHs.Add(nhanVien);
            database.SaveChanges();
            return RedirectToAction("DanhSachShipper");
        }

        public ActionResult ChinhsuaShipper(String id)
        {
            return View(database.NhanVienGHs.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhsuaShipper(String id, NhanVienGH nhanVien)
        {
            database.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DanhSachShipper");
        }

        public ActionResult XoaShipper(String id)
        {
            return View(database.NhanVienGHs.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaShipper(String id, NhanVienGH nhanvien)
        {
            nhanvien = database.NhanVienGHs.Where(s => s.MaNV == id).FirstOrDefault();
            database.NhanVienGHs.Remove(nhanvien);
            database.SaveChanges();
            return RedirectToAction("DanhSachShipper");

        }

        //-----------------------------------------------PHUONG TIEN GIAO HANG------------------------------------------
        public ActionResult DanhSachPhuongTienGiaoHang()
        {
            return View(database.PhuongTienGHs.ToList().OrderByDescending(s => s.MaPT));
        }
     
        public ActionResult ThemPhuongTienGiaoHang()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ThemPhuongTienGiaoHang(PhuongTienGH phuongtien)
        {
            Random rd = new Random();
            var mapt = "51A-" + rd.Next(1, 1000);
            phuongtien.MaPT = mapt;
            database.PhuongTienGHs.Add(phuongtien);
            database.SaveChanges();
            return RedirectToAction("DanhSachPhuongTienGiaoHang");
        }


        public ActionResult ChinhSuaPhuongTienGiaoHang(String id)
        {
            return View(database.PhuongTienGHs.Where(s => s.MaPT == id).FirstOrDefault());
        }


        [HttpPost]
        public ActionResult ChinhSuaPhuongTienGiaoHang(String id, PhuongTienGH pt)
        {
            database.Entry(pt).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DanhSachPhuongTienGiaoHang");
        }


        public ActionResult XoaPhuongTienGiaoHang(String id)
        {
            return View(database.PhuongTienGHs.Where(s => s.MaPT == id).FirstOrDefault());
        }


        [HttpPost]
        public ActionResult XoaPhuongTienGiaoHang(String id, PhuongTienGH pt)
        {
            pt = database.PhuongTienGHs.Where(s => s.MaPT == id).FirstOrDefault();
            database.PhuongTienGHs.Remove(pt);
            database.SaveChanges();
            return RedirectToAction("DanhSachPhuongTienGiaoHang");
        }

    }
}