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

        public ActionResult DanhSachShipper()
        {
            return View();
        }

    }
}