using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }
        public List<DonHang> LayDonHang_TheoThang(string thang_chon)
        {
            List<DonHang> tatcadonhang= database.DonHangs.Where(s=>s.TrangThai== "Đã xét duyệt").ToList();
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

            List<DonHang> dhchon = LayDonHang_TheoThang(thangs);
            return View(dhchon);



        }

        public ActionResult QLDonNo()
        {
            //ViewBag.MaDL = new SelectList(database.DonHangs, "MaDL", "MaDL");
            BindDropDownList();
            var dh = database.DonHangs.ToList().OrderByDescending(s => s.MaDH);
            return View(dh);
        }
        private void BindDropDownList()
        {
            List<SelectListItem> m = new List<SelectListItem>();
            m.Add(new SelectListItem { Text = "Select", Value = "0" });
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
        [HttpPost]
        public ActionResult QLDonNo(string thangs, string searchString)
        {
            BindDropDownList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs);
            return View(dhchon);
        }
        
        public ActionResult QLDonThanhToan()
        {
            BindDropDownList();
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        [HttpPost]
        public ActionResult QLDonThanhToan(string thangs)
        {
            BindDropDownList();
            List<DonHang> dhchon = LayDonHang_TheoThang(thangs);
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
            return View(database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));
        }




        private double TinhTongDonHang(string madl)
        {
            double tinhtongdh = 0;
            List<DonHang> donhang = database.DonHangs.Where(s => s.MaDL == madl && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TrangThai == "Đã xét duyệt").ToList();
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

        public ActionResult CongnoDL(string id)
        {
            var c = database.DaiLies.Where(s => s.MaDL == id).ToList().FirstOrDefault();
            ViewBag.TongCN = TinhTongDonHang(id);

            return View(c);

        }

        public ActionResult DSCongnoDL(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaDL == id).ToList());

        }
        public List<DonHang> LayDonHang_ChuaThanhToan(string madl)
        {
            List<DonHang> donhang = database.DonHangs.Where(s =>s.MaDL==madl && s.TinhTrangThanhToan == "Chưa thanh toán" && s.TrangThai == "Đã xét duyệt").ToList();
            return donhang;
        }
        public ActionResult TaoCongno(String id, PhieuCongNo phieuCongNo, DonHang dh)
        {
            //NGÀY

            var time = DateTime.Today;


            // lấy hóa đơn
            List<DonHang> dhcn = LayDonHang_ChuaThanhToan(id);
            //var c = database.DonHangs.Where(s => s.MaDL == id).ToList().FirstOrDefault();   

            double tien = 0;
            tien = (double)TinhTongDonHang(id);
            foreach(var dhxet in dhcn)
            {
                dhxet.TinhTrangThanhToan = "Đang nợ";
            }    
            


            Random rd = new Random();
            var macn = "CN" + rd.Next(1, 1000);

            phieuCongNo.MaCongNo = macn;
            phieuCongNo.TrangThai = "Chưa thanh toán";
            phieuCongNo.MaDL = id;
            phieuCongNo.TienNo = tien;
            phieuCongNo.NgayLapCN = time;
           
            phieuCongNo.HanTra = time.AddDays(15);

            TempData["taocn"] = "success";
            database.PhieuCongNoes.Add(phieuCongNo);
            database.SaveChanges();
            return RedirectToAction("DSDaiLy");

        }

        public ActionResult ChinhsuCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuCN(String id, PhieuCongNo phieuCongNo)
        {
            TempData["chinhsuacn"] = "success";
            database.Entry(phieuCongNo).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();          
            return RedirectToAction("DSDaiLy");
        }

        public ActionResult XoaCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaCN(String id, PhieuCongNo phieuCongNo)
        {
            try
            {
                TempData["xoacn"] = "success";
                phieuCongNo = database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault();
                database.PhieuCongNoes.Remove(phieuCongNo);
                database.SaveChanges();
                return RedirectToAction("DSCongnoDL");

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
            List<DonHang> dh = database.DonHangs.ToList();
            List<DonHang> dt = new List<DonHang>();
            for (int i = 0; i < dh.Count; i++)
            {
                if (dh[i].TinhTrangThanhToan == "Đã thanh toán")
                {
                    dt.Add(dh[i]);
                }
            }
            return View(dt);
            //return View(database.DoanhSoes.ToList().OrderByDescending(s => s.ThoiGian));
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