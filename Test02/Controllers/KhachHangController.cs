using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [AuthenticationDL]
    public class KhachHangController : Controller
    {
        QuanLyDLEntities2 db = new QuanLyDLEntities2();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageSanPham()
        {
           
            return View(db.SanPhams.ToList().OrderByDescending(s => s.TenSP));
        }

        // GET: ChiTietDonHangs
        public ActionResult DonHangDL(string id)
        {
            
            return View(db.DonHangs.ToList());
        }

        // GET: ChiTietDonHangs/Details/5
        public ActionResult DetailsDH(string id)
        {
            TempData["madh"] = id;
            return View(db.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));

        }

        // GET: ChiTietDonHangs/Delete/5
        public ActionResult DeleteDH(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("DeleteDH")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDHConfirmed(string id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
            db.SaveChanges();
            return RedirectToAction("DonHangDL");
        }
    }
}