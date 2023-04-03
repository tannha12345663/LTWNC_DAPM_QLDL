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
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
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
        public void LuuAnh(NhanVien nv, HttpPostedFileBase HinhAnh)
        {
            #region Hình ảnh
            //Xác định đường dẫn lưu file : Url tương đói => tuyệt đói
            var urlTuongdoi = "/Data/Images/";
            var urlTuyetDoi = Server.MapPath(urlTuongdoi);// Lấy đường dẫn lưu file trên server

            //Check trùng tên file => Đổi tên file  = tên file cũ (ko kèm đuôi)
            //Ảnh.jpg = > ảnh + "-" + 1 + ".jpg" => ảnh-1.jpg

            string fullDuongDan = urlTuyetDoi + HinhAnh.FileName;
            int i = 1;
            while (System.IO.File.Exists(fullDuongDan) == true)
            {
                // 1. Tách tên và đuôi 
                var ten = Path.GetFileNameWithoutExtension(HinhAnh.FileName);
                var duoi = Path.GetExtension(HinhAnh.FileName);
                // 2. Sử dụng biến i để chạy và cộng vào tên file mới
                fullDuongDan = urlTuyetDoi + ten + "-" + i + duoi;
                i++;
                // 3. Check lại 
            }
            #endregion
            //Lưu file (Kiểm tra trùng file)
            HinhAnh.SaveAs(fullDuongDan);
            nv.HinhAnh = urlTuongdoi + Path.GetFileName(fullDuongDan);
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
                LuuAnh(nhanVien, HinhAnh);
                Random rd = new Random();
                var manv = "NV" + rd.Next(1, 1000);
                nhanVien.MaNV = manv;

                database.NhanViens.Add(nhanVien);
                database.SaveChanges();
                return RedirectToAction("QLNhanVien");
            }
            
            return View(nhanVien);
            
           
        }

        public ActionResult ChinhsuaNV(String id,NhanVien nhanVien)
        {
            ViewBag.MaChucVu = new SelectList(database.ChucVus, "MaChucVu", "MaChucVu");
            return View(database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhsuaNV(String id,NhanVien nhanVien,HttpPostedFileBase HinhAnh)
        {
            
            LuuAnh(nhanVien, HinhAnh);
            database.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;
            ViewBag.MaChucVu = new SelectList(database.ChucVus, "MaChucVu", "MaChucVu", nhanVien.MaChucVu);
            database.SaveChanges();
            return RedirectToAction("QLNhanVien");
        }
        public ActionResult ChitietNV(string id)
        {
            var nhanvien = database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault();
            return View(nhanvien);
        }
        
       
        public ActionResult XoaNV(String id,NhanVien nhanvien)
        {
            nhanvien = database.NhanViens.Where(s => s.MaNV == id).FirstOrDefault();
            database.NhanViens.Remove(nhanvien);
            database.SaveChanges();
            return RedirectToAction("QLNhanVien");
        }



    }
}