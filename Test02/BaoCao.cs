//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test02
{
    using System;
    using System.Collections.Generic;
    
    public partial class BaoCao
    {
        public string MaBC { get; set; }
        public string MaNV { get; set; }
        public string MaThongKe { get; set; }
        public string LoaiBC { get; set; }
        public Nullable<System.DateTime> NgayGioBC { get; set; }
        public string NoiDungBC { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
        public virtual ThongKe ThongKe { get; set; }
    }
}
