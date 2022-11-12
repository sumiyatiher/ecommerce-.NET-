using System;
using System.Collections.Generic;
using System.Text;

namespace Toko_Sumi.VIEWMODELS
{
    public class VMVariant
    {
        public int IdVariant { get; set; }
        public string NameVariant { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
