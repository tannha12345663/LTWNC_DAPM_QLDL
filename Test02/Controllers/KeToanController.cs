using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    public class KeToanController : Controller
    {
        //[Authentication]

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

        public ActionResult QLHoaDon()
        {
            return View();
        }

        public ActionResult QLCongno()
        {
            return View(database.PhieuCongNoes.ToList());
        }
        public ActionResult CreateCongno()
        {
            ViewBag.MaCongno = new SelectList(database.LoaiDLs, "MaLoaiDL", "TenDaiLy");
            return View();
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
    }
}