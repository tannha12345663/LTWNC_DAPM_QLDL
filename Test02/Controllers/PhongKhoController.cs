using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;

namespace Test02.Controllers
{
    public class PhongKhoController : Controller
    {
        [Authentication]
        // GET: PhongKho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test02()
        {
            return View();
        }
        public ActionResult QuanLyDL()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        public ActionResult ThemSanPham()
        {
            return View();
        }
        public ActionResult NhapKho()
        {
            return View();
        }
        public ActionResult TaoPhieuNhapKho()
        {
            return View();
        }
        public ActionResult XuatKho()
        {
            return View();
        }
        public ActionResult TaoPhieuXuatKho()
        {
            return View();
        }
        public ActionResult TonKho()
        {
            return View();
        }
        public ActionResult GiaiQuyetTonKho()
        {
            return View();
        }
        public ActionResult BaoCao()
        {
            return View();
        }
    }
}