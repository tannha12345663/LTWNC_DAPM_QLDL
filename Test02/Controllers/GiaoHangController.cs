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

namespace Test02.Controllers
{

    [Authentication(MaChucVu = "NVGH")]
    public class GiaoHangController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: GiaoHang
        public ActionResult DonHangMoi()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult DanhSachGiaoHang()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult DanhSachDonHoan()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult BaoCaoLyDoHoanDon()
        {
            return View(database.DonHangs.ToList().OrderByDescending(s => s.MaDH));
        }

        public ActionResult BaoCaoPhongGiaoHang()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BaoCaoPhongGiaoHang(BaoCao baocao)
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

        public ActionResult DanhSachShipper()
        {
            return View();
        }

    }
}