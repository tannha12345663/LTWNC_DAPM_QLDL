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
    
    public partial class DaiLy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaiLy()
        {
            this.DonHangs = new HashSet<DonHang>();
        }
    
        public string MaDL { get; set; }
        public string MaLoaiDL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TenDL { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> NgayTiepNhan { get; set; }
    
        public virtual LoaiDL LoaiDL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
