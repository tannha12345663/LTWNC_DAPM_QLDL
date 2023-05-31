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
using System.IO;
using System.Web.Helpers;

namespace Test02.Controllers
{
    [Authentication (MaChucVu ="NVKD")]
    public class KinhDoanhController : Controller
    {

        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
        // GET: KinhDoanh
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult QuanLyDL()
        {
            return View(database.DaiLies.ToList().OrderByDescending(s=> s.NgayTao));
        }
        //Kiểm tra tên đại lý
        [HttpPost]
        public ActionResult KTTenDL(string TenDL)
        {
            var check = database.DaiLies.Where(s => s.TenDL == TenDL).FirstOrDefault();
            if(check != null)
            {
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult KTSDT(string SDT)
        {
            var check = database.DaiLies.Where(s => s.SDT == SDT).FirstOrDefault();
            if (check != null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult KTEmail(string Email)
        {
            if (Email.IndexOf("@gmail.com") > 0)
            {
                var check = database.DaiLies.Where(s => s.Email == Email).FirstOrDefault();
                if (check != null)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            
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
        public ActionResult ResetPass(string madl)
        {
            if (madl == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resetpass = database.DaiLies.Find(madl);
                resetpass.Password = "123456";
                database.Entry(resetpass).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["madl"] = madl;
                //Gửi emil về tk
                var daily = database.DaiLies.Find(madl);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/ResetPasss.html"));
                content = content.Replace("{{TenDL}}", daily.Email);
                content = content.Replace("{{username}}", daily.UserName);
                content = content.Replace("{{time}}", DateTime.Now.ToString());
                //content = content.Replace("{{Total}}", tongtien);
                //content = content.Replace("{{Thoigian}}", Convert.ToString(ttkh.CreateDate));
                //content = content.Replace("{{Invoice}}", themhd.MaHD);
                string subject = "Tài khoản đại lý "+daily.MaDL+" của bạn vừa được cập nhật !";
                WebMail.Send(daily.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDL([Bind(Include = "MaLoaiDL,UserName,Password,TenDL,SDT,DiaChi,Email")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var madl = "DL" + rd.Next(0, 10) + rd.Next(0, 10) + rd.Next(0, 10) + rd.Next(0, 10);
                daiLy.MaDL = madl;
                daiLy.NgayTao = System.DateTime.Now;
                daiLy.Password = "123456";
                database.DaiLies.Add(daiLy);
                database.SaveChanges();
                TempData["messageAlert"] = "Đã thêm mới";
                TempData["TenDLthem"] = daiLy.TenDL;
                //Gửi emil về tk
                string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/AddAccount.html"));
                content = content.Replace("{{email}}", daiLy.Email);
                content = content.Replace("{{Password}}", daiLy.Password);
                content = content.Replace("{{tentk}}", daiLy.TenDL);
                //content = content.Replace("{{Total}}", tongtien);
                //content = content.Replace("{{Thoigian}}", Convert.ToString(ttkh.CreateDate));
                //content = content.Replace("{{Invoice}}", themhd.MaHD);
                string subject = "Đây là tin nhắn tự động từ hệ thống POS";
                WebMail.Send(daiLy.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
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
            //Gửi emil về tk
            string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/DeleteAccount.html"));
            content = content.Replace("{{TenDL}}", daiLy.TenDL);
            content = content.Replace("{{username}}", daiLy.UserName);
            content = content.Replace("{{time}}", DateTime.Now.ToString());
            //content = content.Replace("{{Total}}", tongtien);
            //content = content.Replace("{{Thoigian}}", Convert.ToString(ttkh.CreateDate));
            //content = content.Replace("{{Invoice}}", themhd.MaHD);
            string subject = "BẠN ĐÃ XÓA TÀI KHOẢN ĐẠI LÝ";
            WebMail.Send(daiLy.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
            return RedirectToAction("QuanLyDL");
        }
        //Quản lý đơn hàng
        public ActionResult QuanLyDH()
        {
            return View(database.DonHangs.ToList().OrderBy(s => s.NgayLap));
        }
        [HttpPost]
        public ActionResult CheckCongNoDL(string MaDL, string ThanhTien)
        {
            var tongtiendh = 0;
            var daily = database.DaiLies.Where(s => s.MaDL == MaDL).FirstOrDefault();
            tongtiendh += Convert.ToInt32(ThanhTien);
            if (daily.MaLoaiDL == "LDL01" && tongtiendh <= 500000000)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else if (daily.MaLoaiDL == "LDL02" && tongtiendh <= 250000000)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else if (daily.MaLoaiDL == "LDL03" && tongtiendh <= 100000000)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
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
            DataTable dh = new DataTable();
            if (Session["DonHang"] == null)
            {
                dh.Columns.Add("MaDH");
                dh.Columns.Add("MaDL");
                dh.Columns.Add("MaNVLap");
                dh.Columns.Add("NgayLap");
                dh.Columns.Add("TrangThai");
                dh.Columns.Add("TinhTrangThanhToan");
                dh.Columns.Add("DiemGiao");
                //Tạo xong lưu lại Session
                Session["DonHang"] = dh;
            }
            else
            {
                //Lấy thông tin từ Session
                dh = Session["DonHang"] as DataTable;
            }
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
                //Lưu thông tin đơn hàng vào bảng DataTable
                DataRow dr = dh.NewRow();
                dr["MaDH"] = donHang.MaDH;
                dr["MaDL"] = donHang.MaDL;
                dr["MaNVLap"] = donHang.MaNVLap;
                dr["NgayLap"] = donHang.NgayLap;
                dr["TrangThai"] = donHang.TrangThai;
                dr["TinhTrangThanhToan"] = donHang.TinhTrangThanhToan;
                dr["DiemGiao"] = donHang.DiemGiao;
                dh.Rows.Add(dr);

                //database.DonHangs.Add(donHang);
                //database.SaveChanges();
                return RedirectToAction("ThemCTHD");
            }

            return View(donHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemLDL(LoaiDL loaiDL)
        {
            Random rd = new Random();
            var madl = "LDL" + rd.Next(1, 1000);
            loaiDL.MaLoaiDL = madl;
            database.LoaiDLs.Add(loaiDL);
            database.SaveChanges();
            TempData["messageAlert"] = "Đã thêm mới loại đại lý";
            return RedirectToAction("ThemDL");
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
                //Thêm thông tin đơn hàng sau khi đã điền chi tiết đơn hàng

                LuudhvaoDB();
                //Thêm chi tiết đơn hàng
                chiTietDonHang.MaDH = (string)Session["tempdata"];
                var dongia = database.SanPhams.Where(s => s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                if (chiTietDonHang.ChietKhau == null)
                {
                    chiTietDonHang.ThanhTien = (dongia.Gia) * (chiTietDonHang.SoLuong);
                }
                else
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (dongia.Gia) * (chiTietDonHang.ChietKhau);
                }
                var checkslp = database.SanPhams.Where(s => s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                if (chiTietDonHang.SoLuong  > checkslp.TongTon)
                {
                    var madh = database.DonHangs.Where(s=> s.MaDH == chiTietDonHang.MaDH).FirstOrDefault();
                    TempData["messageAlert"] = "Không đủ số lượng để đặt";
                    database.DonHangs.Remove(madh);
                    database.SaveChanges();
                    return RedirectToAction("QuanLyDH");
                }
                else {
                    var ctk = database.ChiTietKhoes.Where(s => s.MaSP == chiTietDonHang.MaSP).ToList();
                    var slsp = (int)chiTietDonHang.SoLuong;
                    var checksl = -slsp;
                    foreach (var udk in ctk)
                    {
                        checksl += (int)udk.SoLuong;
                            if(checksl>=0)
                            {
                                udk.SoLuong = checksl;
                                if (udk.SoLuong < 1500)
                                {
                                    udk.TinhTrang = "Sắp hết hàng";
                                }
                                database.Entry(udk).State = System.Data.Entity.EntityState.Modified;
                                database.SaveChanges();
                                break;
                            }
                            else if (checksl <0)
                            {
                                udk.TinhTrang = "Hết hàng";
                                udk.SoLuong = 0;
                                database.Entry(udk).State = System.Data.Entity.EntityState.Modified;
                                database.SaveChanges();
                            }
                    }
                    database.ChiTietDonHangs.Add(chiTietDonHang);
                    database.SaveChanges();
                    //THêm tổng tiền của đơn hàng
                    var dh = database.DonHangs.Find(chiTietDonHang.MaDH);
                    dh.TongTien = chiTietDonHang.ThanhTien;
                    database.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                    TempData["messageAlert"] = "Đã thêm mới đơn hàng";
                    TempData["themmadh"] = chiTietDonHang.MaDH;
                    return RedirectToAction("QuanLyDH");
                }
                
                
            }

            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }
        //Version 2.0 mới 
        [HttpPost]
        public ActionResult ThemDH02(DonHang dh)
        {
            int id = int.Parse(Request["sl"]);
            if (id > 0)
            {
                var user = (Test02.Models.NhanVien)Session["user"];
                Random rd = new Random();
                var madh = "DH" + rd.Next(1, 1000);
                dh.MaDH = madh;
                dh.NgayLap = System.DateTime.Now;
                dh.MaNVLap = user.MaNV;
                dh.TinhTrangThanhToan = "Chưa thanh toán";
                dh.TrangThai = "Chưa xử lý";
                dh.TongTien = 0;
                var ldl = database.DaiLies.Where(s => s.MaDL == dh.MaDL).FirstOrDefault();
                dh.DiemGiao = ldl.DiaChi;
                dh.PhieuXuatKho = false;
                dh.XuatHoaDon = false;
                dh.TinhTrangGH = null;
                database.DonHangs.Add(dh);
                database.SaveChanges();
                var loaidl = database.DaiLies.Find(dh.MaDL);
                var ck = database.LoaiDLs.Where(s => s.MaLoaiDL == loaidl.MaLoaiDL).FirstOrDefault();

                for (int i = 1; i <= id; i++)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaCTDH = rd.Next(0, 10000);
                    ctdh.MaDH = dh.MaDH;
                    ctdh.MaSP = Request["MaSP"+i];
                    ctdh.SoLuong =Convert.ToInt32( Request["SoLuong" + i]);
                    //ctdh.ChietKhau = ck.ChietKhau;
                    var dg = database.SanPhams.Where(s => s.MaSP == ctdh.MaSP).FirstOrDefault();
                    if (ctdh.ChietKhau == null)
                    {
                        ctdh.ThanhTien = (dg.Gia) * (ctdh.SoLuong);
                    }
                    //else
                    //{
                    //    ctdh.ThanhTien = (ctdh.SoLuong) * (dg.Gia) * (ctdh.ChietKhau);
                    //}
                    //Kiểm tra số lượng sản phẩm tồn trong kho
                    var checkslp = database.SanPhams.Where(s => s.MaSP == ctdh.MaSP).FirstOrDefault();
                    if (ctdh.SoLuong > checkslp.TongTon)
                    {
                        database.DonHangs.Remove(dh);
                        TempData["messageAlert"] = "Không đủ số lượng để đặt";
                        return RedirectToAction("QuanLyDH");
                    }
                    else
                    {
                        //var ctk = database.ChiTietKhoes.Where(s => s.MaSP == ctdh.MaSP).ToList();
                        //var slsp = (int)ctdh.SoLuong;
                        //var checksl = -slsp;
                        //foreach (var udk in ctk)
                        //{
                        //    checksl += (int)udk.SoLuong;
                        //    if (checksl >= 0)
                        //    {
                        //        udk.SoLuong = checksl;
                        //        if (udk.SoLuong < 1500)
                        //        {
                        //            udk.TinhTrang = "Sắp hết hàng";
                        //        }
                        //        database.Entry(udk).State = System.Data.Entity.EntityState.Modified;
                        //        database.SaveChanges();
                        //        break;
                        //    }
                        //    else if (checksl < 0)
                        //    {
                        //        udk.TinhTrang = "Hết hàng";
                        //        udk.SoLuong = 0;
                        //        database.Entry(udk).State = System.Data.Entity.EntityState.Modified;
                        //        database.SaveChanges();
                        //    }
                        //}
                    }
                    dh.TongTien += ctdh.ThanhTien;
                    database.ChiTietDonHangs.Add(ctdh);
                    database.SaveChanges();
                }
                dh.TongTien = dh.TongTien - (dh.TongTien * ck.ChietKhau);
                if (ldl.MaLoaiDL == "LDL01")
                {
                    if (dh.TongTien > 500000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = dh.MaDH;
                        TempData["madl"] = dh.MaDL;
                        database.DonHangs.Remove(dh);
                        database.SaveChanges();
                        return RedirectToAction("QuanLyDH");
                    }
                }
                else if (ldl.MaLoaiDL == "LDL02")
                {
                    if (dh.TongTien > 250000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = dh.MaDH;
                        TempData["madl"] = dh.MaDL;
                        database.DonHangs.Remove(dh);
                        database.SaveChanges();
                        return RedirectToAction("QuanLyDH");
                    }
                }
                else if (ldl.MaLoaiDL == "LDL03")
                {
                    if (dh.TongTien > 100000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = dh.MaDH;
                        TempData["madl"] = dh.MaDL;
                        database.DonHangs.Remove(dh);
                        database.SaveChanges();
                        return RedirectToAction("QuanLyDH");
                    }
                }
                dh.TongTien = dh.TongTien - (dh.TongTien * ck.ChietKhau);
                database.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();

                TempData["messageAlert"] = "Đã thêm mới đơn hàng";
                TempData["themmadh"] = dh.MaDH;

                //Gửi email đã lên đơn hàng thành công
                //Lấy thông tin kh
                var ttkh = database.DaiLies.Find(dh.MaDL);
                var tongtien = string.Format("{0:0,0 vnđ}", dh.TongTien);
                //Lấy thông tin IP
                string ipAddress = Request.UserHostAddress;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/NewOrder.html"));
                content = content.Replace("{{Username}}", ttkh.UserName);
                content = content.Replace("{{IdOrder}}", dh.MaDH);
                content = content.Replace("{{datetime}}", DateTime.Now.ToString());
                content = content.Replace("{{TenDL}}", ttkh.TenDL);
                content = content.Replace("{{MyIP}}", ipAddress);
                content = content.Replace("{{emailntt}}", ttkh.Email);
                content = content.Replace("{{email}}", ttkh.Email);
                content = content.Replace("{{TinhTrang}}", dh.TrangThai);
                content = content.Replace("{{SDT}}", ttkh.SDT);
                content = content.Replace("{{Tongtien}}", tongtien);
                content = content.Replace("{{chietkhau}}", ck.ChietKhau.ToString());
                content = content.Replace("{{DCGH}}", ttkh.DiaChi);
                //content = content.Replace("{{Total}}", tongtien);
                //content = content.Replace("{{Thoigian}}", Convert.ToString(ttkh.CreateDate));
                //content = content.Replace("{{Invoice}}", themhd.MaHD);
                string subject = "Đại lý phân phối nước ngọt - Đơn hàng mới "+dh.MaDH;
                WebMail.Send(ttkh.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
                return RedirectToAction("QuanLyDH");

            }
            TempData["messageAlert"] = "Không đủ số lượng để đặt";
            return RedirectToAction("QuanLyDH");
        }
        public void LuudhvaoDB()
        {
            DonHang donHang = new DonHang();
            
            DataTable dh = Session["DonHang"] as DataTable;
            foreach (DataRow dr in dh.Rows)
            {
                donHang.MaDH = dr["MaDH"].ToString();
                donHang.MaDL = dr["MaDL"].ToString();
                donHang.MaNVLap = dr["MaNVLap"].ToString();
                var date = Convert.ToDateTime(dr["NgayLap"]);
                donHang.NgayLap = date;
                donHang.TrangThai = dr["TrangThai"].ToString();
                donHang.TinhTrangThanhToan = dr["TinhTrangThanhToan"].ToString();
                donHang.DiemGiao = dr["DiemGiao"].ToString();
            }
            database.DonHangs.Add(donHang);
            database.SaveChanges();
        }
        //public ActionResult DatThemCTDH()
        //{

        //    ChiTietDonHang prodouct = (ChiTietDonHang)database.ChiTietDonHangs.Where(s=>s.MaDH==id).FirstOrDefault();  // tim sp theo sanPhamID
        //    if (prodouct == null)
        //    {
        //        LuudhvaoDB();
        //    }
        //    ChiTietDonHang newItem = new ChiTietDonHang()
        //    {
        //        MaDH = id,
        //        MaSP = prodouct.MaSP,
        //        SoLuong = prodouct.SoLuong,
        //        DonGia = prodouct.DonGia,
        //        ChietKhau = prodouct.ChietKhau,
        //        ThanhTien = (prodouct.DonGia * prodouct.SoLuong),
        //        DiemGiao = prodouct.DiemGiao,
        //        DonViTinh = prodouct.DonViTinh
        //    };
        //    database.ChiTietDonHangs.Add(newItem);
        //    database.SaveChanges();
        //    // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
        //    return RedirectToAction("ThemCTHD");
        //}
        //Chỉnh sửa chi tiết đơn hàng
        // GET: ChiTietDonHangs/Edit/5
        public ActionResult ChinhSuaCTDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var madh = database.ChiTietDonHangs.Where(s => s.MaSP == id).FirstOrDefault();
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
                var dongia = database.SanPhams.Where(s => s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                chiTietDonHang.MaCTDH = (int)Session["MaCTDH"];
                if (chiTietDonHang.ChietKhau == null)
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (dongia.Gia);
                }
                else
                {
                    chiTietDonHang.ThanhTien = (chiTietDonHang.SoLuong) * (dongia.Gia) * (chiTietDonHang.ChietKhau);
                }
                chiTietDonHang.MaDH = (string)Session["madh"];
                TempData["messageAlert"] = "Đã cập nhật chi tiết đơn hàng";
                TempData["capnhatdh"] = chiTietDonHang.MaDH;
                database.Entry(chiTietDonHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("DanhSachCTDH","KinhDoanh",new {id = chiTietDonHang.MaDH });
            }
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        //Hiển thị danh sách CTDH
        public ActionResult DanhSachCTDH(string id)
        {
            TempData["madh"] = id;
            return View(database.ChiTietDonHangs.ToList().Where(s=>s.MaDH == id));
        }
        public ActionResult ThemCTHD1()
        {

            ViewBag.MaDH = new SelectList(database.DonHangs, "MaDH", "MaDL");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemCTHD2([Bind(Include = "MaDH,MaSP,SoLuong")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                int sl = int.Parse (Request["sl"]);
                double total = 0;
                var dh = database.DonHangs.Find(chiTietDonHang.MaDH);
                var ldl = database.DaiLies.Where(s => s.MaDL == dh.MaDL).FirstOrDefault();
                var ck = database.LoaiDLs.Where(s => s.MaLoaiDL == ldl.MaLoaiDL).FirstOrDefault();
                Random rd = new Random();
                for (int i = 1; i <= sl; i++)
                {
                    chiTietDonHang.MaSP = Request["MaSP" + i];
                    var soluong = Convert.ToInt32(Request["SoLuong" + i]);
                    var dg = database.SanPhams.Where(s => s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                    //Kiểm tra số lượng sản phẩm tồn trong kho
                    var checkslp = database.SanPhams.Where(s => s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                    if (soluong > checkslp.TongTon)
                    {
                        TempData["messageAlert"] = "Không đủ số lượng để đặt";
                        TempData["slmasp"] = chiTietDonHang.MaSP;
                        return RedirectToAction("DanhSachCTDH", "KinhDoanh", new { id = chiTietDonHang.MaDH });
                    }
                    var checkdh = database.ChiTietDonHangs.Where(s => s.MaDH == chiTietDonHang.MaDH && s.MaSP == chiTietDonHang.MaSP).FirstOrDefault();
                    if (checkdh != null)
                    {
                        chiTietDonHang = checkdh;
                        //Nếu trùng sản phẩm
                        chiTietDonHang.MaCTDH = checkdh.MaCTDH;
                        chiTietDonHang.SoLuong = checkdh.SoLuong + soluong;
                        chiTietDonHang.ThanhTien = (dg.Gia) * (chiTietDonHang.SoLuong);
                        database.Entry(chiTietDonHang).State = System.Data.Entity.EntityState.Modified;
                        database.SaveChanges();
                    }
                    else
                    {
                        chiTietDonHang.MaCTDH = rd.Next(0, 100000);
                        chiTietDonHang.SoLuong = soluong;
                        chiTietDonHang.ThanhTien = (dg.Gia) * (soluong);
                        database.ChiTietDonHangs.Add(chiTietDonHang);
                        database.SaveChanges();
                    }
                }
                //Kiểm tra tổng tiền hiện tại đơn hàng
                var checktongtien = database.ChiTietDonHangs.Where(s => s.MaDH == chiTietDonHang.MaDH).ToList();
                foreach(var item01 in checktongtien)
                {
                    total += (double) item01.ThanhTien;
                }
                dh.TongTien = total;
                if (ldl.MaLoaiDL == "LDL01")
                {
                    if (dh.TongTien > 500000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = chiTietDonHang.MaDH;
                        TempData["madl"] = dh.MaDL;
                        return RedirectToAction("QuanLyDH");
                    }
                }
                else if (ldl.MaLoaiDL == "LDL02")
                {
                    if (dh.TongTien > 250000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = dh.MaDH;
                        TempData["madl"] = dh.MaDL;
                        return RedirectToAction("QuanLyDH");
                    }
                }
                else if (ldl.MaLoaiDL == "LDL03")
                {
                    if (dh.TongTien > 100000000)
                    {
                        TempData["messageAlert"] = "error01";
                        TempData["themmadh"] = dh.MaDH;
                        TempData["madl"] = dh.MaDL;
                        return RedirectToAction("QuanLyDH");
                    }
                }
                dh.TongTien = dh.TongTien - (dh.TongTien * ck.ChietKhau);
                //Cập nhật lại tổng tiền của đơn hàng
                //dh.TongTien += chiTietDonHang.ThanhTien;
                database.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["messageAlert"] = "Đã cập nhật chi tiết đơn hàng";
                TempData["capnhatdh"] = chiTietDonHang.MaDH;
                TempData["messageAlert"] = "Đã cập nhật chi tiết đơn hàng";
                return RedirectToAction("DanhSachCTDH","KinhDoanh",new { id= chiTietDonHang.MaDH });
            }

            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }
        //Check tổng tiền trước
        [HttpPost]
        public ActionResult CheckTongTien(string MaSP, int SoLuong)
        {
            var total = 0;
            var checksp = database.SanPhams.Where(s => s.MaSP == MaSP).FirstOrDefault(); 
            var tongton = checksp.TongTon;
            total = (int)(checksp.Gia * SoLuong);
            return Json(new { data=total,tongton},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CheckCongNo(string MaDH, string ThanhTien)
        {
            var tongtiendh = 0;
            var checkdh = database.DonHangs.Where(s => s.MaDH == MaDH).FirstOrDefault();
            var daily = database.DaiLies.Where(s => s.MaDL == checkdh.MaDL).FirstOrDefault();
            var ctdh = database.ChiTietDonHangs.Where(s => s.MaDH == MaDH);
            foreach(var item in ctdh)
            {
                var sp = database.SanPhams.Where(s => s.MaSP == item.MaSP).FirstOrDefault();
                tongtiendh += Convert.ToInt32(sp.Gia * item.SoLuong);
            }
            tongtiendh += Convert.ToInt32(ThanhTien);
            if (daily.MaLoaiDL == "LDL01" && tongtiendh <=500000000)
            {
                return Json(new { success=true }, JsonRequestBehavior.AllowGet);
            }
            else if (daily.MaLoaiDL == "LDL02" && tongtiendh <= 250000000)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }else if (daily.MaLoaiDL == "LDL03" && tongtiendh <= 100000000)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
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
        [HttpPost,ActionName("ChinhSuaDH")]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaDH(DonHang donHang)
        {
            ChiTietDonHang ct = new ChiTietDonHang();
            if (ModelState.IsValid)
            {
                //var madh = database.ChiTietDonHangs.Where(s => s.MaDH == donHang.MaDH).ToList();
                //var total = 0;
                //foreach (var item in madh)
                //{
                //    total += Convert.ToInt32(item.ThanhTien);
                //};
                //donHang.TongTien = total;
                database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
            }
            return View(donHang);
        }
        //Xóa chi tiết đơn hàng
        // GET: ChiTietDonHangs/Delete/5
        public ActionResult XoaCTDH(string id)
        {
            var madh = database.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault();
            if (madh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(madh);
        }
        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("XoaCTDH")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaCTDH1(string id)
        {
            var madh = database.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault();

            database.ChiTietDonHangs.Remove(madh);
            database.SaveChanges();

            var dh = database.DonHangs.Find(id);
            var ldl = database.DaiLies.Where(s => s.MaDL == dh.MaDL).FirstOrDefault();
            var ck = database.LoaiDLs.Where(s => s.MaLoaiDL == ldl.MaLoaiDL).FirstOrDefault();
            double total = 0;
            var checktongtien = database.ChiTietDonHangs.Where(s => s.MaDH == id).ToList();
            foreach (var item01 in checktongtien)
            {
                total += (double)item01.ThanhTien;
            }

            
            dh.TongTien = total - (total*ck.ChietKhau);

            database.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QuanLyDH");
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
            var daily = database.DaiLies.Find(donHang.MaDL);
            TempData["capnhatdh"] = donHang.MaDH;
            database.DonHangs.Remove(donHang);
            database.SaveChanges();
            TempData["messageAlert"] = "Đã xóa đơn hàng";
            
            //Gửi emil về tk
            string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/CancleOrder.html"));
            content = content.Replace("{{Username}}", daily.UserName);
            content = content.Replace("{{Madh}}", donHang.MaDH);
            content = content.Replace("{{datetime}}", DateTime.Now.ToString());
            content = content.Replace("{{tinhtrangTT}}", donHang.TrangThai);
            content = content.Replace("{{TrangThaiGH}}", donHang.TinhTrangGH);
            //content = content.Replace("{{Total}}", tongtien);
            //content = content.Replace("{{Thoigian}}", Convert.ToString(ttkh.CreateDate));
            //content = content.Replace("{{Invoice}}", themhd.MaHD);
            string subject = "ĐƠN HÀNG "+donHang.MaDH+" ĐÃ ĐƯỢC HỦY";
            WebMail.Send(daily.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
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

        //Hàm lưu ảnh (Khi chuyển sang phòng khác nhớ đổi SanPham thành tên đối tượng cần được lưu ảnh  )
        public void LuuAnh(SanPham sp , HttpPostedFileBase HinhAnh)
        {
            #region Hình ảnh
            //Xác định đường dẫn lưu file : Url tương đói => tuyệt đói
            var urlTuongdoi = "/Data/Images/";
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
            sp.HinhAnh = urlTuongdoi + Path.GetFileName(fullDuongDan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSP([Bind(Include = "TenSP,DonViTinh,Gia,HanSD,NgaySX,HinhAnh")] SanPham sanPham, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                LuuAnh(sanPham, HinhAnh);
                Random rd = new Random();
                var themSP = "SP" + rd.Next(0,9)+rd.Next(0,9)+rd.Next(0,9);
                
                sanPham.MaSP = themSP;
                database.SanPhams.Add(sanPham);
                database.SaveChanges();
                TempData["messageAlert"] = "Đã thêm mới sản phẩm";
                TempData["maspTT"] = sanPham.MaSP;
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
        public ActionResult ChinhSuaSP(SanPham sanPham, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if(HinhAnh == null)
                {
                    var hac = database.SanPhams.AsNoTracking().Where(s=>s.MaSP==sanPham.MaSP).FirstOrDefault();
                    sanPham.HinhAnh = hac.HinhAnh;
                    database.Entry(sanPham).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["messageAlert"] = "Đã cập nhật sản phẩm";
                    TempData["maspTT"] = sanPham.MaSP;
                }
                else
                {
                    LuuAnh(sanPham, HinhAnh);
                    database.Entry(sanPham).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["messageAlert"] = "Đã cập nhật sản phẩm";
                    TempData["maspTT"] = sanPham.MaSP;
                }
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

                TempData["messageAlert"] = "Đã xóa sản phẩm";
                TempData["maspTT"] = sanPham.MaSP;
                return RedirectToAction("QuanLySP");
            }
            catch
            {
                return Content("Không xóa được dữ liệu");
            };

        }
        public ActionResult QuanLyHD()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s=>s.NgayLap));
        }
        public ActionResult QuanLyKho()
        {
            return View();
        }
        //Tiếp nhận phiếu đề nghị nhập/ xuất hàng hóa
        public ActionResult TiepNhanHH()
        {
            var dstiepnhan = database.PhieuNhapXuats.ToList();
            return View(dstiepnhan);
        }
        //Phiếu nhập kho
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

        //Phiếu xuất kho
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
        //Xét duyệt phiếu
        public ActionResult XetDuyetPhieu(string MaPhieu)
        {
            var check = database.PhieuNhapXuats.Where(s => s.MaPhieu == MaPhieu).FirstOrDefault();
            if(check != null)
            {
                check.TinhTrang = "Đã xét duyệt";
                database.Entry(check).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Json(new { success = true,MaPhieu=check.MaPhieu }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        // Báo cáo
        public ActionResult BaoCao()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BaoCao(BaoCao baocao)
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
                return RedirectToAction("TrangChu");
            }

            return View(baocao);
        }
        public void Hamrandom( )
        {

        }
        public ActionResult TuChoiTruyCap()
        {
            return View();
        }
        // Hình ảnh
        
    }
}