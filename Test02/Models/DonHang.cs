//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test02.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    
        public string MaDH { get; set; }
        public string MaDL { get; set; }
        public string MaNVLap { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string TrangThai { get; set; }
        public string TinhTrangThanhToan { get; set; }
        public string DiemGiao { get; set; }
        public Nullable<double> TongTien { get; set; }
        public Nullable<bool> PhieuXuatKho { get; set; }
        public Nullable<bool> XuatHoaDon { get; set; }
        public string TinhTrangGH { get; set; }
        public string MaGH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ChuyenGiao ChuyenGiao { get; set; }
        public virtual DaiLy DaiLy { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
