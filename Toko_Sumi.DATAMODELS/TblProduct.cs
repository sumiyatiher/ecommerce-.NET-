using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Toko_Sumi.DATAMODELS
{
    public partial class TblProduct
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int IdVariant { get; set; }
        public string Image { get; set; }
        public bool IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
