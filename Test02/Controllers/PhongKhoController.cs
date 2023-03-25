using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Test02.App_Start;
using Test02.Models;

namespace Test02.Controllers
{
    [Authentication (MaChucVu ="NVK")]
    
    public class PhongKhoController : Controller
    {
        QuanLyDLEntities2 database = new QuanLyDLEntities2();
        // GET: PhongKho
        public ActionResult Index()
        {
            return View();
        }

    // Quản lý kho

        public ActionResult QuanLyKho()
        {
            return View(database.Khoes.ToList().OrderByDescending(s => s.TenKho));
        }
        // GET: Khoes/Details/5
        //----------------------------------------------------------------------------------
        // Chi tiết kho
        public ActionResult ChiTietKho(string id)
        {
            TempData["makho"] = id;
            return View(database.ChiTietKhoes.ToList().Where(s => s.MaKho == id));
        }
        // GET: ChiTietKhoes/Create
        public ActionResult CreateCTKho(string id)
        {
            Session["makho1"] = id;
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho" );
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        // POST: ChiTietKhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCTKho([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {

            if (ModelState.IsValid)
            {
                chiTietKho.MaKho = (string)Session["makho1"];
                //Random rd = new Random();
                //var mactk = "CTK" + rd.Next(1, 1000);
                //chiTietKho.MaCTKho = mactk;
                if (chiTietKho.SoLuong <= 100 && chiTietKho.SoLuong > 0)
                {
                    chiTietKho.TinhTrang = "Sắp hết hàng";
                }
                else if (chiTietKho.SoLuong > 100 && chiTietKho.SoLuong < 1000)
                {
                    chiTietKho.TinhTrang = "Còn hàng";
                }
                //var ktra = (System.DateTime.Now - chiTietKho.NgayXuat);
                else if(chiTietKho.SoLuong >= 1000)
                {
                    chiTietKho.TinhTrang = "Tồn kho";
                }
                else if (chiTietKho.SoLuong == 0)
                {
                    chiTietKho.TinhTrang = "Hết hàng";
                }
                else
                {
                    TempData["AlertMessage"] = "error null";
                    return RedirectToAction("CreateCTKho", new RouteValueDictionary(
                                        new { controller = "PhongKho", action = "CreateCTKho", Id = chiTietKho.MaKho }));
                }
                if(chiTietKho.NgayXuat == null)
                {
                    DateTime ngayDf = new DateTime(2000, 01, 01);
                    chiTietKho.NgayXuat = ngayDf;
                }
                database.ChiTietKhoes.Add(chiTietKho);
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã thêm";
                TempData["MaCTKkk"] = chiTietKho.STT;
                return RedirectToAction("ChiTietKho", new RouteValueDictionary(
                                        new { controller = "PhongKho", action = "ChiTietKho", Id = chiTietKho.MaKho }));
            }
            return View();
        }

