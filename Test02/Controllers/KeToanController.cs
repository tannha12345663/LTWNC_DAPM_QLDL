using System;
using System.Collections.Generic;
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
            //return View(database.HoaDons.ToList());
        }
        public double tongno(string madl)
        {
            double no = 0;
            List<PhieuCongNo> congno = database.PhieuCongNoes.Where(n => n.MaDL == madl
                && n.TrangThai == "Chưa thanh toán").ToList();
            foreach (var cn in congno)
            {
                no += (double)cn.TienNo;
            }
            return no;
        }
        //public int DemDL(double tongno,double maxcndl)
        //{
        //    int count = 0;
            
             
        //    return count;
        //}
        public int SoTinhTrangDaiLy()
        {
            double no = 0;
            int countdl = 0;
            List<DaiLy> dly = database.DaiLies.ToList();
            foreach(var item in dly)
            {
                no = tongno(item.MaDL);
               double maxcndl = maxno(item.MaLoaiDL);
                double dangercn = maxcndl - (maxcndl * 0.1);

                if (no >= dangercn)
                {
                    countdl++;
                }
            }
            return countdl;
        }

        public double TinhDoanhThu()
        {
            double doanhthu = 0;
            DateTime homnay = DateTime.Now;
            
            List<DonHang> donhang = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đã thanh toán" 
            &&s.TinhTrangGH=="Đã giao").ToList();
            foreach(var item in donhang)
            {
                DateTime thang = (DateTime)item.NgayLap;
                if(homnay.Month==thang.Month)
                {
                    doanhthu += (double)item.TongTien;
                }    
               
            }
            return doanhthu;
        }

        public List<DonHang> DonTrongNgay()
        {

            string homnay = DateTime.Now.ToString("dd/MM/yyyy");
            List<DonHang> donHangs = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt").ToList();
            List<DonHang> donhangngay = new List<DonHang>();
            foreach (var item in donHangs)
            {
                DateTime itemngay = (DateTime)item.NgayLap;
                string fm = itemngay.ToString("dd/MM/yyyy");
                if (homnay == fm)
                    donhangngay.Add(item);
            }


            return donhangngay;
        }

        public List<PhieuCongNo> CongnodenHan()
        {
            DateTime aDatetime = DateTime.Now;
            List<PhieuCongNo> phieucn = database.PhieuCongNoes.Where(s => s.TrangThai == "Chưa thanh toán").ToList();
            List<PhieuCongNo> cnhan = new List<PhieuCongNo>();

            foreach (var item in phieucn)
            {
                DateTime hantra = Convert.ToDateTime(item.HanTra);
                TimeSpan Time = hantra - DateTime.Now;
                int TongSoNgay = Time.Days;
                if(TongSoNgay<4)
                {
                    cnhan.Add(item);
                }
            }
            return cnhan;
        }
        public ActionResult TrangChuKeToan()
        {
            List<DonHang> dh = DonTrongNgay();
            Session["dsdonngay"] = dh;
            int dem = SoTinhTrangDaiLy();
            Session["demdl"] = dem;
            
            Session["doanhthu"] = TinhDoanhThu();
            List<PhieuCongNo> cnhan = CongnodenHan();
            Session["cndenhan"] = cnhan;
            return View(database.DonHangs.ToList());
        }
        
        public ActionResult Thongbao()
        {
            return View(database.PhieuCongNoes.ToList());
        }
        private void BindDropDownList()
        {

            List<SelectListItem> m = new List<SelectListItem>();
            m.Add(new SelectListItem { Text = "All", Value = "0" });
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
            tinhtrang.Add(new SelectListItem { Text = "Chưa thanh toán", Value = "Chưa thanh toán" });
            tinhtrang.Add(new SelectListItem { Text = "Đang nợ", Value = "Đang nợ" });
            tinhtrang.Add(new SelectListItem { Text = "Đã thanh toán", Value = "Đã thanh toán" });


            TempData["tinhtrangdh"] = tinhtrang;


        }
        public ActionResult QLDonHang()
        {
            BindDropDownList();
            Tinhtangdonhang();
            List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" 
            && s.TinhTrangThanhToan=="Chưa thanh toán" && s.TinhTrangGH==null).ToList();

            return View(donhangchuathanhtoan);
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
            //List<DonHang> dhchon = new List<DonHang>();
            //var test01 = database.sp_SapxepNgay().ToList().OrderBy(s=>s.NgayLap);

            List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            && s.TinhTrangGH == null).ToList();
            List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();


            return View(dhchon);
        }

        public ActionResult QLDonChoGiao()
        {
            BindDropDownList();
            Tinhtangdonhang();
            List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH == "Chờ giao").ToList();

            return View(donhangchuathanhtoan);
        }
        [HttpPost]
        public ActionResult QLDonChoGiao(string thangs, string tinhtrangdh)
        {

            BindDropDownList();
            Tinhtangdonhang();
            List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" 
           && s.TinhTrangGH == "Chờ giao").ToList();
            List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();
            return View(dhchon);
        }

        public ActionResult QLDonDaGiao()
        {
            BindDropDownList();
            Tinhtangdonhang();
            List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
            && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH == "Đã giao").ToList();

            return View(donhangchuathanhtoan);
        }
        [HttpPost]
        public ActionResult QLDonDaGiao(string thangs, string tinhtrangdh)
        {

            BindDropDownList();
            Tinhtangdonhang();
            List<DonHang> tatca = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt"
           && s.TinhTrangGH=="Đã giao").ToList();
            List<DonHang> dhchon = LocDonHang_TheoDK(thangs, tatca, tinhtrangdh).ToList();
            return View(dhchon);
        }


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




        private double TinhTongDonHang(List<DonHang> donhang)
        {
            double tinhtongdh = 0;
             //= database.DonHangs.Where(s => s.MaDL == madl && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TrangThai == "Đã xét duyệt").ToList();
            if (donhang != null)
            {
                foreach(var item in donhang)
                {
                    tinhtongdh += (double)item.TongTien;
                    
                }    
            }
            return tinhtongdh;

        }

        public ActionResult DSDaiLy()
        {
            database.sp_capnhat();
            return View(database.DaiLies.ToList().OrderBy(s => s.MaDL));
        }
        public List<DonHang> LayDonHang_ChuaThanhToan(string madl)
        {
            List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == madl 
            && s.TinhTrangThanhToan == "Chưa thanh toán" 
            && s.TrangThai == "Đã xét duyệt" && s.TinhTrangGH == "Đã giao").ToList();

            return donhang;
        }
        public int ChuyenThang(DateTime a)
        {
            int b = a.Month;
            return b;
        }

        
        public ActionResult CongnoDL(string id)
        {
            BindDropDownList();

            var c = database.DaiLies.Where(s => s.MaDL == id).ToList().FirstOrDefault();
            List<DonHang> dh = LayDonHang_ChuaThanhToan(id);
            ViewBag.TongCN = TinhTongDonHang(dh);
            Session["Madly"] = id;

            return View(dh);

        }
        [HttpPost]
        public ActionResult CongnoDL(string id, string thangs)
        {
            BindDropDownList();
            var c = database.DaiLies.Where(s => s.MaDL == id).ToList().FirstOrDefault();
            List<DonHang> dhchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" 
            && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH=="Đã giao").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs, dhchuathanhtoan);
            List<DonHang> dhnew = new List<DonHang>();
            foreach (var dh in dhchon)
            {
                if (dh.MaDL == id)
                {
                    dhnew.Add(dh);
                }
            }
            Session["listdh"] = dhnew;
            Session["thanglapcn"] = thangs;
            ViewBag.TongCN = TinhTongDonHang(dhnew);


            return View(dhnew);

        }

        private List<DonHang> Xet_DonHang(string id, List<DonHang> dhchon)
        {
            List<DonHang> dhmoi = new List<DonHang>();
            foreach (var dh in dhchon)
            {
                if (dh.MaDL == id)
                {
                    dhmoi.Add(dh);
                    dh.TinhTrangThanhToan = "Đang nợ";
                }
            }
            return dhmoi;
        }

      
        public ActionResult TaoCongno(string MaDL, PhieuCongNo phieuCongNo,DateTime Hantra,DateTime NgayLapCN)
        {
            //NGÀY
           
            //var time = DateTime.Today;

            string t= (string)Session["thanglapcn"];

            var c = database.DaiLies.Where(s => s.MaDL == MaDL).ToList().FirstOrDefault();

            //t là tháng
            List<DonHang> dhnew = new List<DonHang>();
            if (t!=null)
            {
                List<DonHang> dhchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && 
                s.TinhTrangThanhToan == "Chưa thanh toán" && s.TinhTrangGH=="Đã giao").ToList();
                List<DonHang> dhchon = LayDonHang_TheoThang(t, dhchuathanhtoan);
                dhnew = Xet_DonHang(MaDL, dhchon);
            } else
            {
                List<DonHang> dhnull = LayDonHang_ChuaThanhToan(MaDL);
                dhnew = Xet_DonHang(MaDL, dhnull);

            }    

          
            
            double tien = (double)TinhTongDonHang(dhnew); 
            Random rd = new Random();
            var macn = "CN" + rd.Next(1, 1000);
            phieuCongNo.MaCongNo = macn;

            phieuCongNo.TrangThai = "Chưa thanh toán";
            phieuCongNo.MaDL = MaDL;
            Session["macndl"] = MaDL;
            phieuCongNo.TienNo = tien;
            phieuCongNo.NgayLapCN = NgayLapCN;

            
            phieuCongNo.HanTra = Hantra;

            
            database.PhieuCongNoes.Add(phieuCongNo);
            database.SaveChanges();
            TempData["taocn"] = "success";
            return RedirectToAction("DSDaiLy");

        }
        //Cong no chua thanh toan
        private void TinhtangCongno()
        {

            List<SelectListItem> tinhtrang = new List<SelectListItem>();
            tinhtrang.Add(new SelectListItem { Text = "Chưa thanh toán", Value = "Chưa thanh toán" });
            tinhtrang.Add(new SelectListItem { Text = "Đã thanh toán", Value = "Đã thanh toán" });
            TempData["tinhtrangcn"] = tinhtrang;
        }
        public ActionResult DSCongnoNo(String id)
        {
            Session["madailycn"] = id;
            BindDropDownList();
            TinhtangCongno();
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id && s.TrangThai=="Chưa thanh toán").ToList();
            return View(dscongno);

        }
        public List<PhieuCongNo> LocCongno_TheoDK(string thang_chon, List<PhieuCongNo> tatcacn, string ttrang)
        {

            List<PhieuCongNo> congno = new List<PhieuCongNo>();
            if (thang_chon != "0")
            {
                foreach (var locdh in tatcacn)
                {
                    DateTime day = (DateTime)locdh.NgayLapCN;
                    var m = Convert.ToString(day.Month);

                    if (thang_chon == m && locdh.TrangThai == ttrang)
                    {
                        congno.Add(locdh);
                    }
                }
            }
            else
            {
                foreach (var locdh in tatcacn)
                {
                    if (locdh.TrangThai == ttrang)
                    {
                        congno.Add(locdh);
                    }
                }

            }
            return congno;

        }
        [HttpPost]
        public ActionResult DSCongnoNo(String id,string thangs,string tinhtrangcn)
        {
            BindDropDownList();
            TinhtangCongno();
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id).ToList();
            List<PhieuCongNo> cnchon =LocCongno_TheoDK(thangs, dscongno, tinhtrangcn).ToList();
            
            Session["madailycn"] = id;
            return View(cnchon);

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

            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
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