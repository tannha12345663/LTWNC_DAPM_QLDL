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

        // Chi tiết sản phẩm
        public ActionResult ChiTietSP(string id)
        {
            TempData["masp"] = id;
            return View(db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault());
        }

        // Giỏ hàng
        public ActionResult GioHangDL()
        {
            if (Session["Cart"] == null)
                return View();
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        // Tạo mới giỏ hàng
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Thêm vào giỏ hàng
        public ActionResult ThemVaoGH(string id)
        {
            TempData["Themvaogiohang"] = "thanhcong";
            var sp = db.SanPhams.SingleOrDefault(s => s.MaSP == id);
            if (sp != null)
            {
                GetCart().Add(sp);
            }
            return RedirectToAction("PageSanPham", "KhachHang");
        }

        // Update số lượng sản phẩm
        public ActionResult CapNhatSL(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id = form["MaSP"];
            int sl = int.Parse(form["soluong"]);
            cart.CapNhatSL(id, sl);
            return RedirectToAction("GioHangDL", "KhachHang");
        }

        public ActionResult XoaSP(string id)
        {
            TempData["xoasp"] = "thanhcong ";
            Cart cart = Session["Cart"] as Cart;
            cart.XoaSP(id);
            return RedirectToAction("GioHangDL", "KhachHang");
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