        // GET: ChiTietKhoes/Edit/5
        public ActionResult EditCTKho(int id)
        {
            TempData["mactk"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mactk = database.ChiTietKhoes.Where(s => s.STT == id).FirstOrDefault();
            //ChiTietKho chiTietKho = database.ChiTietKhoes.Find(id);
            if (mactk == null)
            {
                return HttpNotFound();
            }
            Session["Mactkho"] = mactk.STT;
            Session["Makho"] = mactk.MaKho;
            
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View(mactk);
        }

        // POST: ChiTietKhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCTKho([Bind(Include = "MaCTKho,MaSP,MaKho,NgayNhap,NgayXuat,SoLuong,TinhTrang")] ChiTietKho chiTietKho)
        {
            if (ModelState.IsValid)
            {
                chiTietKho.STT = (int)Session["Mactkho"];
                chiTietKho.MaKho = (string)Session["Makho"];
                if (chiTietKho.SoLuong <= 100 && chiTietKho.SoLuong > 0)
                {
                    chiTietKho.TinhTrang = "Sắp hết hàng";
                }
                else if (chiTietKho.SoLuong > 100 && chiTietKho.SoLuong < 1000)
                {
                    chiTietKho.TinhTrang = "Còn hàng";
                }
                //var ktra = (System.DateTime.Now - chiTietKho.NgayXuat);
                else if (chiTietKho.SoLuong >= 1000)
                {
                    chiTietKho.TinhTrang = "Tồn kho";
                }
                else if (chiTietKho.SoLuong == 0)
                {
                    chiTietKho.TinhTrang = "Hết hàng";
                }
                else
                {
                    TempData["AlertMessage"] = "check null";
                    return RedirectToAction("EditCTKho");
                }
                database.Entry(chiTietKho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã cập nhật";
                TempData["MaCTKkk"] = Session["Mactkho"];
                return RedirectToAction("Chitietkho", new RouteValueDictionary(
                                        new { controller = "PhongKho", action = "Chitietkho", Id = chiTietKho.MaKho }));
            }
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", chiTietKho.MaKho);
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietKho.MaSP);
            return View(chiTietKho);
        }

        // GET: ChiTietKhoes/Delete/5
        public ActionResult DeleteCTKho(int id)
        {
            var mactk = database.ChiTietKhoes.Where(s => s.STT == id).FirstOrDefault();
            //ChiTietKho chiTietKho = database.ChiTietKhoes.Find(id);
            if (mactk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(mactk);
        }

        // POST: ChiTietKhoes/Delete/5
        [HttpPost, ActionName("DeleteCTKho")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCTKhoConfirmed(int id)
        {
            var mactk = database.ChiTietKhoes.Where(s => s.STT == id).FirstOrDefault();
            database.ChiTietKhoes.Remove(mactk);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["MaCTKkk"] = mactk.STT;
            return RedirectToAction("QuanLyKho");
        }

        //-----------------------------------------------------------------------------------
        // GET: Khoes/Create
        public ActionResult Themkho()
        {
            return View();
        }

        // POST: Khoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKho([Bind(Include = "MaKho,TenKho,DiaChi")] Kho kho)
        {
            //if (ModelState.IsValid)
            //{
            //    Random rd = new Random();
            //    var makho = "KO" + rd.Next(1, 1000);
            //    kho.MaKho = makho;
            //    database.Khoes.Add(kho);
            //    database.SaveChanges();
            //    TempData["AlertMessage"] = "Đã thêm";
            //    TempData["MaCTKkk"] = makho;
            //    return RedirectToAction("QuanLyKho");
            //}
            if(kho.TenKho == null || kho.DiaChi == null)
            {
                TempData["AlertMessage"] = "check null";
                return RedirectToAction("Themkho");
            }
            else
            {
                Random rd = new Random();
                var makho = "KO" + rd.Next(1, 1000);
                kho.MaKho = makho;
                database.Khoes.Add(kho);
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã thêm";
                TempData["MaCTKkk"] = makho;
                return RedirectToAction("QuanLyKho");
            }
            return View(kho);
        }

        // GET: Khoes/Edit/5
        
        public ActionResult ChinhSuaKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = database.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            return View(kho);
        }

        // POST: Khoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaKho([Bind(Include = "MaKho,TenKho,DiaChi")] Kho kho)
        {
            //if (ModelState.IsValid)
            //{
            //    database.Entry(kho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
            //    database.SaveChanges();
            //    TempData["AlertMessage"] = "Đã cập nhật";
            //    TempData["MaCTKkk"] = kho.MaKho;
            //    return RedirectToAction("QuanLyKho");
            //}
            if (kho.TenKho == null || kho.DiaChi == null)
            {
                TempData["AlertMessage"] = "check null";
                return RedirectToAction("ChinhSuaKho");
            }
            else
            {
                database.Entry(kho).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã cập nhật";
                TempData["MaCTKkk"] = kho.MaKho;
                return RedirectToAction("QuanLyKho");
            }
            return View(kho);
        }

        // GET: Khoes/Delete/5
        public ActionResult XoaKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = database.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            return View(kho);
        }

        // POST: Khoes/Delete/5
        [HttpPost, ActionName("XoaKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaKhoConfirmed(string id)
        {
            Kho kho = database.Khoes.Find(id);
            database.Khoes.Remove(kho);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["MaCTKkk"] = id;
            return RedirectToAction("QuanLyKho");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    

        public ActionResult Test02()
        {
            //tính tổng số kho
            var total = database.Khoes.ToList().Count;
            TempData["tongKho"] = total;
            //tính tổng sản phẩm sắp hết hàng
            var dssphh = database.ChiTietKhoes.ToList();
            var total2 = 0;
            foreach (var item in dssphh)
            {
                if (item.TinhTrang == "Sắp hết hàng")
                {
                    total2 = total2 + 1;
                }
            }
            TempData["tongSPHH"] = total2;

            //tính tổng số sản phẩm
            var totalsp = database.SanPhams.ToList().Count;
            TempData["Tongsp1"] = totalsp;
            //tính tổng số Phiếu nhập xuất hàng
            var tongpn = 0;
            var tongpx = 0;
            var pnx = database.PhieuNhapXuats.ToList();
            foreach (var item in pnx)
            {
                string str = item.MaPhieu.Substring(0, 2);
                if (str == "PX")
                {
                    tongpx = tongpx + 1;
                }
                else if (str == "PN")
                {
                    tongpn = tongpn + 1;
                }
                else
                {
                    tongpx = tongpx + 1;
                }
            }
            TempData["Tongpn"] = tongpn;
            TempData["Tongpx"] = tongpx;
            //tính tổng sản phẩm tồn kho
            var total3 = 0;
            foreach (var item in dssphh)
            {
                if (item.TinhTrang == "Tồn kho")
                {
                    total3 = total3 + 1;
                }
            }
            TempData["TongSPTK"] = total3;

            return View(database.Khoes.ToList());
        }
        public ActionResult QuanLyDL()
        {
            return View();
        }


        ////----------------------------------------------------------------------
        //// GET: PhieuNhapXuats/Create
        public ActionResult TaoPhieuNhapKho()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        //// POST: PhieuNhapXuats/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoPhieuNhapKho([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu,MaNVLap")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var maphieu = "PNK" + rd.Next(1, 1000);
                phieuNhapXuat.MaPhieu = maphieu;
                Session["phieunhap"] = maphieu;
                phieuNhapXuat.NgayLap = System.DateTime.Now;
                phieuNhapXuat.LoaiPhieu = "Phiếu đề nghị nhập hàng vào kho";
                Session["KinhGui"] = " ";
                Session["HoTen"] = " ";
                database.PhieuNhapXuats.Add(phieuNhapXuat);
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã thêm";
                TempData["MaPNKho"] = maphieu;
                return RedirectToAction("QuanLyDL");
            }

            return View(phieuNhapXuat);
        }

        //// GET: PhieuNhapXuats/Details/5
        public ActionResult ChiTietPhieuNhapKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["maPhieuNhap"] = id;
            Session["KinhGui"] = "Trưởng phòng kinh doanh";
            Session["HoTen"] = "Nguyễn Thị Diễm Khang";
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }
        // GET: PhieuNhapXuats/Delete/5
        public ActionResult XoaPhieuNhapKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("XoaPhieuNhapKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaPhieuNhapKhoConfirmed(string id)
        {
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            database.PhieuNhapXuats.Remove(phieuNhapXuat);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["MaPNKho"] = id;
            return RedirectToAction("NhapKho");
        }
        //-------------------------------------Hết phần Phiếu Nhập----------------------
        //-----------------------------Bắt đầu phiếu xuất - tồn kho------------------------------------
        public ActionResult TonKho()
        {
            return View();
        }

        // GET: PhieuNhapXuats/Create
        public ActionResult GiaiQuyetTonKho()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "MaSP");
            return View();
        }

