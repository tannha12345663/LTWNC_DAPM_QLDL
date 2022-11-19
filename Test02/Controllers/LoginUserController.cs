using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.Models;
namespace Test02.Controllers
{
    public class LoginUserController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: LoginUser
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var kd = "NVKD"; var kho = "NVK";var kt = "NVKT";var gh = "NVGH";
            var data = database.NhanViens.Where(s => s.UserName == username && s.Password == password).FirstOrDefault();
            if(data == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("Login");
            }
            else
            {
                //add session
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["TenNV"] = data.TenNV.ToString();
                Session["MaNV"] = data.MaNV.ToString();
                if (data.IdChucVu.ToString() == kd)
                {
                    return RedirectToAction("TrangChu", "KinhDoanh");
                }
                else if (data.IdChucVu.ToString() == kho)
                {
                    return RedirectToAction("TrangChu", "Kho");
                }
                else if (data.IdChucVu.ToString() == kt)
                {
                    return RedirectToAction("TrangChu", "KeToan");
                }
                else if (data.IdChucVu.ToString() == gh)
                {
                    return RedirectToAction("TrangChu", "GiaoHang");
                }
            }
            ViewBag.ErrorInfo = "Sai thông tin";
            return View("Login");
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

    }
}