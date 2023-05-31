using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test02.Models;

namespace Test02.Models
{
    public class CartItem
    {
        public SanPham idSP { get; set; }
        public int soLuong { get; set; }
    }
    public class Cart
    {
        QuanLyDLproEntities2 db = new QuanLyDLproEntities2();

        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        //Thêm sản phẩm vào giỏ hàng
        public void Add(SanPham sp, int sl = 10)
        {
            var item = Items.FirstOrDefault(s => s.idSP.MaSP == sp.MaSP);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    idSP = sp,
                    soLuong = sl
                });
            }
            else
            {
                item.soLuong += sl;
            }
        }

        //Tính tổng số lượng trong giỏ
        public int TongSLTrongGio()
        {
            return items.Sum(s => s.soLuong);
        }

        //Tính thành tiền
        public double ThanhTien(string MaSP)
        {
            var item = items.Find(s => s.idSP.MaSP == MaSP);
            var thanhTien = item.idSP.Gia * item.soLuong;
            return (double)thanhTien;
        }

        //Tính tổng tiền
        public double TongTien()
        {
            var tong = items.Sum(s => s.soLuong * s.idSP.Gia);
            return (double)tong;
        }

        //Cập nhật số lượng
        public void CapNhatSL(string id, int slmoi)
        {
            var item = items.Find(s => s.idSP.MaSP == id);
            if (item != null)
            {
                item.soLuong = slmoi;
            }
        }

        //Xóa sản phẩm trong giỏ hàng
        public void XoaSP(string id)
        {
            items.RemoveAll(s => s.idSP.MaSP == id);
        }

        //Xóa sản sau khi đặt hàng
        public void XoaSauKhiDat()
        {
            items.Clear();
        }
    }
}
