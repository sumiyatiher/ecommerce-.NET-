using System;

namespace Toko_Sumi.VIEWMODELS
{
    public class VMCategory
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
