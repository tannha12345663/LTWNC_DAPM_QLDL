using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test02.Controllers
{
    public class KinhDoanhController : Controller
    {
        QuanLyDLEntities database = new QuanLyDLEntities();
        // GET: KinhDoanh
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult QuanLyDL()
        {
            return View(database.DaiLies.ToList());
        }
        public ActionResult QuanLyDH()
        {
            return View();
        }
        public ActionResult QuanLyHD()
        {
            return View();
        }
        public ActionResult QuanLySP()
        {
            return View(database.SanPhams.ToList());
        }
        public ActionResult QuanLyKho()
        {
            return View();
        }
        public ActionResult BaoCao()
        {
            return View();
        }
        public ActionResult Test01()
        {
            return View(database.DaiLies.ToList());
        }
    }
}