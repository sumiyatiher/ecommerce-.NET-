using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Toko_Sumi.DATAMODELS
{
    public partial class TblCategory
    {
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public string Description { get; set; }
        public bool? IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
