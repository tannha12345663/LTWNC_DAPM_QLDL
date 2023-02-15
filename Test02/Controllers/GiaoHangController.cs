using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
namespace Test02.Controllers
{
    [Authentication (MaChucVu ="NVGH")]
    public class GiaoHangController : Controller
    {
        // GET: GiaoHang
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult DanhSachHD()
        {
            return View();
        }
        public ActionResult TiepNhanDH()
        {
            return View();
        }
        public ActionResult BaoCao()
        {
            return View();
        }
    }
}