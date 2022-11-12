using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Toko_Sumi.DATAMODELS
{
    public partial class TblOrderHeaders
    {
        public int IdHeader { get; set; }
        public string CodeTransaction { get; set; }
        public int IdCustomer { get; set; }
        public decimal Amount { get; set; }
        public int TotalQty { get; set; }
        public bool IsCheckout { get; set; }
        public bool IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
