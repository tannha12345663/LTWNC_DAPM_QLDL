﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;

    public partial class PhieuCongNo
    {
        [DisplayName("Mã Công nợ")]
        public string MaCongNo { get; set; }

        [DisplayName("Mã Đại lý")]
        public string MaDL { get; set; }

        [DisplayName("Tiền nợ")]
        public Nullable<double> TienNo { get; set; }

        [DisplayName("Hạn trả")]
        public Nullable<System.DateTime> HanTra { get; set; }

        [DisplayName("Trạng thái thanh toán")]
        public string TrangThai { get; set; }
    
        public virtual DaiLy DaiLy { get; set; }
    }
}
