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
        //chu y
        public ActionResult QLyDonHangKeToan(String id,DonHang dh)
        {
            
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

       //public ActionResult TaoHD(String id)
       // {
       //     return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
       // }
       // [HttpPost]
       // public ActionResult TaoHD(String id,DonHang dh,HoaDon hoaDon)
       // {
       //     dh = database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault();
       //     List<HoaDon> lisths = database.HoaDons.ToList();
       //     for (int i = 0; i < lisths.Count; i++)
       //     {
       //         if (lisths[i].MaDH == dh.MaDH)
       //         {
       //             TempData["mess"] = "Mã đơn hàng bị trùng ";
       //             return RedirectToAction("QlyDonHangKeToan");
       //         }
                
       //     }
       //     var user = (Test02.Models.NhanVien)Session["user"];
       //     Random rd = new Random();
       //     var maHD = "HD" + rd.Next(1, 1000);
       //     hoaDon.MaHD = maHD;
       //     hoaDon.MaDH = dh.MaDH;

       //     hoaDon.TongTien = dh.TongTien;
       //     hoaDon.TenDVTiepNhan = user.MaNV;
       //     hoaDon.NgayLap = System.DateTime.Now;

       //     TempData["ktra"] = "thanhcong ";
       //     database.HoaDons.Add(hoaDon);
       //     database.SaveChanges();
       //     return RedirectToAction("QLHoaDon");
            
       // }

       // public ActionResult XoaHD(String id)
       // {
       //     return View(database.HoaDons.Where(s => s.MaHD== id).FirstOrDefault());
       // }
       // [HttpPost]
       // public ActionResult XoaHD(String id,HoaDon hoaDon)
       // {
       //     TempData["xoahd"] = "thanhcong ";
       //     hoaDon = database.HoaDons.Where(s => s.MaHD == id).FirstOrDefault();
       //     database.HoaDons.Remove(hoaDon);
       //     database.SaveChanges();
       //     return RedirectToAction("QLHoaDon");
       // }

        public ActionResult QLHoaDon()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult ChinhsuaHD(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuaHD(DonHang donHang,String id)
        {
           
            database.Entry(donHang).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QLHoaDon");
        }


        public ActionResult XuatHDBH(String id)
        {
            return View(database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));
        }





        public ActionResult DSDaiLy()
        {
            return View(database.DaiLies.ToList().OrderByDescending(s => s.MaDL));
        }

        public ActionResult CongnoDL(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDL == id).ToList());
            //List<DonHang> dh = new List<DonHang>();

            //for(int i=0;i<listdh.Count;i++)
            //{
            //    if(listdh[i].TinhTrangThanhToan=="Chưa thanh toán")
            //    {
            //        dh.Add(listdh[i]);
            //    }    
            //}    



        }



        public ActionResult QLCongno()
        {
            return View(database.PhieuCongNoes.ToList().OrderByDescending(s => s.MaCongNo));
        }
        //nhập tay
        //public ActionResult ThemCongno()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ThemCongno(PhieuCongNo phieu)
        //{
        //    Random rd = new Random();
        //    var macn = "CN" + rd.Next(1, 1000);
        //    phieu.MaCongNo = macn;

        //    TempData["taocn"] = "success";
        //    database.PhieuCongNoes.Add(phieu);
        //    database.SaveChanges();
        //    return RedirectToAction("QLCongno");
        //}


        //tao tu dong tu danh sách đại lý
        public ActionResult TaoCongno(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDL == id).ToList().FirstOrDefault());
        }

        [HttpPost]
        public ActionResult TaoCongno(String id, PhieuCongNo phieuCongNo, DonHang dh)
        {
            //NGÀY
            var time = System.DateTime.Now;

            // lấy hóa đơn
            List<DonHang> dl = database.DonHangs.ToList();

               
            var c = database.DonHangs.Where(s => s.MaDL == id).ToList().FirstOrDefault();   
            var tien = 0;
            for (int i = 0; i < dl.Count; i++)
            {
                if (dl[i].MaDL == c.MaDL && dl[i].TinhTrangThanhToan == "Chưa thanh toán")
                {
                    tien = (int)(tien + dl[i].TongTien);
                    dl[i].TinhTrangThanhToan = "Đang nợ";
                }
                   
            }


            Random rd = new Random();
            var macn = "CN" + rd.Next(1, 1000);
            phieuCongNo.MaCongNo = macn;
            phieuCongNo.TrangThai = "Chưa thanh toán";
            phieuCongNo.MaDL = c.MaDL;
            phieuCongNo.TienNo = tien;
            phieuCongNo.NgayLapCN = time;
           
            phieuCongNo.HanTra = time.AddDays(15);

            TempData["taocn"] = "success";
            database.PhieuCongNoes.Add(phieuCongNo);
            database.SaveChanges();
            return RedirectToAction("QLCongno");

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
            return RedirectToAction("QLCongno");
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
                return RedirectToAction("QLCongno");

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