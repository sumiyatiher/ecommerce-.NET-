using System;
using System.Collections.Generic;
using System.Text;

namespace Toko_Sumi.VIEWMODELS
{
	public class VMCustomer
	{
        public int IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? IdRole { get; set; }
        public bool IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
