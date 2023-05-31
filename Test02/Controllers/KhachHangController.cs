using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;
using PagedList;
namespace Test02.Controllers
{
    [AuthenticationDL]
    public class KhachHangController : Controller
    {
        QuanLyDLproEntities2 db = new QuanLyDLproEntities2();
        // GET: KhachHang

        public ActionResult PageSanPham(int? page, string query)
        {
            TempData["timkiem"] = query;
            Session["page"] = page;
            Session["query"] = query;
            int pageSize = 6;
            int pageNum = (page ?? 1);   
            if (query == null || query == "")
            {
                return View(db.SanPhams.ToList().OrderByDescending(s => s.TenSP).ToPagedList(pageNum, pageSize));
            }
            
            if (query.Length == 1)
            {
                var data = db.SanPhams.Where(s => s.MaSP.Substring(0, 1) == query || s.TenSP.Substring(0, 1) == query).ToList().OrderByDescending(s => s.TenSP).ToPagedList(pageNum, pageSize);
                return View(data);
            }
            else 
            {
                var data = db.SanPhams.Where(s => s.MaSP.Contains(query)|| s.TenSP.Contains(query)).ToList().OrderByDescending(s => s.TenSP).ToPagedList(pageNum, pageSize);
                return View(data);
            }
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
            return RedirectToAction("PageSanPham", new { page = Session["page"], query = Session["query"] });
        }

        public ActionResult ThemVaoGHViewChiTietSP(string id)
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
            return RedirectToAction("ChiTietSP/" + id, "KhachHang");
        }

        //Update số lượng sản phẩm
        //public ActionResult CapNhatSL(FormCollection form)
        //{
        //    Cart cart = Session["Cart"] as Cart;
        //    string id = form["MaSP"];
        //    int sl = int.Parse(form["soluong"]);
        //    var sol = db.SanPhams.Find(id);
        //    if (sl > sol.TongTon)
        //    {
        //        TempData["Warning"] = "Số lượng cập nhật lớn hơn số lượng tồn!!!";
        //        return RedirectToAction("GioHangDL", "KhachHang");
        //    }
        //    if (sl <= 0)
        //    {
        //        TempData["SLSP=0"] = "Số lượng sản phẩm phải lớn hơn 0";
        //        return RedirectToAction("GioHangDL", "KhachHang");
        //    }
        //    cart.CapNhatSL(id, sl);
        //    return RedirectToAction("GioHangDL", "KhachHang");
        //}

