using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toko_Sumi.VIEWMODELS
{
    public class VMProduct
    {
        public int IdCategory { get; set; }
        public string NameCategori { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int IdVariant { get; set; }
        public string NameVariant { get; set; }
        public string Image { get; set; }
        public bool IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int IdProduct { get; set; }
        public string NameCategory { get; set; }

        public IFormFile ProductImage { get; set; }
    }
}
