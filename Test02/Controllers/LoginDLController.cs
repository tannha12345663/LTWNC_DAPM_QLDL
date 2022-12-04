using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test02.Models;
namespace Test02.Controllers
{
    public class LoginDLController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: LoginDL
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            
            var data = database.DaiLies.Where(s => s.UserName == username && s.Password == password).FirstOrDefault();
            var taikhoan = database.DaiLies.SingleOrDefault(s => s.UserName == username && s.Password == password);
            
            if (data == null)
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng";
                return View("Login");
            }
            else if (taikhoan != null)
            {
                //add session
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["userDL"] = taikhoan;
                return RedirectToAction("PageSanPham", "KhachHang");
                
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult QuenMK()
        {
            return View();
        }
    }
}