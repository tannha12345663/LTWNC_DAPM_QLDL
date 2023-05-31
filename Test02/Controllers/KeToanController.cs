using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Test02.App_Start;
using Test02.Models;



namespace Test02.Controllers
{
    [Authentication(MaChucVu = "NVKT")]
    public class KeToanController : Controller
    {
        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
        // GET: KeToan
        public ActionResult Index()
        {
            return View();
        }

        public double maxno(string maloai)
        {
            double maxcn = 0;
            if (maloai == "LDL01")
            {
                maxcn = 500000000;
            }
            else if (maloai == "LDL02")
            {
                maxcn = 250000000;
            }
            else
            {
                maxcn = 100000000;
            }
            return maxcn;
        }
        public int SoTinhTrangDaiLy()
        {
            int countdl = 0;
            List<DaiLy> dly = database.DaiLies.ToList();
            foreach (var item in dly)
            {
                double? a = database.sp_SumCN(item.MaDL).FirstOrDefault();
                double maxcndl = maxno(item.MaLoaiDL);
                double dangercn = maxcndl - (maxcndl * 0.2);
                if (a >= dangercn)
                {
                    countdl++;
                }
            }
            return countdl;
        }





        public ActionResult TrangChuKeToan()
        {

            ViewBag.tongdathanhtoan = database.sp_DHtheotinhtrang("Đã thanh toán").FirstOrDefault();
            ViewBag.tongchuathanhtoan = database.sp_DHtheotinhtrang("Chưa thanh toán").FirstOrDefault();
            ViewBag.tongdonno = database.sp_DHtheotinhtrang("Đang nợ").FirstOrDefault();
            ViewBag.donthanhtoan = database.sp_DonHangtrongngay("Đã thanh toán").FirstOrDefault();
            ViewBag.donchuathanhtoan = database.sp_DonHangtrongngay("Chưa thanh toán").FirstOrDefault();
            ViewBag.donno = database.sp_DonHangtrongngay("Đang nợ").FirstOrDefault();

            int dem = SoTinhTrangDaiLy();
            Session["demdl"] = dem;
            Session["doanhthu"] = database.sp_DoanhThutrongthang().FirstOrDefault();
            List<sp_CongNoDenHan_Result> cnhan = database.sp_CongNoDenHan().ToList();
            Session["cndenhan"] = cnhan;
            return View();
        }



        public List<DonHang> LocDonHang_TheoDK(string thang_chon, List<DonHang> tatcadonhang, string ttrang)
        {

            List<DonHang> dh = new List<DonHang>();
            if (thang_chon != "0")
            {
                foreach (var locdh in tatcadonhang)
                {
                    DateTime day = (DateTime)locdh.NgayLap;
                    var m = Convert.ToString(day.Month);

                    if (thang_chon == m && locdh.TinhTrangThanhToan == ttrang)
                    {
                        dh.Add(locdh);
                    }
                }
            }
            else
            {
                foreach (var locdh in tatcadonhang)
                {
                    if (locdh.TinhTrangThanhToan == ttrang)
                    {
                        dh.Add(locdh);
                    }
                }

            }
            return dh;

        }
        public List<DonHang> LayDonHang_TheoThang(string thang_chon, List<DonHang> tatcadonhang)
        {

            List<DonHang> dh = new List<DonHang>();
            if (thang_chon != "0")
            {
                foreach (var locdh in tatcadonhang)
                {
                    DateTime day = (DateTime)locdh.NgayLap;
                    var m = Convert.ToString(day.Month);

                    if (thang_chon == m)
                    {
                        dh.Add(locdh);
                    }
                }
            }
            else
            {
                dh = tatcadonhang;
            }
            return dh;

        }
        private void BindDropDownList()
        {
            List<SelectListItem> m = new List<SelectListItem>();
            m.Add(new SelectListItem { Text = "All", Value = "All" });
            m.Add(new SelectListItem { Text = "1", Value = "1" });
            m.Add(new SelectListItem { Text = "2", Value = "2" });
            m.Add(new SelectListItem { Text = "3", Value = "3" });
            m.Add(new SelectListItem { Text = "4", Value = "4" });
            m.Add(new SelectListItem { Text = "5", Value = "5" });
            m.Add(new SelectListItem { Text = "6", Value = "6" });
            m.Add(new SelectListItem { Text = "7", Value = "7" });
            m.Add(new SelectListItem { Text = "8", Value = "8" });
            m.Add(new SelectListItem { Text = "9", Value = "9" });
            m.Add(new SelectListItem { Text = "10", Value = "10" });
            m.Add(new SelectListItem { Text = "11", Value = "11" });
            m.Add(new SelectListItem { Text = "12", Value = "12" });
            TempData["thangs"] = m;
        }
        private void Tinhtangdonhang()
        {
            List<SelectListItem> tinhtrang = new List<SelectListItem>();
            tinhtrang.Add(new SelectListItem { Text = "All", Value = "All" });
            tinhtrang.Add(new SelectListItem { Text = "Chưa thanh toán", Value = "Chưa thanh toán" });
            tinhtrang.Add(new SelectListItem { Text = "Đang nợ", Value = "Đang nợ" });
            tinhtrang.Add(new SelectListItem { Text = "Đã thanh toán", Value = "Đã thanh toán" });
            TempData["tinhtrangdh"] = tinhtrang;
        }
        public ActionResult QLDonHang()
        {
            BindDropDownList();
            Tinhtangdonhang();
            List<sp_LocDonHang_Result> dh = database.sp_LocDonHang("All", "All", "Đã xét duyệt").ToList();
            return View(dh);
        }
        [HttpPost]
        public ActionResult QLDonHang(string thangs, string tinhtrangdh)
        {
            BindDropDownList();
            Tinhtangdonhang();
            List<sp_LocDonHang_Result> dh = database.sp_LocDonHang(thangs, tinhtrangdh, "Đã xét duyệt").ToList();
            return View(dh);
        }

