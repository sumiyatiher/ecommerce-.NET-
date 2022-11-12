using System;
using System.Collections.Generic;
using System.Text;

namespace Toko_Sumi.VIEWMODELS
{
    public class VMOrder
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

        public int IdDetail { get; set; }
        public int IdProduct { get; set; }
        public int Qty { get; set; }
        public decimal SumPrice { get; set; }

        public string NameProduct { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
