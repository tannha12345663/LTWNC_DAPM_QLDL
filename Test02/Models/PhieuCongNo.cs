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
    
    public partial class PhieuCongNo
    {
        public int MaCongNo { get; set; }
        public string MaDL { get; set; }
        public string MaDH { get; set; }
        public Nullable<int> TienNo { get; set; }
        public Nullable<System.DateTime> HanTra { get; set; }
    
        public virtual DonHang DonHang { get; set; }
    }
}