        //public ActionResult QLDonChoGiao()
        //{
        //    BindDropDownList();
        //    Tinhtangdonhang();
        //    List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
        //    && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH == "Chờ giao").ToList();

        //    return View(donhangchuathanhtoan);
        //}
        //[HttpPost]
        //public ActionResult QLDonChoGiao(string thangs, string tinhtrangdh)
        //{

        //    BindDropDownList();
        //    Tinhtangdonhang();
        //    List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" 
        //   && s.TinhTrangGH == "Chờ giao").ToList();
        //    List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();
        //    return View(dhchon);
        //}

        //public ActionResult QLDonDaGiao()
        //{
        //    BindDropDownList();
        //    Tinhtangdonhang();
        //    List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
        //    && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH == "Đã giao").ToList();

        //    return View(donhangchuathanhtoan);
        //}
        //[HttpPost]
        //public ActionResult QLDonDaGiao(string thangs, string tinhtrangdh)
        //{

        //    BindDropDownList();
        //    Tinhtangdonhang();
        //    List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
        //   && s.TinhTrangGH=="Đã giao").ToList();
        //    List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();
        //    return View(dhchon);
        //}


        //public ActionResult ChinhsuaHD(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    DonHang donHang = database.DonHangs.Find(id);
        //    if (donHang == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(donHang);
        //}





        public ActionResult XuatHDBH(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonHang donHang = database.DonHangs.Find(id);
            Session["madaily"] = id;
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        public ActionResult ChinhsuaHD(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["edithd"] = "oke";
                return RedirectToAction("XuatHDBH/" + donHang.MaDH);
            }

            return View(donHang);
        }



        //private double TinhTongDonHang(List<DonHang> donhang)
        //{
        //    double tinhtongdh = 0;
        //    //= database.DonHangs.Where(s => s.MaDL == madl && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TrangThai == "Đã xét duyệt").ToList();
        //    if (donhang != null)
        //    {
        //        foreach (var item in donhang)
        //        {
        //            tinhtongdh += (double)item.TongTien;

        //        }
        //    }
        //    return tinhtongdh;

        //}
        public List<DonHang> Get_DonHang(string thangs, string thanhtoan, string trangthai)
        {
            var dh = database.sp_LocDonHang(thangs, thanhtoan, trangthai).ToList();
            List<DonHang> dsdh = new List<DonHang>();
            foreach (var item in dh)
            {
                var don = database.DonHangs.Where(s => s.MaDH == item.MaDH).FirstOrDefault();
                dsdh.Add(don);
            }
            return dsdh;
        }
        public int KtraDH(List<DonHang> dh)
        {
            int c = 0;
            for (int i = 0; i < dh.Count - 1; i++)
            {
                DateTime m = (DateTime)dh[i].NgayLap;
                DateTime m1 = (DateTime)dh[i + 1].NgayLap;
                if (m.Month != m1.Month)
                    c++;
            }
            return c;
        }

        public int KtraThang(List<DonHang> dh)
        {
            int thang = 0;
            if (dh.Count != 0)
            {
                DateTime thangdau = (DateTime)dh.Take(1).FirstOrDefault().NgayLap;
                thang = thangdau.Month;
            }
            return thang;
        }

