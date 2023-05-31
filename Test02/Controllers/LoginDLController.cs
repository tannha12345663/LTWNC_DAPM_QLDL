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
        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
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

        public JsonResult CheckEmailAvailability(string email)
        {
            System.Threading.Thread.Sleep(200);

            var mailCus = database.DaiLies.Where(x => x.Email == email).SingleOrDefault();
            if (mailCus != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult CheckNoPhoneAvailability(string noPhone)
        {
            System.Threading.Thread.Sleep(200);

            var noPhoneCus = database.DaiLies.Where(x => x.SDT == noPhone).SingleOrDefault();
            if (noPhoneCus != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult CheckUserNameAvailability(string userName)
        {
            System.Threading.Thread.Sleep(200);

            var userNameCus = database.DaiLies.Where(x => x.UserName == userName).SingleOrDefault();
            if (userNameCus != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(DaiLy daiLy, string ConfirmPassword)
        {
            Random random = new Random();

            var sdt = database.DaiLies.Count(s => s.SDT == daiLy.SDT);
            var email = database.DaiLies.Count(s => s.Email == daiLy.Email);
            var username = database.DaiLies.Count(s => s.UserName == daiLy.UserName);

            if ((sdt >= 1) || (email >= 1) || (username >= 1))
            {
                TempData["errorInfo"] = "Thông tin đã bị trùng";
                return View(daiLy);
            }

            if (daiLy.Password.Length < 6)
            {
                TempData["errorPass"] = "Mật khẩu tổi thiểu 6 ký tự";
                return View(daiLy);
            }
            if (ModelState.IsValid)
            {
                if (ConfirmPassword != daiLy.Password)
                {
                    TempData["errorMK"] = "Xác thực mật khẩu không chính xác";
                    return View(daiLy);
                }
                else if (ConfirmPassword == daiLy.Password)
                {
                    var maDL = "DL" + random.Next(0, 9) + random.Next(0, 9) + random.Next(0, 9) + random.Next(0, 9);
                    daiLy.MaDL = maDL;
                    daiLy.MaLoaiDL = "LDL03";
                    daiLy.NgayTao = System.DateTime.Now;
                    database.DaiLies.Add(daiLy);
                    database.SaveChanges();
                    TempData["success"] = "Đăng kí thành công";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("DangKy");
        }
    }
}