        [HttpPost]
        public ActionResult CapNhatSL(string MaSP, int soluong)
        {
            var cart = GetCart();
            var maSP = db.SanPhams.FirstOrDefault(s => s.MaSP == MaSP);

            cart.CapNhatSL(MaSP, soluong);

            double tongTien = cart.TongTien();
            double thanhTien = cart.ThanhTien(maSP.MaSP);

            var result = new
            {
                tongTien = "Tổng tiền: " + tongTien.ToString("#,##0") + " VNĐ",
                thanhTien = thanhTien.ToString("#,##0") + " VNĐ",
            };
            return Json(result);
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

        public double TinhCongNo(string MaDL)
        {
            List<PhieuCongNo> phieuCongNos = db.PhieuCongNoes.Where(s => s.MaDL == MaDL && s.TrangThai == "Chưa thanh toán").ToList();

            double tongCN = 0;
            foreach(var i in phieuCongNos)
            {
                tongCN += (double)i.TienNo;
            }
            return tongCN;
        }

        public double MaxNo(string maLoaiDL)
        {
            double congNo = 0;
            if (maLoaiDL == "LDL01")
            {
                congNo = 500000000;
            }
            else if (maLoaiDL == "LDL02")
            {
                congNo = 250000000;
            }
            else
            {
                congNo = 100000000;
            }
            return congNo;
        }

        //Trang đặt hàng
        public ActionResult PageDatHang()
        {
            Cart cart = Session["Cart"] as Cart;

            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];

            if (cart == null || cart.Items.Count() <= 0)
            {
                TempData["GHNull"] = "Vui lòng chọn sản phẩm!!!";
                return RedirectToAction("GioHangDL", "KhachHang");
            }

            else if(cart.Items != null)
            {
                double tongphu = cart.TongTien();
                double giam = tongphu * (double)daiLy.LoaiDL.ChietKhau;
                double thanhtien = tongphu - giam;

                double tongTienCN = TinhCongNo(daiLy.MaDL);
                double maxCN = MaxNo(daiLy.MaLoaiDL);
                double tong = tongphu + tongTienCN;

                var gioHang = cart.Items.ToList();
                foreach (var item in gioHang)
                {
                    if (item.soLuong > item.idSP.TongTon || item.soLuong < 0)
                    {
                        TempData["ErrorSL"] = "Số lượng không hợp lệ";
                        return RedirectToAction("GioHangDL", "KhachHang");
                    }
                }

                if (tong >= maxCN)
                {
                    TempData["MaxCN"] = "Công nợ vượt quá mức quy định, vui lòng thanh toán công nợ trước khi đặt hàng";
                    return RedirectToAction("GioHangDL", "KhachHang");
                }
                else
                {
                    TempData["giam"] = string.Format("{0:0,0}", giam);
                    TempData["thanhtien"] = string.Format("{0:0,0}", thanhtien);
                    return View(cart);
                }
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
                var maDH = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);

                donHang.MaDH = maDH;
                donHang.MaDL = daiLy.MaDL;
                donHang.MaNVLap = null;
                donHang.NgayLap = DateTime.Now;
                donHang.TrangThai = "Chưa xử lý";
                donHang.TinhTrangThanhToan = "Chưa thanh toán";
                donHang.DiemGiao = daiLy.DiaChi;
                donHang.TongTien = (float)thanhtien;
                donHang.PhieuXuatKho = false;
                donHang.XuatHoaDon = false;
                db.DonHangs.Add(donHang);

                ChiTietDonHang chiTietDonHang = new ChiTietDonHang();
                Random maCTDH = new Random();
                //Thêm vào bảng chi tiết đơn hàng
                foreach (var item in cart.Items)
                {
                    
                    var iDCTDH = maCTDH.Next(1, 10000);
                    chiTietDonHang = new ChiTietDonHang();
                    chiTietDonHang.MaCTDH = iDCTDH;
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

        public ActionResult DSChoGiao()
        {
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            var dsDH = db.DonHangs.Where(s => s.MaDL == daiLy.MaDL && s.TinhTrangGH == "Chờ giao").ToList();
            return View(dsDH);
        }

        public ActionResult DSDangGiao()
        {
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            var dsDH = db.DonHangs.Where(s => s.MaDL == daiLy.MaDL && s.TinhTrangGH == "Đang giao").ToList();
            return View(dsDH);
        }

        public ActionResult DSDHDaGiao()
        {
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            var dsDH = db.DonHangs.Where(s => s.MaDL == daiLy.MaDL && s.TinhTrangGH == "Đã giao").ToList();
            return View(dsDH);
        }

        // GET: ChiTietDonHangs/Details/5
        public ActionResult DetailsDH(string id)
        {
            TempData["madh"] = id;
            var maDH = db.DonHangs.FirstOrDefault(s => s.MaDH == id);
            TempData["DiaDiemGH"] = maDH.DiemGiao;
            return View(db.ChiTietDonHangs.Where(s => s.MaDH == id).ToList());
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

        public ActionResult Thang()
        {
            List<SelectListItem> months = new List<SelectListItem>();
            months.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
            months.Add(new SelectListItem { Text = "Tháng 1", Value = "1" });
            months.Add(new SelectListItem { Text = "Tháng 2", Value = "2" });
            months.Add(new SelectListItem { Text = "Tháng 3", Value = "3" });
            months.Add(new SelectListItem { Text = "Tháng 4", Value = "4" });
            months.Add(new SelectListItem { Text = "Tháng 5", Value = "5" });
            months.Add(new SelectListItem { Text = "Tháng 6", Value = "6" });
            months.Add(new SelectListItem { Text = "Tháng 7", Value = "7" });
            months.Add(new SelectListItem { Text = "Tháng 8", Value = "8" });
            months.Add(new SelectListItem { Text = "Tháng 9", Value = "9" });
            months.Add(new SelectListItem { Text = "Tháng 10", Value = "10" });
            months.Add(new SelectListItem { Text = "Tháng 11", Value = "11" });
            months.Add(new SelectListItem { Text = "Tháng 12", Value = "12" });
            ViewBag.Months = months;
            return View();
        }

        public ActionResult CongNo()
        {
            Thang();
            var daiLy = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            TempData["daiLy"] = daiLy.MaDL;
            var dsCongNo = db.PhieuCongNoes.Where(s => s.MaDL == daiLy.MaDL).ToList();

            var conNo = db.PhieuCongNoes.Where(s => s.TrangThai == "Chưa thanh toán" && s.MaDL == daiLy.MaDL);

            if (daiLy.MaLoaiDL == "LDL01")
            {
                ViewBag.CongNo = 500000000;
            }
            else if (daiLy.MaLoaiDL == "LDL02")
            {
                ViewBag.CongNo = 250000000;
            }
            else
            {
                ViewBag.CongNo = 100000000;
            }

            double tong = 0;
            foreach (var i in conNo)
            {
                tong += (double)i.TienNo;
            }
            TempData["TongConNo"] = tong;
            return View(dsCongNo);
        }

        public ActionResult LienHe()
        {
            return View();
        }

        public JsonResult CheckEmailAvailability(string email)
        {
            System.Threading.Thread.Sleep(200);

            var mailCus = db.DaiLies.Where(x => x.Email == email).SingleOrDefault();
            if (mailCus != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult CheckNoPhoneAvailability(string noPhone)
        {
            System.Threading.Thread.Sleep(200);

            var noPhoneCus = db.DaiLies.Where(x => x.SDT == noPhone).SingleOrDefault();
            if (noPhoneCus != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public ActionResult ThongTinDL(string id)
        {
            //var maDL = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
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
        public ActionResult ThongTinDL(DaiLy daiLy, HttpPostedFileBase HinhAnh, string id)
        {
            DaiLy info = db.DaiLies.Find(id);

            var totalEmail = db.DaiLies.Count(s => s.Email == daiLy.Email);
            var totalNoPhone = db.DaiLies.Count(s => s.SDT == daiLy.SDT);

            if ((daiLy.SDT != info.SDT && totalNoPhone >= 1) ||
                (daiLy.Email != info.Email && totalEmail >= 1))
            {
                TempData["errorEmailPhone"] = "Không thể cập nhật, vì số điện thoại và email này đã được sử dụng";
                return View(daiLy);
            }

            if (ModelState.IsValid)
            {
                DaiLy update = db.DaiLies.Find(id);
                if (daiLy.Email == info.Email && daiLy.SDT == info.SDT && daiLy.DiaChi == info.DiaChi && HinhAnh == null)
                {
                    return RedirectToAction("ThongTinDL");
                }

                else
                {
                    LuuAnh(daiLy, HinhAnh);
                    update.TenDL = daiLy.TenDL;
                    update.Email = daiLy.Email;
                    update.SDT = daiLy.SDT;
                    update.DiaChi = daiLy.DiaChi;
                    update.HinhAnh = daiLy.HinhAnh;
                    Session["userDL"] = info;
                    db.SaveChanges();
                    TempData["UpdateTC"] = "Cập nhật đại lý thành công";
                    return RedirectToAction("ThongTinDL");
                }
            }
            return RedirectToAction("ThongTinDL");
        }

        public ActionResult CapNhatMatKhau(string id)
        {
            //var maDL = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatMatKhau(DaiLy daiLy, string matkhau, string confirmPass, string id)
        {
            //var maDL = (Test02.Models.DaiLy)HttpContext.Session["userDL"];
            
            var maDL = db.DaiLies.Find(id);
            if (matkhau != maDL.Password)
            {
                TempData["errorPass"] = "Mật khẩu không chính xác";
                return RedirectToAction("CapNhatMatKhau");
            }

            if (confirmPass != daiLy.Password)
            {
                TempData["errorConfirm"] = "Xác thực mật khẩu không chính xác";
                return RedirectToAction("CapNhatMatKhau");
            }

            if (ModelState.IsValid)
            {
                DaiLy update = db.DaiLies.Find(id);
                //db.Entry(daiLy).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                update.Password = daiLy.Password;
                db.SaveChanges();
                TempData["Pass"] = "Đổi mật khẩu thành công!!!";
                return RedirectToAction("ThongTinDL/" + maDL.MaDL);
            }
            return View(daiLy);
        }

        public void LuuAnh(DaiLy daiLy, HttpPostedFileBase HinhAnh)
        {
            #region Hình ảnh
            if (HinhAnh == null)
            {
                daiLy.HinhAnh = daiLy.HinhAnh;
            }
            else
            {
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
}
