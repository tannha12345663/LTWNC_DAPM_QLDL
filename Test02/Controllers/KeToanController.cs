using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;


namespace Test02.Controllers
{
    [Authentication]
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
            return View();
        }
        //chu y
        public ActionResult QLyDonHangKeToan(String id,DonHang dh)
        {
            
            List<DonHang> listdh = new List<DonHang>();
            List<DonHang> listdh1 = new List<DonHang>();
            listdh = database.DonHangs.ToList();
            for(int i=0;i<listdh.Count;i++)
            {
                if (listdh[i].TrangThai == "Đã xét duyệt")
                {
                    listdh1.Add(listdh[i]);
                }
            }    
            //dh = database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault();
               
            return View(listdh1);
        }

       public ActionResult TaoHD(String id)
        {
            return View(database.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult TaoHD(String id,ChiTietDonHang ct,HoaDon hoaDon)
        {
            ct = database.ChiTietDonHangs.Where(s => s.MaDH == id).FirstOrDefault();

            Random rd = new Random();
            var maHD = "HD" + rd.Next(1, 1000);
            hoaDon.MaHD = maHD;
            hoaDon.MaDH = ct.MaDH;
            hoaDon.TongTien = ct.ThanhTien;



            database.HoaDons.Add(hoaDon);
                database.SaveChanges();
                return RedirectToAction("QLCongno");
            
        }

        public ActionResult XoaHD(String id)
        {
            return View(database.HoaDons.Where(s => s.MaHD== id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaHD(String id,HoaDon hoaDon)
        {
            hoaDon = database.HoaDons.Where(s => s.MaHD == id).FirstOrDefault();
            database.HoaDons.Remove(hoaDon);
            database.SaveChanges();
            return RedirectToAction("QLHoaDon");
        }

        public ActionResult QLHoaDon()
        {
            return View(database.HoaDons.ToList().OrderByDescending(s => s.MaHD));
        }

        public ActionResult ChinhsuHD()
        {
            return View();
        }


        public ActionResult XuatHDBH(String id)
        {
            
            return View(database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));


        }






        //cong no
        public ActionResult QLCongno()
        {
            return View(database.PhieuCongNoes.ToList().OrderByDescending(s => s.MaCongNo));
        }

        public ActionResult TaoCongno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaoCongno(PhieuCongNo phieuCongNo)
        {
            
            try
            {
                Random rd = new Random();
                var macn = "CN" + rd.Next(1, 1000);
                phieuCongNo.MaCongNo = macn;
                //   daiLy.NgayTao = System.DateTime.Now;
                phieuCongNo.TrangThai = "Chưa thanh toán";
                database.PhieuCongNoes.Add(phieuCongNo);
                database.SaveChanges();
                return RedirectToAction("QLCongno");
            }
            catch
            {
                return Content("Error");
            }

        }

        public ActionResult ChinhsuCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s=>s.MaCongNo==id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhsuCN(String id, PhieuCongNo phieuCongNo)
        {
            database.Entry(phieuCongNo).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QLCongno");
        }

        public ActionResult XoaCN(String id)
        {
            return View(database.PhieuCongNoes.Where(s => s.MaCongNo == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaCN(String id,PhieuCongNo phieuCongNo)
        {
            try
            {
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

        public ActionResult Doanhthu()
        {
            return View();
        }


        public ActionResult CreateDoanhThu()
        {
            return View();
        }

       public ActionResult LoiNhuan()
        {
            return View();
        }

        public ActionResult DSDaiLy()
        {
            return View(database.DaiLies.ToList().OrderByDescending(s => s.MaDL));
        }

      


       
    }
}