        public ActionResult DSDaiLy()
        {
            database.sp_capnhat();
            return View(database.DaiLies.ToList().OrderBy(s => s.MaDL));
        }


        public ActionResult CongnoDL(string id)
        {
            BindDropDownList();
            List<DonHang> dh = Get_DonHang("All", "Chưa thanh toán", "Đã xét duyệt").Where(s => s.MaDL == id && s.TinhTrangGH == "Đã giao").ToList();
            ViewBag.TongCN = database.sp_TienDHNoDaiLy(id, "All").FirstOrDefault();
            Session["Madly"] = id;
            Session["thangtrongdonhang"] = KtraThang(dh);
            Session["KtraDH"] = KtraDH(dh);
            return View(dh);
        }
        [HttpPost]
        public ActionResult CongnoDL(string id, string thangs)
        {
            BindDropDownList();
            List<DonHang> dh = Get_DonHang(thangs, "Chưa thanh toán", "Đã xét duyệt").Where(s => s.MaDL == id && s.TinhTrangGH == "Đã giao").ToList();
            ViewBag.TongCN = database.sp_TienDHNoDaiLy(id, thangs).FirstOrDefault();
            Session["KtraDH"] = KtraDH(dh);
            Session["Madly"] = id;
            Session["thangtrongdonhang"] = KtraThang(dh);
            return View(dh);

        }


        public JsonResult KtraNgayLapCN(DateTime ngaylap, string thanglap)
        {
            System.Threading.Thread.Sleep(200);
            int t = ngaylap.Month;


            if (t != int.Parse(thanglap))
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult KtraHanCN(DateTime ngaylap, DateTime hantra)
        {
            System.Threading.Thread.Sleep(200);
            TimeSpan Time = hantra - ngaylap;
            int TongSoNgay = Time.Days;
            if (TongSoNgay > 30 || TongSoNgay <= 0)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }

        //private List<DonHang> Xet_DonHang(string id, List<DonHang> dhchon)
        //{
        //    List<DonHang> dhmoi = new List<DonHang>();
        //    foreach (var dh in dhchon)
        //    {
        //        if (dh.MaDL == id)
        //        {
        //            dhmoi.Add(dh);
        //            dh.TinhTrangThanhToan = "Đang nợ";
        //        }
        //    }
        //    return dhmoi;
        //}
        //[HttpPost]

        public ActionResult TaoCongno(string MaDL, PhieuCongNo phieuCongNo, DateTime Hantra, DateTime NgayLapCN)
        {
            System.Threading.Thread.Sleep(5000);
            try
            {
                var c = database.DaiLies.Where(s => s.MaDL == MaDL).ToList().FirstOrDefault();
                string thanglapcn = NgayLapCN.Month.ToString();
                var tienno = database.sp_TienDHNoDaiLy(MaDL, thanglapcn).FirstOrDefault();
                database.sp_CongnoDLy(MaDL, tienno, Hantra, NgayLapCN, thanglapcn);
            }
            catch
            {
                TempData["taocn"] = "loicn";
                return RedirectToAction("DSDaiLy");
            }

            TempData["taocn"] = "success";
            return RedirectToAction("DSDaiLy");

        }
        //Cong no chua thanh toan
        private void TinhtangCongno()
        {
            List<SelectListItem> tinhtrang = new List<SelectListItem>();
            tinhtrang.Add(new SelectListItem { Text = "All", Value = "All" });
            tinhtrang.Add(new SelectListItem { Text = "Chưa thanh toán", Value = "Chưa thanh toán" });
            tinhtrang.Add(new SelectListItem { Text = "Đã thanh toán", Value = "Đã thanh toán" });
            TempData["tinhtrangcn"] = tinhtrang;
        }

        //public List<PhieuCongNo> LocCongno_TheoDK(string thang_chon, List<PhieuCongNo> tatcacn, string ttrang)
        //{

        //    List<PhieuCongNo> congno = new List<PhieuCongNo>();
        //    if (thang_chon != "0")
        //    {
        //        foreach (var locdh in tatcacn)
        //        {
        //            DateTime day = (DateTime)locdh.NgayLapCN;
        //            var m = Convert.ToString(day.Month);

        //            if (thang_chon == m && locdh.TrangThai == ttrang)
        //            {
        //                congno.Add(locdh);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (var locdh in tatcacn)
        //        {
        //            if (locdh.TrangThai == ttrang)
        //            {
        //                congno.Add(locdh);
        //            }
        //        }

        //    }
        //    return congno;

        //}
        public ActionResult DSCongnoNo(String id)
        {
            Session["madailycn"] = id;
            BindDropDownList();
            TinhtangCongno();
            List<sp_LocPhieuCongNo_Result> dscongno = database.sp_LocPhieuCongNo("All", "All", id).ToList();
            return View(dscongno);

        }
        [HttpPost]
        public ActionResult DSCongnoNo(String id, string thangs, string tinhtrangcn)
        {
            BindDropDownList();
            TinhtangCongno();
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id).ToList();
            List<sp_LocPhieuCongNo_Result> cn = database.sp_LocPhieuCongNo(thangs, tinhtrangcn, id).ToList();
            Session["madailycn"] = id;
            return View(cn);

        }