        // POST: PhieuNhapXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiaiQuyetTonKho([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu")] PhieuNhapXuat phieuNhapXuat)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var maphieux = "PXK" + rd.Next(1, 1000);
                phieuNhapXuat.MaPhieu = maphieux;
                Session["phieuXuat"] = maphieux;
                phieuNhapXuat.NgayLap = System.DateTime.Now;
                phieuNhapXuat.LoaiPhieu = "Phiếu đề nghị xuất hàng tồn";
                Session["KinhGui"] = " ";
                Session["HoTen"] = " ";
                database.PhieuNhapXuats.Add(phieuNhapXuat);
                database.SaveChanges();
                TempData["AlertMessage"] = "Đã thêm";
                TempData["MaPNKho"] = maphieux;
                return RedirectToAction("QuanLyDL");
            }
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Details/5
        public ActionResult ChiTietPhieuXuatKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["maPhieuXuat"] = id;
            Session["KinhGui"] = "Trưởng phòng kinh doanh";
            Session["HoTen"] = "Nguyễn Thị Diễm Khang";
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }
        // GET: PhieuNhapXuats/Delete/5
        public ActionResult XoaPhieuXuatKho(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("XoaPhieuXuatKho")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaPhieuXuatKhoConfirmed(string id)
        {
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            database.PhieuNhapXuats.Remove(phieuNhapXuat);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["MaPNKho"] = id;
            return RedirectToAction("NhapKho");
        }

        //------------------------------------------Hết phần tồn kho - phiếu xuất---------------------------
        public ActionResult BaoCao()
        {
            //Chức vụ nhân viên lập biên bảng kiểm kê
            TempData["tencv1"] = TempData["tencv"];
            //tính tổng số kho
            var totalkho = database.Khoes.ToList().Count;
            TempData["TongKho"] = totalkho;
            //tính tổng số sản phẩm
            var totalsp = database.SanPhams.ToList().Count;
            TempData["Tongsp"] = totalsp;
            //tính tổng số Phiếu nhập xuất hàng
            var tongpn = 0;
            var tongpx = 0;
            var pnx = database.PhieuNhapXuats.ToList();
            foreach(var item in pnx)
            {
                string str = item.MaPhieu.Substring(0, 2); 
                if(str == "PX")
                {
                    tongpx = tongpx + 1;
                }
                else 
                { 
                    tongpn = tongpn + 1; 
                }
            }
            TempData["Tongpn"] = tongpn;
            TempData["Tongpx"] = tongpx;
            //tính tổng sản phẩm sắp hết hàng
            var dssphh = database.ChiTietKhoes.ToList();
            var total2 = 0;
            foreach (var item in dssphh)
            {
                if (item.TinhTrang == "Sắp hết hàng")
                {
                    total2 = total2 + 1;
                }
                else if (item.TinhTrang == "Hết hàng")
                {
                    total2 = total2 + 1;
                }
            }
            TempData["TongSPHH"] = total2;
            //tính tổng sản phẩm tồn kho
            var total3 = 0;
            foreach (var item in dssphh)
            {
                if (item.TinhTrang == "Tồn kho")
                {
                    total3 = total3 + 1;
                }
            }
            TempData["TongSPTK"] = total3;
            //var bienBangKiemKes = database.BienBangKiemKes.Include(b => b.NhanVien).Include(b => b.SanPham);
            return View(database.BienBangKiemKes.ToList().OrderByDescending(s => s.NgayLap));
        }

        // GET: BienBangKiemKes/Create
        public ActionResult CreateBBKK()
        {
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho");
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaNV");
            return View();
        }

        // POST: BienBangKiemKes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBBKK([Bind(Include = "MaKK,MaKho,MaNVLap,TenBienBang,NgayLap")] BienBangKiemKe bienBangKiemKe)
        {
            DataTable bb = new DataTable();
            if (Session["BBKK"] == null)
            {
                bb.Columns.Add("MaKK");
                bb.Columns.Add("MaKho");
                bb.Columns.Add("MaNVLap");
                bb.Columns.Add("NgayLap");
                bb.Columns.Add("TenBB");
                //bb.Columns.Add("MaNVLap");
                //Tạo xong lưu lại Session
                Session["BBKK"] = bb;
            }
            else
            {
                //Lấy thông tin từ Session
                bb = Session["BBKK"] as DataTable;
            }
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var mabb = "BB" + rd.Next(1, 1000);
                bienBangKiemKe.MaKK = mabb;
                Session["tempdata"] = mabb;
                bienBangKiemKe.NgayLap = System.DateTime.Now;
                if(bienBangKiemKe.TenBienBang != null)
                {
                    DataRow dr = bb.NewRow();
                    dr["MaKK"] = bienBangKiemKe.MaKK;
                    dr["MaNVLap"] = bienBangKiemKe.MaNVLap;
                    dr["MaKho"] = bienBangKiemKe.MaKho;
                    dr["TenBB"] = bienBangKiemKe.TenBienBang;
                    dr["NgayLap"] = bienBangKiemKe.NgayLap;
                    bb.Rows.Add(dr);
                    //database.BienBangKiemKes.Add(bienBangKiemKe);
                    //database.SaveChanges();
                    
                    return RedirectToAction("CreateCTBBKK");
                }
                else
                {
                    TempData["AlertMessage"] = "null";
                    return RedirectToAction("CreateBBKK");
                }
            }

            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", bienBangKiemKe.MaKho);
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaNV", bienBangKiemKe.MaNVLap);
            return View(bienBangKiemKe);
        }

        // GET: ChiTietBienBangs/Create
        public ActionResult CreateCTBBKK()
        {
            ViewBag.MaKK = new SelectList(database.BienBangKiemKes, "MaKK", "MaKho");
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietBienBangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCTBBKK([Bind(Include = "STT,MaKK,MaSP,SLTonKho,SLThucTe,ChenhLech,LyDo")] ChiTietBienBang chiTietBienBang)
        {
            if (ModelState.IsValid)
            {
                if(chiTietBienBang.SLThucTe != null && chiTietBienBang.LyDo != null)
                {
                    chiTietBienBang.MaKK = (string)Session["tempdata"];
                    Random rd = new Random();
                    //var mactbb = "CTBB" + rd.Next(1, 1000);
                    chiTietBienBang.STT = rd.Next(1, 1000);
                    BienBangKiemKe bienBangKiemKe = new BienBangKiemKe();

                    DataTable bb = Session["BBKK"] as DataTable;

                    foreach (DataRow dr in bb.Rows)
                    {
                        bienBangKiemKe.MaKK = dr["MaKK"].ToString();
                        bienBangKiemKe.MaKho = dr["MaKho"].ToString();
                        bienBangKiemKe.MaNVLap = dr["MaNVLap"].ToString();
                        var date = Convert.ToDateTime(dr["NgayLap"]);
                        bienBangKiemKe.NgayLap = date;
                        bienBangKiemKe.TenBienBang = dr["TenBB"].ToString();
                    }
                    var makhofromds = database.ChiTietKhoes.ToList();
                    var slton = 0;
                    foreach (var item in makhofromds)
                    {
                        if (item.MaKho == bienBangKiemKe.MaKho && item.MaSP == chiTietBienBang.MaSP)
                        {
                            slton += (int)item.SoLuong;
                        }
                    }
                    chiTietBienBang.SLTonKho = slton;
                    chiTietBienBang.ChenhLech = Math.Abs((int)chiTietBienBang.SLTonKho - (int)chiTietBienBang.SLThucTe) ;

                    database.BienBangKiemKes.Add(bienBangKiemKe);
                    database.SaveChanges();
                    database.ChiTietBienBangs.Add(chiTietBienBang);
                    database.SaveChanges();
                    TempData["AlertMessage"] = "Đã thêm";
                    return RedirectToAction("BaoCao");
                }
                else
                {
                    TempData["AlertMessage"] = "null";
                    return RedirectToAction("CreateCTBBKK");
                }
                
            }
            ViewBag.MaKK = new SelectList(database.BienBangKiemKes, "MaKK", "MaKho", chiTietBienBang.MaKK);
            ViewBag.MaSP = new SelectList(database.SanPhams, "MaSP", "TenSP", chiTietBienBang.MaSP); 
            return View(chiTietBienBang);
        }

        // GET: BienBangKiemKes/Delete/5
        public ActionResult DeleteBBKK(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            ChiTietBienBang ctbienBangKiemKe = database.ChiTietBienBangs.Where(s => s.MaKK == bienBangKiemKe.MaKK).FirstOrDefault();
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }
            return View(ctbienBangKiemKe);
        }

        // POST: BienBangKiemKes/Delete/5
        [HttpPost, ActionName("DeleteBBKK")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBBKKConfirmed(string id)
        {
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            ChiTietBienBang ctbienBangKiemKe = database.ChiTietBienBangs.Where(s => s.MaKK == bienBangKiemKe.MaKK).FirstOrDefault();
            database.BienBangKiemKes.Remove(bienBangKiemKe);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["MaBBKKe"] = id;
            return RedirectToAction("BaoCao");
        }

        // GET: ChiTietBienBangs/Details/5
        public ActionResult DetailsBB(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["makk"] = id;
            BienBangKiemKe bienBangKiemKe = database.BienBangKiemKes.Find(id);
            ChiTietBienBang ctbienBangKiemKe = database.ChiTietBienBangs.Where(s => s.MaKK == bienBangKiemKe.MaKK).FirstOrDefault();
            if (bienBangKiemKe == null)
            {
                return HttpNotFound();
            }

            //var cv = database.ChucVus.ToList();
            //foreach (var item in cv)
            //{
            //    if (bienBangKiemKe.NhanVien.MaChucVu == item.MaChucVu)
            //    {
            //        TempData["tencv"] = item.ChucVu1 + " " + item.ViTri;
            //        break;
            //    }
            //}
            return View(ctbienBangKiemKe);
            //ChiTietBienBang chiTietBienBang = database.ChiTietBienBangs.Find(id);
            //if (chiTietBienBang == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(chiTietBienBang);
        }
//----------------------------------------------------------------------------------------------------
        //Phiếu nhập xuất từng kho
        // GET: PhieuNhapXuats/Create
        public ActionResult CreatePXuat(string id)
        {
            
            TempData["mahdx"] = id;
            TempData["ngaylap"] = System.DateTime.Now;
            var maDH = id;
            foreach (var item in database.DonHangs)
            {
                if (maDH == item.MaDH)
                {
                    TempData["tenDL"] =  item.DaiLy.TenDL;
                    TempData["diemgiao"] =  item.DiemGiao;
                }
            }
            ViewBag.MaKho = new SelectList(database.Khoes, "TenKho", "MaKho");
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaChucVu");
            ViewBag.MaHD = new SelectList(database.DonHangs, "MaDH", "MaDH");
            return View();
        }

        // POST: PhieuNhapXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePXuat([Bind(Include = "MaPhieu,MaKho,NgayLap,LoaiPhieu,MaNVLap")] PhieuNhapXuat phieuNhapXuat, string ThongTinKho1, string vanchuyen, string nguoigiaohang)
        {
            if (ModelState.IsValid)
            {
                
                Random rd = new Random();
                var maphieu = "PHXHD0" + rd.Next(1, 1000);
                phieuNhapXuat.MaPhieu = maphieu;
                phieuNhapXuat.NgayLap = System.DateTime.Now;
                phieuNhapXuat.LoaiPhieu = "Phiếu xuất kho theo đơn hàng số "+ Session["madhxk"];
                var mk = ThongTinKho1;
                phieuNhapXuat.MaKho = mk;
                phieuNhapXuat.NguoiGiaoHang = nguoigiaohang;
                phieuNhapXuat.PhuongTienGiaoHang = vanchuyen;
                phieuNhapXuat.MaDH = (string)Session["madhxk"];
                var user = (Test02.Models.NhanVien)HttpContext.Session["user"];
                phieuNhapXuat.MaNVLap = user.MaNV;
                
                if (ThongTinKho1 != "----chọn kho----")
                {
                    TempData["makho001"] = ThongTinKho1;
                    database.PhieuNhapXuats.Add(phieuNhapXuat);
                    database.SaveChanges();
                    TempData["AlertMessage"] = "Đã thêm";
                    TempData["mphieu"] = maphieu;
                    return RedirectToAction("ChiTietDonHang", new RouteValueDictionary(
                                            new { controller = "PhongKho", action = "ChiTietDonHang", Id = Session["madhxk"] }));
                }
                else
                {
                    TempData["AlertMessage"] = "khonull";
                    return View(phieuNhapXuat);
                }
                
            }
            
            ViewBag.MaKho = new SelectList(database.Khoes, "MaKho", "TenKho", phieuNhapXuat.MaKho);
            ViewBag.MaNVLap = new SelectList(database.NhanViens, "MaNV", "MaChucVu", phieuNhapXuat.MaNVLap);
            return View(phieuNhapXuat);
        }

        //đơn hàng
        public ActionResult ChiTietDonHang(string id)
        {
            Session["madhxk"] = id;
            TempData["madh"] = id;
            return View(database.ChiTietDonHangs.ToList().Where(s => s.MaDH == id));
        }

        //Phiếu xuất theo hóa đơn
        //-----------------------------------------------------------------------
        //phiếu nhập xuất
        public ActionResult NhapKho()
        {
            return View(database.PhieuNhapXuats.ToList().OrderByDescending(s => s.NgayLap));
        }
        // GET: PhieuNhapXuats
        //public ActionResult Index()
        //{
        //    var phieuNhapXuats = database.PhieuNhapXuats.Include(p => p.Kho).Include(p => p.NhanVien);
        //    return View(phieuNhapXuats.ToList());
        //}

        // GET: PhieuNhapXuats/Details/5
        public ActionResult DetailsPXK(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            TempData["maPhieuXuattheodonhang"] = id;
            var maDH = phieuNhapXuat.MaDH;
            TempData["ctmadh"] = phieuNhapXuat.MaDH;
            foreach (var item in database.DonHangs)
            {
                if (maDH == item.MaDH)
                {
                    TempData["tenDL"] = item.DaiLy.TenDL;
                    TempData["diemgiao"] = item.DiemGiao;
                }
            }
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }

        // GET: PhieuNhapXuats/Delete/5
        public ActionResult DeletePNX(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            if (phieuNhapXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhapXuat);
        }
        // POST: PhieuNhapXuats/Delete/5
        [HttpPost, ActionName("DeletePNX")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePNXConfirm(string id)
        {
            PhieuNhapXuat phieuNhapXuat = database.PhieuNhapXuats.Find(id);
            TempData["AlertMessage"] = "Đã xóa";
            var maphieu = id;
            database.PhieuNhapXuats.Remove(phieuNhapXuat);
            database.SaveChanges();
            TempData["AlertMessage"] = "Đã xóa";
            TempData["xoamphieu"] = maphieu;
            return RedirectToAction("NhapKho");
        }

    }
}