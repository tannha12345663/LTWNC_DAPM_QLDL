using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult TrangChuKeToan()
        {
            return View(database.DonHangs.ToList());
        }
        
        public ActionResult Thongbao()
        {
            return View(database.PhieuCongNoes.ToList());
        }

        public ActionResult QLDonHang()
        {
            BindDropDownList();
            List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Chưa thanh toán").ToList();
            return View(donhangchuathanhtoan);
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
        public ActionResult QLDonHang(string thangs, string searchString)
        {
          
            BindDropDownList();

            List<DonHang> donhangchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan=="Chưa thanh toán").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs,donhangchuathanhtoan);
            return View(dhchon);



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
        public ActionResult QLDonNo()
        {
            //ViewBag.MaDL = new SelectList(database.DonHangs, "MaDL", "MaDL");
            BindDropDownList();
            List<DonHang> donhangno = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đang nợ").ToList();
            return View(donhangno);
        }
        [HttpPost]
        public ActionResult QLDonNo(string thangs, string searchString)
        {
            BindDropDownList();
            List<DonHang> donhangno = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đang nợ").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs,donhangno);
            return View(dhchon);
        }
        
        public ActionResult QLDonThanhToan()
        {
            BindDropDownList();
            List<DonHang> dhthanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đã thanh toán").ToList();
            return View(dhthanhtoan);
        }

        [HttpPost]
        public ActionResult QLDonThanhToan(string thangs)
        {
            BindDropDownList();
            List<DonHang> dhthanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đã thanh toán").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs,dhthanhtoan);
            return View(dhchon);}



        public ActionResult ChinhsuaHD(string id)
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
        public ActionResult ChinhsuaHD(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["edithd"] = "oke";
                if (donHang.TinhTrangThanhToan=="Chưa thanh toán")
                    return RedirectToAction("QLDonHang");
                else if(donHang.TinhTrangThanhToan=="Đã thanh toán")
                    return RedirectToAction("QLDonThanhToan");
                else
                    return RedirectToAction("QLDonNo");

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
            if (donHang == null)
            {
                return HttpNotFound();
            }

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
            
            return View(database.DaiLies.ToList().OrderByDescending(s => s.MaDL));
        }
        public List<DonHang> LayDonHang_ChuaThanhToan(string madl)
        {
            List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == madl && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TrangThai == "Đã xét duyệt").ToList();

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
            //DateTime today = DateTime.Now;
            //int thang = ChuyenThang(today);
            //List<DonHang> donHangs = new List<DonHang>();
            //foreach(var don in dh)
            //{
            //    DateTime item =(DateTime) don.NgayLap;
            //    int itemmonth = ChuyenThang(item);
            //    if(thang==itemmonth)
            //    {
            //        donHangs.Add(don);
            //    }    
            //}    


            ViewBag.TongCN = TinhTongDonHang(dh);
            Session["Madly"] = id;

            return View(dh);

        }
        [HttpPost]
        public ActionResult CongnoDL(string id, string thangs)
        {
            BindDropDownList();
            var c = database.DaiLies.Where(s => s.MaDL == id).ToList().FirstOrDefault();
            List<DonHang> dhchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Chưa thanh toán").ToList();
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

            List<DonHang> dhnew = new List<DonHang>();
            if (t!=null)
            {
                List<DonHang> dhchuathanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Chưa thanh toán").ToList();
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

            TempData["taocn"] = "success";
            database.PhieuCongNoes.Add(phieuCongNo);
            database.SaveChanges();
            return RedirectToAction("DSDaiLy");

        }
        //Cong no chua thanh toan
        public ActionResult DSCongnoNo(String id)
        {
            Session["madailycn"] = id;
            var dscongno = database.PhieuCongNoes.Where(s => s.MaDL == id && s.TrangThai=="Chưa thanh toán").ToList();
            return View(dscongno);

        }
        public ActionResult ChinhsuCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuCN(String id, PhieuCongNo phieuCongNo)
        {
            
            database.Entry(phieuCongNo).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            TempData["chinhsuacn"] = "success";
            return RedirectToAction("DSDaiLy");
        }

     
        public ActionResult XoaCN(string id, PhieuCongNo phieuCongNo)
        {
            try
            {
                
                phieuCongNo = database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault();
                Session["matim"] = phieuCongNo.MaDL;
                database.PhieuCongNoes.Remove(phieuCongNo);
                database.SaveChanges();
                TempData["xoacn"] = "success";
                return RedirectToAction("DSCongnoNo", new RouteValueDictionary(
                                       new { controller = "KeToan", action = "DSCongnoNo", Id = Session["matim"] }));

            }
            catch
            {
                return Content("err");
            }

        }

        public ActionResult XuatCN(String id)
        {
            
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }    

        public ActionResult ChitietCN(String id)
        {

            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }

        public ActionResult Doanhthu()
        {
            BindDropDownList();
            var donhangtt = database.DonHangs.Where(s => s.TinhTrangThanhToan == "Đã thanh toán").ToList();
            return View(donhangtt);
        }
        [HttpPost]
        public ActionResult Doanhthu(string thangs)
        {
            BindDropDownList();
            List<DonHang> dhthanhtoan = database.DonHangs.Where(s => s.TrangThai == "Đã xét duyệt" && s.TinhTrangThanhToan == "Đã thanh toán").ToList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs,dhthanhtoan);

            return View(dhchon);
        }

        //public ActionResult TaoDoanhThu(DoanhSo doanhSo)
        //{
        //    Random rd = new Random();
        //    var madt = "DT" + rd.Next(1, 1000);
        //    List<DonHang> donhang = new List<DonHang>();
        //    for (int i=0;i<donhang.Count;i++)
        //    { 
        //        DateTime time = (DateTime)donhang[i].NgayLap;
        //        int thang = time.Month;

        //        if (doanhSo.ThoiGian == thang)
        //        {

        //        }    
        //    }    




        //    database.DoanhSoes.Add(doanhSo);
        //    database.SaveChanges();
        //    return RedirectToAction("Doanhthu");

        //}




        //public ActionResult LoiNhuan()
        //{
        //    return View();
        //}







    }
}