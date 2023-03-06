using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [AuthenticationDL]
    public class KhachHangController : Controller
    {
        QuanLyDLEntities2 db = new QuanLyDLEntities2();
        // GET: KhachHang
        public ActionResult PageSanPham()
        {
            return View(db.SanPhams.ToList().OrderByDescending(s => s.TenSP));
        }

        // Chi tiết sản phẩm
        public ActionResult ChiTietSP(string id)
        {
            TempData["masp"] = id;
            return View(db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault());
        }

        // Giỏ hàng
        public ActionResult GioHangDL()
        {
            if (Session["Cart"] == null)
                return View();
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        // Tạo mới giỏ hàng
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Thêm vào giỏ hàng
        public ActionResult ThemVaoGH(string id)
        {
            var sp = db.SanPhams.SingleOrDefault(s => s.MaSP == id);

            if (sp.TongTon == 0 || sp.TongTon == null)
            {
                TempData["hethang"] = "Sản phẩm đã hết hàng!!!";
            }
            else if (sp.TongTon > 0)
            {
                TempData["Themvaogiohang"] = "thanhcong";
                GetCart().Add(sp);
            }
            return RedirectToAction("PageSanPham", "KhachHang");
        }

        // Update số lượng sản phẩm
        public ActionResult CapNhatSL(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id = form["MaSP"];
            int sl = int.Parse(form["soluong"]);
            var sol = db.SanPhams.Find(id);
            if (sl > sol.TongTon)
            {
                TempData["Warning"] = "Số lượng cập nhật lớn hơn số lượng tồn!!!";
                return RedirectToAction("GioHangDL", "KhachHang");
            }
            cart.CapNhatSL(id, sl);
            return RedirectToAction("GioHangDL", "KhachHang");
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public ActionResult XoaSP(string id)
        {
            TempData["XoaSP"] = "Xóa sản phẩm thành công!!!";
            Cart cart = Session["Cart"] as Cart;
            cart.XoaSP(id);
            Session["Cart"] = cart;
            return RedirectToAction("GioHangDL", "KhachHang");
        }

        //Trang đặt hàng
        public ActionResult PageDatHang()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || cart.Items.Count() <= 0)
            {
                TempData["GHNull"] = "Vui lòng chọn sản phẩm!!!";
                return RedirectToAction("GioHangDL", "KhachHang");
            }

            else if(cart.Items != null)
            {
                var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
                double tongphu = cart.TongTien();
                double giam = tongphu * (double)daiLy.LoaiDL.ChietKhau;
                double thanhtien = tongphu - giam;

                TempData["giam"] = string.Format("{0:0,0}", giam);
                TempData["thanhtien"] = string.Format("{0:0,0}", thanhtien);
                return View(cart);
            }
            return View();
        }

        //Đặt hàng
        public ActionResult DatHang(FormCollection form)
        {
            try
            {
                Random rd = new Random();
                Cart cart = Session["Cart"] as Cart;
                DonHang donHang = new DonHang();
                var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
                //Tính tổng tiền
                double tongphu = cart.TongTien();
                //Tính chiết khấu
                double giam = tongphu * (double)daiLy.LoaiDL.ChietKhau;
                //Tính thành tiền
                double thanhtien = tongphu - giam;

                //Thêm vào bảng đơn hàng
                var maDH = "DH" + rd.Next(1, 1000);
                donHang.MaDH = maDH;
                donHang.MaDL = daiLy.MaDL;
                donHang.MaNVLap = "NV01";
                donHang.NgayLap = DateTime.Now;
                donHang.TrangThai = "Chưa xử lý";
                donHang.TinhTrangThanhToan = "Chưa thanh toán";
                donHang.DiemGiao = form["diachigh"];
                donHang.TongTien = thanhtien;
                db.DonHangs.Add(donHang);

                //Thêm vào bảng chi tiết đơn hàng
                foreach (var item in cart.Items)
                {
                    ChiTietDonHang chiTietDonHang = new ChiTietDonHang();
                    chiTietDonHang.MaDH = maDH;
                    chiTietDonHang.MaSP = item.idSP.MaSP;
                    chiTietDonHang.SoLuong = item.soLuong;
                    chiTietDonHang.ThanhTien = item.soLuong * item.idSP.Gia.Value;
                    db.ChiTietDonHangs.Add(chiTietDonHang);
                }
                db.SaveChanges();
                cart.XoaSauKhiDat();
                TempData["muahangthanhcong"] = "Đặt hàng thành công";
                return RedirectToAction("PageSanPham", "KhachHang");
            }
            catch
            {
                return Content("không thành công");
            }
        }

        // GET: ChiTietDonHangs
        public ActionResult DonHangDL()
        {
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            var dsDH = db.DonHangs.Where(s => s.MaDL == daiLy.MaDL).ToList();
            return View(dsDH);
        }

        // GET: ChiTietDonHangs/Details/5
        public ActionResult DetailsDH(string id)
        {
            TempData["madh"] = id;

            var donhang = db.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault();
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            double tongphu = 0;
            tongphu += (double)donhang.ThanhTien;
            double giam = tongphu * (double)daiLy.LoaiDL.ChietKhau;
            double thanhtien = tongphu - giam;

            TempData["giam"] = string.Format("{0:0,0}", giam);
            TempData["thanhtien"] = string.Format("{0:0,0}", thanhtien);

            return View(db.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));
        }

        // GET: ChiTietDonHangs/Delete/5
        public ActionResult DeleteDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("DeleteDH")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDHConfirmed(string id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
            db.SaveChanges();
            return RedirectToAction("DonHangDL");
        }

        public ActionResult ThongTinDL(string id)
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
        //Đổi hình ảnh đại lý
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThongTinDL(DaiLy daiLy, HttpPostedFileBase HinhAnh)
        {
            var maDL = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            if (ModelState.IsValid)
            {
                daiLy = db.DaiLies.Find(maDL.MaDL);
                LuuAnh(daiLy, HinhAnh);
                var update = db.DaiLies.Find(daiLy.MaDL);
                update.HinhAnh = daiLy.HinhAnh;
                db.SaveChanges();
                maDL.HinhAnh = daiLy.HinhAnh;
                Session["userDL"] = maDL;
                return RedirectToAction("ThongTinDL");
            }
            return View(daiLy);
        }

        public void LuuAnh(DaiLy daiLy, HttpPostedFileBase HinhAnh)
        {
            #region Hình ảnh
            //Xác định đường dẫn lưu file : Url tương đói => tuyệt đói
            var urlTuongdoi = "/Data/ImagesDL/";
            var urlTuyetDoi = Server.MapPath(urlTuongdoi);// Lấy đường dẫn lưu file trên server

            //Check trùng tên file => Đổi tên file  = tên file cũ (ko kèm đuôi)
            //Ảnh.jpg = > ảnh + "-" + 1 + ".jpg" => ảnh-1.jpg

            string fullDuongDan = urlTuyetDoi + HinhAnh.FileName;
            int i = 1;
            while (System.IO.File.Exists(fullDuongDan) == true)
            {
                // 1. Tách tên và đuôi 
                var ten = Path.GetFileNameWithoutExtension(HinhAnh.FileName);
                var duoi = Path.GetExtension(HinhAnh.FileName);
                // 2. Sử dụng biến i để chạy và cộng vào tên file mới
                fullDuongDan = urlTuyetDoi + ten + "-" + i + duoi;
                i++;
                // 3. Check lại 
            }
            #endregion
            //Lưu file (Kiểm tra trùng file)
            HinhAnh.SaveAs(fullDuongDan);
            daiLy.HinhAnh = urlTuongdoi + Path.GetFileName(fullDuongDan);
        }
    }
}
