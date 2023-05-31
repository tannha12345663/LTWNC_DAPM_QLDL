using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [Authentication (MaChucVu ="QTHT")]
    public class QTVController : Controller
    {
        QuanLyDLproEntities2 database = new QuanLyDLproEntities2();
        // GET: QTV
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QLNhanVien()
        {
            return View(database.NhanViens.ToList().OrderByDescending(s => s.MaNV));
        }
        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData =database.NhanViens.Where(x => x.UserName == userdata).SingleOrDefault();
            
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult CheckEmailAvailability(string usermail)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData = database.NhanViens.Where(x => x.Email == usermail).SingleOrDefault();

            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult CheckSDTAvailability(string userSDT)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData = database.NhanViens.Where(x => x.SDT == userSDT).SingleOrDefault();

            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult KtraPass(string pass1,string pass2)
        {
            System.Threading.Thread.Sleep(200);
            if (pass1!=pass2)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        
        public ActionResult ThemNV()
        {

            ViewBag.MaChucVu = new SelectList(database.ChucVus, "MaChucVu", "MaChucVu");
           
            return View();
        }

        [HttpPost]
        public ActionResult ThemNV(NhanVien nhanVien,HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    //Lấy tên file của hình được up lên

                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    //Tạo đường dẫn tới file

                    var path = Path.Combine(Server.MapPath("~/Data/Images"), fileName);
                    //Lưu tên

                    nhanVien.HinhAnh = fileName;
                    //Save vào Images Folder
                    HinhAnh.SaveAs(path);

                }
                else
                {
                    nhanVien.HinhAnh = "account.png";
                }    
                Random rd = new Random();
                var manv = "NV" + rd.Next(1, 1000);
                nhanVien.MaNV = manv;

                database.NhanViens.Add(nhanVien);
                database.SaveChanges();
                TempData["thongbao"] = "add";
                return RedirectToAction("QLNhanVien");
            }
            
            return View(nhanVien);
            
           
        }

        public ActionResult ChinhsuaNV(String id,NhanVien nhanVien)
        {
            ViewBag.MaChucVu = new SelectList(database.ChucVus, "MaChucVu", "MaChucVu", nhanVien.MaChucVu);
            return View(database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault());
        }
        
            [HttpPost]
            public ActionResult ChinhsuaNV(NhanVien nv, HttpPostedFileBase HinhAnh, string imgnv)
            {
                if (ModelState.IsValid)
                {
                if (HinhAnh != null)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data/Images"), fileName);

                    nv.HinhAnh = fileName;
                    //Save vào Images Folder
                    HinhAnh.SaveAs(path);

                }
                else
                {
                    nv.HinhAnh = imgnv;
                }
                database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["thongbao"] = "edit";
                    return RedirectToAction("QLNhanVien");
                }
                return View(nv);

            }

        
        public ActionResult ChitietNV(string id)
        {
            var nhanvien = database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault();
            return View(nhanvien);
        }
        
       
        public ActionResult XoaNV(string id,NhanVien nhanvien)
        {
            nhanvien = database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault();
            database.NhanViens.Remove(nhanvien);
            database.SaveChanges();
            TempData["thongbao"] = "delete";
            return RedirectToAction("QLNhanVien");
        }

    }
}