using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Test02.App_Start;
using Test02.Models;



namespace Test02.Controllers
{
    [Authentication(MaChucVu ="NVKT")]
    public class KeToanController : Controller
    {
        

        QuanLyDLEntities2 database = new QuanLyDLEntities2();
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
        //public double tongno(string madl)
        //{
        //    double no = 0;
        //    List<PhieuCongNo> congno = database.PhieuCongNoes.Where(n => n.MaDL == madl
        //        && n.TrangThai == "Chưa thanh toán").ToList();
        //    foreach (var cn in congno)
        //    {
        //        no += (double)cn.TienNo;
        //    }
        //    return no;
        //}
     
        public int SoTinhTrangDaiLy()
        {
            double no = 0;
            int countdl = 0;
            List<DaiLy> dly = database.DaiLies.ToList();
            foreach(var item in dly)
            {
                //no = tongno(item.MaDL);
                var a = database.sp_SumCN(item.MaDL);
                no = 0;
               double maxcndl = maxno(item.MaLoaiDL);
                double dangercn = maxcndl - (maxcndl * 0.1);

                if (no >= dangercn)
                {
                    countdl++;
                }
            }
            return countdl;
        }

        //public double TinhDoanhThu()
        //{
        //    double doanhthu = 0;
        //    DateTime homnay = DateTime.Now;
            
        //    List<DonHang> donhang = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đã thanh toán" 
        //    &&s.TinhTrangGH=="Đã giao").ToList();
        //    foreach(var item in donhang)
        //    {
        //        DateTime thang = (DateTime)item.NgayLap;
        //        if(homnay.Month==thang.Month)
        //        {
        //            doanhthu += (double)item.TongTien;
        //        }    
               
        //    }
        //    return doanhthu;
        //}

        //public List<DonHang> DonTrongNgay()
        //{

        //    string homnay = DateTime.Now.ToString("dd/MM/yyyy");
        //    List<DonHang> donHangs = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt").ToList();
        //    List<DonHang> donhangngay = new List<DonHang>();
        //    foreach (var item in donHangs)
        //    {
        //        DateTime itemngay = (DateTime)item.NgayLap;
        //        string fm = itemngay.ToString("dd/MM/yyyy");
        //        if (homnay == fm)
        //            donhangngay.Add(item);
        //    }


        //    return donhangngay;
        //}

        //public List<PhieuCongNo> CongnodenHan()
        //{
        //    DateTime aDatetime = DateTime.Now;
        //    List<PhieuCongNo> phieucn = database.PhieuCongNoes.Where(s => s.TrangThai == "Chưa thanh toán").ToList();
        //    List<PhieuCongNo> cnhan = new List<PhieuCongNo>();

        //    foreach (var item in phieucn)
        //    {
        //        DateTime hantra = Convert.ToDateTime(item.HanTra);
        //        TimeSpan Time = hantra - DateTime.Now;
        //        int TongSoNgay = Time.Days;
        //        if(TongSoNgay<4)
        //        {
        //            cnhan.Add(item);
        //        }
        //    }
        //    return cnhan;
        //}
        public int? Donhangtrongngay(string tinhtrang) 
        {
            return database.sp_DonHangtrongngay(tinhtrang).FirstOrDefault();
        }
        //Tổng đơn hàng
        public int? Tongdonhang(string tinhtrang)
        {
            return database.sp_DHtheotinhtrang(tinhtrang).FirstOrDefault();
        }
        public ActionResult TrangChuKeToan()
        {
            // List<DonHang> dh = DonTrongNgay();
            //var a = database.sp_DoanhThutrongngay();
            Session["tongdathanhtoan"] = Tongdonhang("Đã thanh toán");
            Session["tongchuathanhtoan"] = Tongdonhang("Chưa thanh toán");
            Session["tongno"] = Tongdonhang("Đang nợ");
            //
            Session["sodontt"] = Donhangtrongngay("Đã thanh toán");
            Session["sodonctt"] = Donhangtrongngay("Chưa thanh toán");
            Session["sodonno"] = Donhangtrongngay("Đang nợ");
            //
            int dem = SoTinhTrangDaiLy();
            Session["demdl"] = dem;
            //
            Session["doanhthu"] = database.sp_DoanhThutrongthang().FirstOrDefault();
            var cnhan = database.sp_CongNoDenHan().ToList();
            List<PhieuCongNo> dscn = new List<PhieuCongNo>();
            foreach (var item in cnhan)
            {
                var cn = database.PhieuCongNoes.Where(s => s.MaCongNo == item.MaCongNo).FirstOrDefault();
                dscn.Add(cn);
            }
            Session["cndenhan"] = dscn;
            return View();
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
            List<DonHang> donhang = database.DonHangs.Where(s=>s.TrangThai=="Đã xét duyệt").ToList();

            return View(donhang);
        }

