﻿using System;
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


        //Danh sach don hang moi can xet duyet
        public ActionResult DonHangMoi()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Hien thi chi tiet don giao hang
        public ActionResult ChiTietDonGiaoHang(string id)
        {
            TempData["gh_ctdonhang"] = database.DonHangs.ToList().Where(s => s.MaDH == id);
            return View(TempData["gh_ctdonhang"]);      
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

        


        //--------------------------LAP CAC CHUYEN GIAO HANG(gom nhieu don hang)------------------------------------

        //Danh sach cac chuyen giao 
        public ActionResult DanhSachCacChuyenGiao()
        {
            TempData["ngaylapcg"] = System.DateTime.Now;
            TempData["dschuyengiao"] = database.ChuyenGiaos.ToList().OrderByDescending(s => s.MaGH);
            return View(TempData["dschuyengiao"]);
        }

        //Chinh sua chuyen giao
        public ActionResult ChinhSuaChuyenGiao(string id)
        {
            return View(database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault());
        }

        [HttpPost, ActionName("ChinhSuaChuyenGiao")]
        public ActionResult ChinhSuaChuyenGiao(ChuyenGiao chuyengiao)
        {
            database.Entry(chuyengiao).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DanhSachCacChuyenGiao");
        }



        //Them chuyen giao
        public ActionResult ThemChuyenGiaoHang()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemChuyenGiaoHang([Bind(Include = "MaNVLap,MaNVGiao,MaPT,NgayLap,TrangThai,NgayGiao")] ChuyenGiao chuyenggiao)
        {
            if (chuyenggiao.MaPT == null || chuyenggiao.MaNVGiao == null)
            {
                TempData["AlertMessage"] = "check null";
                return RedirectToAction("ThemChuyenGiaoHang");
            }
            else
            {               
                //ViewBag.ngaylap = TempData["ngaylapcg"] = System.DateTime.Now;
                Random rd = new Random();
                var cg = "GH" + rd.Next(1, 1000);
                chuyenggiao.MaGH = cg;
                chuyenggiao.TrangThai = Convert.ToString("Chờ giao");
                chuyenggiao.NgayLap = System.DateTime.Now;
                database.ChuyenGiaos.Add(chuyenggiao);
                database.SaveChanges();
                TempData["messageAlert"] = "Đã thêm mới đơn hàng";
                return RedirectToAction("DanhSachCacChuyenGiao");
            }
            return View(chuyenggiao);
        }
    
        //Chi tiet chuyen giao
        public ActionResult ChiTietChuyenGiaoHang(string id)
        {
            return View(database.ChiTietChuyenGiaos.ToList().Where(s => s.MaGH == id));










        }

        //Huy chuyen giao
        public ActionResult HuyChuyenGiao(String id)
        {

            return View(database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult HuyChuyenGiao(String id, ChuyenGiao chuyengiao)
        {
            chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault();
            database.ChuyenGiaos.Remove(chuyengiao);
            database.SaveChanges();
            TempData["AlertMessage"] = "Xóa thành công";
            return RedirectToAction("DanhSachCacChuyenGiao");

        }


        //Xep don (test)
        public ActionResult XepDon()
        {
            database.ChuyenGiaos.ToList().OrderByDescending(s => s.MaGH);
            return View(TempData["gh_ctdonhang"]);
        }

        //Them don hang vao chuyen giao (test)
        public ActionResult ThemDonHangVaoChuyenGiao(string id1, string id2)
        {
            Session["macg1"] = id1;
            Session["macg2"] = id2;
            ViewBag.MaCTDVC = new SelectList(database.ChiTietChuyenGiaos, "MaCTDVC", "MaCTDVC");
            ViewBag.MaGH = new SelectList(database.ChiTietChuyenGiaos, "MaGH", "MaGH");
            ViewBag.MaDH = new SelectList(database.SanPhams, "MaDH", "MaDH");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDonHangVaoChuyenGiao([Bind(Include = "MaCTDVC,MaDH,MaGH")] ChiTietChuyenGiao chiTietChuyenGiao, ChuyenGiao chuyenGiao, DonHang donhang)
        {

            if (ModelState.IsValid)
            {
                chiTietChuyenGiao.MaCTDVC = (int)Session["donhang"];
                chuyenGiao.MaGH = (string)Session["donhang1"];
                if (donhang.TinhTrangGH == "Chờ giao")
                {
                    donhang.TinhTrangGH = Convert.ToString("Đang giao");
                }                
              
                database.ChiTietChuyenGiaos.Add(chiTietChuyenGiao);
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã thêm";
                TempData["MaCTKkk"] = chiTietChuyenGiao.MaCTDVC;
                return RedirectToAction("ChiTietChuyenGiaoHang", 
                    new RouteValueDictionary(new { controller = "GiaoHang", 
                        action = "ChiTietChuyenGiaoHang", Id = chiTietChuyenGiao.MaGH }));
            }
            return View();
        }



        //Xet duyet don giao hang (test)
        public ActionResult XetDuyetDonGiaoHang(string id)
        {
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

        

        //Cap nhat trang thai don hang
        public ActionResult CapNhatTrangThaiDonHang(string id)
        {
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }

        [HttpPost, ActionName("CapNhatTrangThaiDonHang")]
        public ActionResult CapNhatTrangThaiDonHang(DonHang donhang)
        {          
            database.Entry(donhang).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DonGiaoHang");
        }



        //Don hang hoan
        public ActionResult DanhSachDonHoan()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Bao cao ly do hoan don
        public ActionResult BaoCaoLyDoHoanDon()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }




        ////---------------------------------------------BAO CAO PHONG GIAO HANG-------------------------------------------
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult BaoCaoPhongGiaoHang(BaoCao baocao)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        Random rd = new Random();
        //        var mabc = "BC" + rd.Next(1, 1000);
        //        baocao.MaBC = mabc;
        //        var manv = (Test02.Models.NhanVien)Session["user"];
        //        baocao.MaNV = manv.MaNV;
        //        database.BaoCaos.Add(baocao);
        //        database.SaveChanges();
        //        return RedirectToAction("BaoCaoPhongGiaoHang");
        //    }

        //    return View(baocao);
        //}
        //public ActionResult BaoCaoPhongGiaoHang()
        //{
        //    return View();
        //}


        //-------------------------------------------------SHIPPER-----------------------------------------------------

        //Danh sach shipper
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
            TempData["AlertMessage"] = "Thêm nhân viên giao hàng thành công";
            return RedirectToAction("DanhSachShipper");
        }

        //chinh sua shipper
        public ActionResult ChinhsuaShipper(String id)
        {
            return View(database.NhanVienGHs.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhsuaShipper(String id, NhanVienGH nhanVien)
        {
            database.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            TempData["AlertMessage"] = "Chỉnh sửa thành công";
            return RedirectToAction("DanhSachShipper");
        }

        //xoa shipper
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
            TempData["AlertMessage"] = "Xóa thành công";
            return RedirectToAction("DanhSachShipper");

        }

        //-----------------------------------------------PHUONG TIEN GIAO HANG------------------------------------------

        //Danh sach phuong tien giao hang
        public ActionResult DanhSachPhuongTienGiaoHang()
        {
            return View(database.PhuongTienGHs.ToList().OrderByDescending(s => s.MaPT));
        }
     

        //Them phuong tien giao hang
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

        //Chinh sua phuong tien giao hang
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

        //Xoa phuong tien giao hang
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


        //----------------------------------------------IN PHIEU--------------------------------------------------
        public ActionResult InPhieuXuatKho(string id)
        { 
            return View(database.PhieuNhapXuats.ToList().Where(s => s.MaPhieu == id));
        }

        public ActionResult InHoaDon(string id)
        {
            return View(database.PhieuNhapXuats.ToList().Where(s => s.MaPhieu == id));
        }


        //-------------------------------------TEST DEMO-------------------------------------------
        public ActionResult ThemDonHang(string id)
        {
            return View();
        }

    }
}