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
    public class LoginUserController : Controller
    {
        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
        // GET: LoginUser
        public ActionResult Login()
        {
                return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var kd = "NVKD"; var kho = "NVK";var kt = "NVKT";var gh = "NVGH"; var qtv = "QTHT";
            var data = database.NhanViens.Where(s => s.UserName == username && s.Password == password).FirstOrDefault();
            var taikhoan = database.NhanViens.SingleOrDefault(s => s.UserName == username && s.Password == password);
            if(data == null)
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng";
                return View("Login");
            }
            else if(taikhoan != null)
            {
                //add session
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["user"] = taikhoan;
                if (data.MaChucVu.ToString() == kd)
                {
                    return RedirectToAction("TrangChu", "KinhDoanh");
                }
                else if (data.MaChucVu.ToString() == kho)
                {
                    return RedirectToAction("Test02", "PhongKho");
                }
                else if (data.MaChucVu.ToString() == kt)
                {
                    return RedirectToAction("TrangChuKeToan", "KeToan");
                }
                else if (data.MaChucVu.ToString() == qtv)
                {
                    return RedirectToAction("QLNhanVien", "QTV");
                }
                else if (data.MaChucVu.ToString() == gh)
                {
                    return RedirectToAction("DonHangMoi", "GiaoHang");
                }
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