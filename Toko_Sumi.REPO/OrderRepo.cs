using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.VIEWMODELS;

namespace Toko_Sumi.REPO
{
    public class OrderRepo
    {
        private readonly Toko_SumiContext db;
        

        public OrderRepo(Toko_SumiContext db)
        {
            this.db = db;
        }

        

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblOrderHeaders, VMOrder>();
                cfg.CreateMap<VMOrder, TblOrderHeaders>();

                cfg.CreateMap<TblOrderDetails, VMOrder>();
                cfg.CreateMap<VMOrder, TblOrderDetails>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public List<VMOrder> AllData()
        {
            List<VMOrder> dataVM = new List<VMOrder>();
            List<TblOrderHeaders> dataModel = db.TblOrderHeaders
                                        .Where(a => a.IsDelete == false)
                                        .ToList();
            dataVM = GetMapper().Map<List<VMOrder>>(dataModel);
            return dataVM;

        }
        public string getCodeTrans()
        {
            DateTime tanggalNow = DateTime.Now;
            string strCode = $"XA-{tanggalNow.ToString("ddMMyyyy")}-0001";
            string nol = "0000";

            VMOrder dataVM = new VMOrder();

            TblOrderHeaders dataModel = db.TblOrderHeaders
                    .OrderByDescending(a => a.CodeTransaction)
                    .FirstOrDefault();

            if (dataModel != null)
            {
                string baru = strCode.Substring(12, 4);
                int Tambah = int.Parse(baru) + 1;
                nol += Tambah.ToString();
                baru = nol.Substring(Tambah.ToString().Length, 4);
                string code = strCode.Substring(0, 12) + baru;
                strCode = code;

            }

            return strCode;
        }

        public bool Order(List<VMOrder> list, int TotalProduct, int Estimate)
        {
            bool isSuccess = false;

            TblOrderHeaders head = new TblOrderHeaders();
            string CodeTransaction = getCodeTrans();

            try
            {
                head.IdCustomer = 1;
                head.CodeTransaction = CodeTransaction;
                head.Amount = Estimate;
                head.TotalQty = TotalProduct;
                head.IsCheckout = false;
                head.IsDelete = false;
                head.CreateBy = 1;
                head.CreateDate = DateTime.Now;
                db.Add(head);
                db.SaveChanges();

                foreach(VMOrder item in list)
                {   //insert per produk di detail
                    TblOrderDetails details= new TblOrderDetails();
                    details.IdHeader = head.IdHeader;
                    details.IdProduct = item.IdProduct;
                    details.Qty = item.Qty;
                    details.SumPrice = item.SumPrice;
                    details.IsDelete = false;
                    details.CreateBy = 1;
                    details.CreateDate = DateTime.Now;

                    db.Add(details);
                    db.SaveChanges();
                    //update stok
                    TblProduct product = db.TblProduct.Where(a => a.IdProduct == details.IdProduct)
                                        .FirstOrDefault();

                    if(product != null)
                    {
                        product.Stock = product.Stock - details.Qty;
                        product.UpdateBy = 1;
                        product.UpdateDate = DateTime.Now;

                        db.Update(product);
                        db.SaveChanges();

                    }
                    
                }
                isSuccess = true;
            }

            catch (Exception)
            {
                throw;
            }

          
            return isSuccess;
        }
        public List<VMOrder> getById(int IdHeader)
        {
            List<VMOrder> dataVM = new List<VMOrder>();
            //TblProduct dataModel = db.TblProduct.Where(a => a.IdProduct == IdProduct).FirstOrDefault();
            dataVM = (from h in db.TblOrderHeaders
                      join d in db.TblOrderDetails
                      on h.IdHeader equals d.IdHeader
                      join p in db.TblProduct
                      on d.IdProduct equals p.IdProduct
                      where h.IdHeader == IdHeader && h.IsDelete == false
                      select new VMOrder
                      {
                          IdHeader = h.IdHeader,
                          CodeTransaction = h.CodeTransaction,
                          Amount = h.Amount,
                          TotalQty = h.TotalQty,
                          CreateBy = h.CreateBy,
                          CreateDate = h.CreateDate,
                          IdDetail = d.IdDetail,
                          NameProduct = p.NameProduct,
                          Price = p.Price,
                          //UpdateBy = d.UpdateBy,
                          //UpdateDate = d.UpdateDate,
                          Qty = d.Qty,
                          SumPrice = d.SumPrice,
                          IdProduct = d.IdProduct

                      }).ToList();
           
            return dataVM;
        }

       




    }
}