        //GUI MAIL
        public ActionResult SendMailCN(string id)
        {
            var cn = database.PhieuCongNoes.Where(t => t.MaCongNo == id).FirstOrDefault();
            var daily = database.DaiLies.Where(t => t.MaDL == cn.MaDL).FirstOrDefault();
            string content = System.IO.File.ReadAllText(Server.MapPath("~/TemplateMail/Doino.html"));

            content = content.Replace("{{Username}}", daily.TenDL);
            content = content.Replace("{{MaCN}}", cn.MaCongNo);
            content = content.Replace("{{Tienno}}", cn.TienNo.ToString());
            content = content.Replace("{{NgayLap}}", cn.NgayLapCN.ToString());
            content = content.Replace("{{Han}}", cn.HanTra.ToString());
            string subject = "Thư báo công nợ chưa thanh toán";

            WebMail.Send(daily.Email, subject, content, null, null, null, true, null, null, null, null, null, null);
            TempData["baono"] = "thanhcong";
            return RedirectToAction("ChitietCN", "KeToan", new { id = id });
        }

        public ActionResult ChinhsuCN(string id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuCN(string id, PhieuCongNo phieuCongNo)
        {
            if (ModelState.IsValid)
            {
                database.Entry(phieuCongNo).State = System.Data.Entity.EntityState.Modified;
                List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == phieuCongNo.MaDL
                 && s.TinhTrangThanhToan == "Đang nợ"
                 && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
                if (phieuCongNo.TrangThai == "Đã thanh toán")
                {
                    foreach (var item in donhang)
                    {
                        item.TinhTrangThanhToan = "Đã thanh toán";
                    }
                }
                database.SaveChanges();
                TempData["chinhsuacn"] = "chinhthanhcong";
                return RedirectToAction("DSDaiLy");
            }
            return View(phieuCongNo);
        }


        public ActionResult XoaCN(string id, PhieuCongNo phieuCongNo)
        {
            System.Threading.Thread.Sleep(5000);

            try
            {
                database.sp_XoaCN(id);
                TempData["huyxoacn"] = "daxoa";
                return RedirectToAction("DSCongnoNo/" + id);

            }
            catch
            {
                TempData["huyxoacn"] = "khongxoa";
                return RedirectToAction("DSCongnoNo/" + id);
            }

        }
        public ActionResult ChitietCN(String id)
        {
            var phieucn = database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault();
            List<DonHang> donhang = new List<DonHang>();
            if (phieucn.TrangThai == "Chưa thanh toán")
            {
                donhang = database.DonHangs.Where(s => s.TinhTrangThanhToan == "Đang nợ"
            && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
            }
            else
            {
                donhang = database.DonHangs.Where(s => s.TinhTrangThanhToan == "Đã thanh toán" && s.TrangThai == "Đã xét duyệt"
                && s.TinhTrangGH == "Đã giao").ToList();
            }
            Session["donhangcheck"] = donhang;
            return View(phieucn);
        }


        public ActionResult Doanhthu()
        {
            BindDropDownList();
            List<DonHang> dhthanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            && s.TinhTrangThanhToan == "Đã thanh toán" && s.TinhTrangGH == "Đã giao").ToList();
            return View(dhthanhtoan);
        }
        [HttpPost]
        public ActionResult Doanhthu(string thangs)
        {
            BindDropDownList();
            List<DonHang> dhthanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            && s.TinhTrangThanhToan == "Đã thanh toán" && s.TinhTrangGH == "Đã giao").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs, dhthanhtoan);

            return View(dhchon);
        }





        public ActionResult ThongKe()
        {
            List<SanPham> sp = database.SanPhams.OrderBy(s => s.MaSP).ToList();
            return View(sp);
        }







    }
}
