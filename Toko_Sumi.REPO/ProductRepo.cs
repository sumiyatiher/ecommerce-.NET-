using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.VIEWMODELS;
using System.Linq;

namespace Toko_Sumi.REPO
{
    public class ProductRepo
    {
        private Toko_SumiContext db;
        
        public ProductRepo(Toko_SumiContext db)
        {
            this.db = db;
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblProduct, VMProduct>();
                cfg.CreateMap<VMProduct, TblProduct>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public List<VMProduct> AllData()
        {
            List<VMProduct> dataVM = new List<VMProduct>();
            dataVM = (from v in db.TblVariant 
                      join c in db.TblCategory
                      on v.IdCategory equals c.IdCategory
                      join p in db.TblProduct
                      on v.IdVariant equals p.IdVariant
                      where p.IsDelete == false
                      select new VMProduct
                      {
                          IdProduct = p.IdProduct,
                          NameProduct = p.NameProduct,
                          Price = p.Price,
                          Stock = p.Stock,
                          Image = p.Image,
                          IsDelete = p.IsDelete,
                          CreateBy = p.CreateBy,
                          CreateDate = p.CreateDate,
                          UpdateBy = p.UpdateBy,
                          UpdateDate = p.UpdateDate,
                          IdVariant = p.IdVariant,
                          NameVariant = v.NameVariant,
                          IdCategory = c.IdCategory,
                          NameCategory = c.NameCategory
                      }).ToList();
            return dataVM;
        }

        public bool Create(VMProduct dataVM)
        {
            bool isSuccess = false;
            TblProduct dataModel = GetMapper().Map<TblProduct>(dataVM);

            try
            {
                db.Add(dataModel);
                db.SaveChanges();
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public VMProduct getById(int IdProduct)
        {
            VMProduct dataVM = new VMProduct();
            //TblProduct dataModel = db.TblProduct.Where(a => a.IdProduct == IdProduct).FirstOrDefault();
            dataVM = (from v in db.TblVariant
                      join c in db.TblCategory
                      on v.IdCategory equals c.IdCategory
                      join p in db.TblProduct
                      on v.IdVariant equals p.IdVariant
                      where p.IdProduct == IdProduct
                      select new VMProduct
                      {
                          IdProduct = p.IdProduct,
                          NameProduct = p.NameProduct,
                          Price = p.Price,
                          Stock = p.Stock,
                          Image = p.Image,
                          IsDelete = p.IsDelete,
                          CreateBy = p.CreateBy,
                          CreateDate = p.CreateDate,
                          UpdateBy = p.UpdateBy,
                          UpdateDate = p.UpdateDate,
                          IdVariant = p.IdVariant,
                          IdCategory = c.IdCategory,
                          NameVariant = v.NameVariant,
                          NameCategory = c.NameCategory
                      }).FirstOrDefault();
            //dataVM = GetMapper().Map<VMProduct>(dataModel);
            return dataVM;
        }

       

        public bool Edit(VMProduct dataVM)
        {
            bool isSuccess = false;
            TblProduct dataModel = db.TblProduct.Where(a => a.IdProduct == dataVM.IdProduct).FirstOrDefault();

            dataModel.IdVariant = dataVM.IdVariant;
            dataModel.NameProduct = dataVM.NameProduct;
            dataModel.Stock = dataVM.Stock;
            dataModel.Price = dataVM.Price;
            dataModel.Image = dataVM.Image;
            dataModel.UpdateBy = dataVM.UpdateBy;
            dataModel.UpdateDate = dataVM.UpdateDate;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                isSuccess = true;
            }
            catch(Exception)
            {
                throw;
            }
            return isSuccess;
        }

       

      

        public bool Delete(VMProduct dataVM)
        {
            bool isSuccess = false;
            TblProduct dataModel = db.TblProduct.
                                   Where(a => a.IdProduct == dataVM.IdProduct)
                                   .FirstOrDefault();
            dataModel.IsDelete = true;
            dataModel.UpdateBy = dataVM.UpdateBy;
            dataModel.UpdateDate= dataVM.UpdateDate;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;

        }

        public List<VMVariant> getVariantByIdCategory(int idCategory)
        {
            List<VMVariant> dataVM = new List<VMVariant>();
            List<TblVariant> dataModel = db.TblVariant.Where(a => a.IdCategory == idCategory).ToList();

            dataVM = GetMapper().Map<List<VMVariant>>(dataModel);
            return dataVM;
        }

        
    }
}
