using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test02.Controllers
{
    public class Test01Controller : Controller
    {
        // GET: Test01
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
    }
}