        public List<DonHang> LocDonHang_TheoDK(string thang_chon, List<DonHang> tatcadonhang,string ttrang)
        {

            List<DonHang> dh = new List<DonHang>();
            if (thang_chon != "0")
            {
                foreach (var locdh in tatcadonhang)
                {
                    DateTime day = (DateTime)locdh.NgayLap;
                    var m = Convert.ToString(day.Month);

                    if (thang_chon == m && locdh.TinhTrangThanhToan==ttrang)
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
        public List<DonHang> LayDonHang_TheoThang(string thang_chon,List<DonHang>tatcadonhang)
        {
           
            List<DonHang> dh = new List<DonHang>();
            if (thang_chon != "0")
            {
                foreach(var locdh in tatcadonhang)
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
        
        [HttpPost]
        public ActionResult QLDonHang(string thangs,string tinhtrangdh)
        {
          
            BindDropDownList();
            Tinhtangdonhang();
            ////List<DonHang> dhchon = new List<DonHang>();
            ////var test01 = database.sp_SapxepNgay().ToList().OrderBy(s=>s.NgayLap);
            //List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            //&& s.TinhTrangGH == null).ToList();
            //List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();
            string trangthai = "Đã xét duyệt";
            var dh = database.sp_LocDonHang(thangs, tinhtrangdh, trangthai).ToList();
            List<DonHang> dsdh = new List<DonHang>();
            foreach (var item in dh)
            {
                var don = database.DonHangs.Where(s => s.MaDH == item.MaDH).FirstOrDefault();
                dsdh.Add(don);
            }
            return View(dsdh);
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


        [HttpPost]
        public ActionResult ChinhsuaHD(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["edithd"] = "oke";
                return RedirectToAction("XuatHDBH", new RouteValueDictionary(
                                       new { controller = "KeToan", action = "XuatHDBH", Id = donHang.MaDH }));

            }
           
            return View(donHang);

        }


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
            //donHang.XuatHoaDon = true;
            //database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
            //database.SaveChanges();

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

        public ActionResult DSDaiLy()
        {
            //database.sp_capnhat();

            return View(database.DaiLies.ToList().OrderBy(s => s.MaDL));

        }
        //public List<DonHang> LayDonHang_ChuaThanhToan(string madl)
        //{
        //    List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == madl 
        //    && s.TinhTrangThanhToan == "Chưa thanh toán" 
        //    && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
        //    return donhang;
        //}
        
        public List<DonHang> Get_DonHang(string thangs,string thanhtoan,string trangthai)
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
            for(int i=0;i<dh.Count-1;i++)
            {
                DateTime m = (DateTime)dh[i].NgayLap;
                DateTime m1 = (DateTime)dh[i+1].NgayLap;
                if (m.Month != m1.Month)
                    c++;
            }
            return c;
        }
        public ActionResult CongnoDL(string id)
        {
            BindDropDownList();
            List<DonHang> dh = Get_DonHang("All","Chưa thanh toán","Đã xét duyệt").Where(s=>s.MaDL==id && s.TinhTrangGH=="Đã giao").ToList();
            ViewBag.TongCN = database.sp_TienDHNoDaiLy(id,"All").FirstOrDefault();
            Session["Madly"] = id;
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
            Session["m"] = thangs;
            return View(dh);

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
        [HttpPost]
        public ActionResult TaoCongno( string MaDL, PhieuCongNo phieuCongNo,DateTime Hantra,DateTime NgayLapCN)
        {
            var c = database.DaiLies.Where(s => s.MaDL == MaDL).ToList().FirstOrDefault();
            string thanglapcn = NgayLapCN.Month.ToString();
            var tienno= database.sp_TienDHNoDaiLy(MaDL, thanglapcn).FirstOrDefault();
            database.sp_TaoCongNo(MaDL, tienno, Hantra, NgayLapCN, thanglapcn);

            //if (t!=null)
            //{
            //    List<DonHang> dhchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && 
            //    s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH=="Đã giao").ToList();
            //    List<DonHang> dhchon = LayDonHang_TheoThang(t, dhchuathanhtoan);
            //    dhnew = Xet_DonHang(MaDL, dhchon);
            //} else
            //{
            //    List<DonHang> dhnull = LayDonHang_ChuaThanhToan(MaDL);
            //    dhnew = Xet_DonHang(MaDL, dhnull);

            //}


            //double tien = 0;
            ////double tien = (double)TinhTongDonHang(dhnew); 
            //Random rd = new Random();
            //var macn = "CN" + rd.Next(1, 9)+ rd.Next(1, 20)+ rd.Next(21, 99);
            //phieuCongNo.MaCongNo = macn;

            //phieuCongNo.TrangThai = "Chưa thanh toán";
            //phieuCongNo.MaDL = MaDL;
            //Session["macndl"] = MaDL;
            //phieuCongNo.TienNo = tien;
            //phieuCongNo.NgayLapCN = NgayLapCN;

            
            //phieuCongNo.HanTra = Hantra;

            
            //database.PhieuCongNoes.Add(phieuCongNo);
            //database.SaveChanges();
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
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id).ToList();
            return View(dscongno);

        }
        [HttpPost]
        public ActionResult DSCongnoNo(String id,string thangs,string tinhtrangcn)
        {
            BindDropDownList();
            TinhtangCongno();
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id).ToList();
            //List<PhieuCongNo> cnchon =LocCongno_TheoDK(thangs, dscongno, tinhtrangcn).ToList();
            var cn = database.sp_LocPhieuCongNo(thangs, tinhtrangcn, id).ToList();
            List<PhieuCongNo> dscn = new List<PhieuCongNo>();
            foreach (var item in cn)
            {
                var congno = database.PhieuCongNoes.Where(s => s.MaDL == item.MaDL).FirstOrDefault();
                dscn.Add(congno);
            }

            Session["madailycn"] = id;
            return View(dscn);

        }
        public ActionResult ChinhsuCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuCN(String id, PhieuCongNo phieuCongNo)
        {
            if (ModelState.IsValid)
            {
                
                database.Entry(phieuCongNo).State = System.Data.Entity.EntityState.Modified;
                List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == phieuCongNo.MaDL
                 && s.TinhTrangThanhToan == "Đang nợ"
                 && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
                if (phieuCongNo.TrangThai=="Đã thanh toán")
                {
                    foreach(var item in donhang)
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
            try
            {
                phieuCongNo = database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault();
                Session["matim"] = phieuCongNo.MaDL;
                if (phieuCongNo.TrangThai=="Đã thanh toán")
                {
                    TempData["huyxoacn"] = "khongxoa";
                    return RedirectToAction("ChitietCN", new RouteValueDictionary(
                                      new { controller = "KeToan", action = "ChitietCN", Id = phieuCongNo.MaCongNo }));

                }    
                
                database.PhieuCongNoes.Remove(phieuCongNo);
                database.SaveChanges();
                
                return RedirectToAction("DSCongnoNo", new RouteValueDictionary(
                                       new { controller = "KeToan", action = "DSCongnoNo", Id = Session["matim"] }));

            }
            catch
            {
                return Content("err");
            }

        }

        //public ActionResult XuatCN(String id)
        //{
            
        //    return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        //}    

        public ActionResult ChitietCN(String id)
        {
            var phieucn = database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault();
            List<DonHang> donhang = new List<DonHang>();
            if (phieucn.TrangThai == "Chưa thanh toán")
            {
                donhang = database.DonHangs.Where(s =>s.TinhTrangThanhToan == "Đang nợ"
            && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
            }
            else
            {
                donhang = database.DonHangs.Where(s =>s.TinhTrangThanhToan == "Đã thanh toán" && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();
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
            && s.TinhTrangThanhToan == "Đã thanh toán" && s.TinhTrangGH=="Đã giao").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs,dhthanhtoan);

            return View(dhchon);
        }





        public ActionResult ThongKe()
        {
            List<SanPham> sp = database.SanPhams.OrderBy(s => s.MaSP).ToList();
            return View(sp);
        }







    }
}