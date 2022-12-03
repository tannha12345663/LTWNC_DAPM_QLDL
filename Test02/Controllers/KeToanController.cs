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
            return View(database.HoaDons.ToList().OrderByDescending(s => s.MaHD));
        }
        //chu y
        public ActionResult QLyDonHangKeToan(String id,DonHang dh)
        {
            
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

       public ActionResult TaoHD(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult TaoHD(String id,DonHang dh,HoaDon hoaDon)
        {
            TempData["successmassage"] = "ha";
            var user = (Test02.Models.NhanVien)Session["user"];
            dh = database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault();
            Random rd = new Random();
            var maHD = "HD" + rd.Next(1, 1000);
            hoaDon.MaHD = maHD;
            hoaDon.MaDH = dh.MaDH;
            hoaDon.TongTien = dh.TongTien;
            hoaDon.TenDVTiepNhan = user.MaNV;
            hoaDon.NgayLap= System.DateTime.Now;



            database.HoaDons.Add(hoaDon);
             database.SaveChanges();
             return RedirectToAction("QLHoaDon");
            
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

        public ActionResult recyclebin()
        {
            return View();
        }


        public ActionResult QLHoaDon()
        {
            return View(database.HoaDons.ToList().OrderByDescending(s => s.MaHD));
        }

        public ActionResult ChinhsuaHD(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDH == id).FirstOrDefault());
        }

        [HttpPost]

        public ActionResult ChinhsuaHD(DonHang ct)
        {
            database.Entry(ct).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QLHoaDon");
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

        public ActionResult TaoCongno(String id)
        {
            return View(database.DaiLies.Where(s => s.MaDL == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult TaoCongno(DaiLy dl,String id,PhieuCongNo phieuCongNo,DonHang dh)
        {
            
            try
            {
                dl = database.DaiLies.Where(s => s.MaDL == id).FirstOrDefault();
                List<DonHang> listdh = database.DonHangs.ToList();
                double count = 0;
               for(int i=0;i<listdh.Count;i++)
                {
                    if(dl.MaDL==listdh[i].MaDL)
                    {
                        count = (double)(count + listdh[i].TongTien);
                    }    
                }    
                Random rd = new Random();
                var macn = "CN" + rd.Next(1, 1000);
                phieuCongNo.MaCongNo = macn;
                phieuCongNo.TrangThai = "Chưa thanh toán";
                phieuCongNo.MaDL = dl.MaDL;
                phieuCongNo.TienNo = count;
                phieuCongNo.HanTra= System.DateTime.Now;

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
            return View(database.DonHangs.ToList());
        }


       

       public ActionResult LoiNhuan()
        {
            return View();
        }

        public ActionResult DSDaiLy()
        {
            return View(database.DaiLies.ToList().OrderByDescending(s => s.MaDL));
        }

        public ActionResult CongnoDL(String id)
        {
            return View(database.DonHangs.Where(s => s.MaDL == id).ToList());
        }
       




    }
}