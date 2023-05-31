using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{

    [Authentication(MaChucVu = "NVGH")]
    public class GiaoHangController : Controller
    {
        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
        // GET: GiaoHang


        //Danh sách đơn hàng mới cần xét duyệt
        public ActionResult DonHangMoi()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        //Hiển thị chi tiết đơn hàng
        public ActionResult ChiTietDonHang(string id)
        {
            TempData["madh02"] = id;
            TempData["gh_ctdonhang02"] = database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id);
            return View(TempData["gh_ctdonhang02"]);
        }

        //Hiển thị chi tiết đơn giao hàng
        public ActionResult ChiTietDonGiaoHang(string id)
        {
            TempData["madh"] = id;
            TempData["gh_ctdonhang"] = database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id);
            return View(TempData["gh_ctdonhang"]);
        }


        //Hien thi danh sach don hang giao
        public ActionResult DonGiaoHang()
        {
            //tính tổng don
            var tongdonchogiao = 0;
            var tongdondanggiao = 0;
            var tongdondagiao = 0;
            var dh = database.DonHangs.ToList();
            foreach (var item in dh)
            {
                if (item.XuatHoaDon == true
                    && item.PhieuXuatKho == true
                    && item.TrangThai == "Đã xét duyệt"
                    && item.TinhTrangGH == "Chờ giao"
                    && item.MaGH != null)
                {
                    tongdonchogiao = tongdonchogiao + 1;
                }
                else if (item.XuatHoaDon == true
                    && item.PhieuXuatKho == true
                    && item.TrangThai == "Đã xét duyệt"
                    && item.TinhTrangGH == "Đang giao"
                    && item.MaGH != null)
                {
                    tongdondanggiao = tongdondanggiao + 1;
                }
                else if (item.XuatHoaDon == true
                    && item.PhieuXuatKho == true
                    && item.TrangThai == "Đã xét duyệt"
                    && item.TinhTrangGH == "Đã giao"
                    && item.MaGH != null)
                {
                    tongdondagiao = tongdondagiao + 1;
                }
            }
            TempData["TongDonChoGiao"] = tongdonchogiao;
            TempData["TongDonDangGiao"] = tongdondanggiao;
            TempData["TongDonDaGiao"] = tongdondagiao;

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
        public ActionResult DonChoGiao()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }


        //--------------------------LAP CAC CHUYEN GIAO HANG(gom nhieu don hang)------------------------------------

        //Danh sach cac chuyen giao 
        public ActionResult DanhSachCacChuyenGiao(ChuyenGiao chuyengiao)
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
        public ActionResult ChinhSuaChuyenGiao(ChuyenGiao chuyengiao, DonHang donhang,
            NhanVienGH shipper, PhuongTienGH phuongtien)
        {
            donhang = database.DonHangs.Where(s => s.MaGH == chuyengiao.MaGH).FirstOrDefault();
            shipper = database.NhanVienGHs.Where(s => s.MaGH == shipper.MaGH).FirstOrDefault();
            phuongtien = database.PhuongTienGHs.Where(s => s.MaPT == phuongtien.MaPT).FirstOrDefault();
            
            if (chuyengiao.TrangThai == "Chờ giao")
            {
                donhang.TinhTrangGH = Convert.ToString("Chờ giao");
                shipper.TinhTrang = Convert.ToString("Đang giao hàng");
                phuongtien.TinhTrang = Convert.ToString("Sử dụng");
            }
            else if (chuyengiao.TrangThai == "Đang giao")
            {
                donhang.TinhTrangGH = Convert.ToString("Đang giao");
                shipper.TinhTrang = Convert.ToString("Đang giao hàng");
                phuongtien.TinhTrang = Convert.ToString("Sử dụng");
            }
            else if (chuyengiao.TrangThai == "Đã giao")
            {
                donhang.TinhTrangGH = Convert.ToString("Đã giao");
                shipper.TinhTrang = Convert.ToString("Sẵn sàng");
                phuongtien.TinhTrang = Convert.ToString("Sẵn sàng");
            }
            database.Entry(chuyengiao).State = System.Data.Entity.EntityState.Modified;
            database.Entry(donhang).State = System.Data.Entity.EntityState.Modified;
            database.Entry(shipper).State = System.Data.Entity.EntityState.Modified;
            database.Entry(phuongtien).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DanhSachCacChuyenGiao");
        }

        //Them chuyen giao
        public ActionResult ThemChuyenGiaoHang()
        {
            ViewBag.PhuongTien = new SelectList(database.PhuongTienGHs.Where(s => s.TinhTrang == "Sẵn sàng"), "MaPT", "MaPT");
            ViewBag.NVGiao = new SelectList(database.NhanVienGHs, "MaNV", "TenNV");
            TempData["messageAlert"] = "Đã thêm chuyến giao mới";
            return View();
        }

        [HttpPost]
        public ActionResult ThemChuyenGiaoHang([Bind(Include = "MaNVLap,MaNVGiao,MaPT,NgayLap,TrangThai,NgayGiao")]
        ChuyenGiao chuyenggiao, NhanVienGH nhanviengiao, PhieuNhapXuat phieunhapxuat, PhuongTienGH phuongtien)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ngaylap = TempData["ngaylapcg"] = System.DateTime.Now;
                Random rd = new Random();
                var cg = "GH" + rd.Next(1, 1000);
                chuyenggiao.TienThu = 0;
                chuyenggiao.MaGH = cg;
                chuyenggiao.TrangThai = Convert.ToString("Chờ giao");
                chuyenggiao.NgayLap = System.DateTime.Now;
                phuongtien = database.PhuongTienGHs.Where(s => s.MaPT == chuyenggiao.MaPT).FirstOrDefault();
                phuongtien.TinhTrang = Convert.ToString("Sử dụng");
                database.ChuyenGiaos.Add(chuyenggiao);
                database.SaveChanges();
            }
            return RedirectToAction("DanhSachCacChuyenGiao");
        }

        //Xoa chuyen giao hang
        public ActionResult XoaChuyenGiao(String id)
        {
            return View(database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaChuyenGiao(String id, ChuyenGiao chuyengiao, NhanVienGH shipper, PhuongTienGH phuongtien)
        {
            chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault();
            shipper = database.NhanVienGHs.Where(s => s.MaGH == id).FirstOrDefault();
            phuongtien = database.PhuongTienGHs.Where(s => s.MaPT == chuyengiao.MaPT).FirstOrDefault();
            shipper.TinhTrang = Convert.ToString("Sẵn sàng");
            shipper.MaGH = null;
            phuongtien.TinhTrang = Convert.ToString("Sẵn sàng");
            database.Entry(shipper).State = System.Data.Entity.EntityState.Modified;
            database.Entry(phuongtien).State = System.Data.Entity.EntityState.Modified;
            database.ChuyenGiaos.Remove(chuyengiao);
            database.SaveChanges();
            TempData["AlertMessage"] = "Xóa thành công";
            return RedirectToAction("DanhSachCacChuyenGiao");
        }


        //Chi tiet chuyen giao(xoa)
        public ActionResult ChiTietChuyenGiaoHang(string id)
        {
            TempData["magh01"] = id;
            return View(database.DonHangs.ToList().Where(s => s.MaGH == id));
        }

        //Huy chuyen giao
        //public ActionResult HuyChuyenGiao(String id)
        //{

        //    return View(database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault());
        //}

        //[HttpPost]
        //public ActionResult HuyChuyenGiao(String id, ChuyenGiao chuyengiao)
        //{
        //    chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault();
        //    database.ChuyenGiaos.Remove(chuyengiao);
        //    database.SaveChanges();
        //    TempData["AlertMessage"] = "Xóa thành công";
        //    return RedirectToAction("DanhSachCacChuyenGiao");

        //}

        //Xep don 
        public ActionResult XepDon()
        {
            database.ChuyenGiaos.ToList().OrderByDescending(s => s.MaGH);
            return View();
        }


        //Cap nhat trang thai don hang, cap nhat tong tien thu khi tinh trang don hang la "Chua thanh toan"
        public ActionResult CapNhatTrangThaiDonHang(string id)
        {
            TempData["madh01"] = id;
            ViewBag.ChuyenGiao = new SelectList(database.ChuyenGiaos.Where(s => s.TrangThai == "Chờ giao"), "MaGH", "MaGH");
            //ViewBag.ChuyenGiao = new SelectList(database.ChuyenGiaos, "MaGH", "MaGH");
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }

        [HttpPost, ActionName("CapNhatTrangThaiDonHang")]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatTrangThaiDonHang(DonHang donhang, ChuyenGiao chuyengiao)
        {
            if (ModelState.IsValid)
            {
                chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == donhang.MaGH).FirstOrDefault();
                if (donhang.TinhTrangThanhToan == "Chưa thanh toán")
                {
                    var tien = donhang.TongTien;
                    chuyengiao.TienThu += tien;
                }
                else if (donhang.TinhTrangThanhToan == "Đã thanh toán")
                {
                    chuyengiao.TienThu = chuyengiao.TienThu;
                }
                database.Entry(donhang).State = System.Data.Entity.EntityState.Modified;
                database.Entry(chuyengiao).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["messageAlert"] = "Xếp đơn thành công";
            }
            return RedirectToAction("DanhSachCacChuyenGiao");
        }


        public ActionResult CapNhatDonGiaoHang(string id)
        {
            TempData["madh01"] = id;
            ViewBag.ChuyenGiao = new SelectList(database.ChuyenGiaos, "MaGH", "MaGH");
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult CapNhatDonGiaoHang(String id, DonHang donHang)
        {
            database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
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
            nhanVien.TinhTrang = Convert.ToString("Sẵn sàng");
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

        //xep xe cho shipper
        public ActionResult XepXe(String id)
        {
            ViewBag.ChuyenGiao = new SelectList(database.ChuyenGiaos.Where(s => s.TrangThai == "Chờ giao"), "MaGH", "MaGH");
            return View(database.NhanVienGHs.Where(s => s.MaNV == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult XepXe(String id, NhanVienGH nhanVien, ChuyenGiao chuyengiao)
        {
            nhanVien.TinhTrang = Convert.ToString("Đang giao hàng");
            database.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;

            chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == nhanVien.MaGH).FirstOrDefault();
            chuyengiao.MaNVLap = nhanVien.MaNV;
            database.Entry(chuyengiao).State = System.Data.Entity.EntityState.Modified;

            database.SaveChanges();
            TempData["AlertMessage"] = "Xếp xe thành công";
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
            //Random rd = new Random();
            //var mapt = "51A-" + rd.Next(1, 1000);
            //phuongtien.MaPT = mapt;
            phuongtien.TinhTrang = Convert.ToString("Sẵn sàng");
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

        //In phieu xuat kho
        public ActionResult InPhieuXuatKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            TempData["maPhieuXuattheodonhang"] = id;
            var maDH = phieuNhapXuat.MaDH;
            TempData["ctmadh"] = phieuNhapXuat.MaDH;
            TempData["nvlap"] = phieuNhapXuat.MaNVLap;
            TempData["tennvlap"] = phieuNhapXuat.NhanVien.TenNV;
            TempData["ngaylap"] = phieuNhapXuat.NgayLap;
            TempData["tenNVGiao"] = phieuNhapXuat.NguoiGiaoHang;
            TempData["phuongtien"] = phieuNhapXuat.PhuongTienGiaoHang;
            foreach (var item in database.DonHangs)
            {
                if (maDH == item.MaDH)
                {
                    TempData["maDL"] = item.MaDL;
                    TempData["tenDL"] = item.DaiLy.TenDL;
                    TempData["diemgiao"] = item.DiemGiao;
                }
            }
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        //In hoa don 
        public ActionResult InHoaDon(string id)
        {
            DonHang donHang = database.DonHangs.Find(id);
            ChuyenGiao chuyengiao = database.ChuyenGiaos.Where(s => s.MaGH == id).FirstOrDefault();
            TempData["chuyengiao"] = chuyengiao;
            TempData["ctmadh"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Session["madaily"] = id;
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }
    }
}