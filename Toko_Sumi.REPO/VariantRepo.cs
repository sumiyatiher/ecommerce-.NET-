using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.VIEWMODELS;

namespace Toko_Sumi.REPO
{
    public class VariantRepo
    {
        private Toko_SumiContext db;

        public VariantRepo(Toko_SumiContext db)
        {
            this.db = db;
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblVariant, VMVariant>();
                cfg.CreateMap<VMVariant, TblVariant>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
                
        }

        public List<VMVariant> AllData()
        {
            List<VMVariant> dataVM = new List<VMVariant>();
            //List<TblVariant> dataModel = db.TblVariant.Where(a=>a.IsDelete==false).ToList();
            //dataVM = GetMapper().Map<List<VMVariant>>(dataModel);  
            dataVM =(from v in db.TblVariant
                     join c in db.TblCategory
                     on v.IdCategory equals c.IdCategory
                     where v.IsDelete == false
                     select new VMVariant
                     {
                         IdVariant = v.IdVariant,
                         NameVariant = v.NameVariant,
                         Description = v.Description,
                         IsDelete = v.IsDelete,
                         CreateBy = v.CreateBy,
                         CreateDate = v.CreateDate,
                         UpdateBy = v.UpdateBy,
                         UpdateDate = v.UpdateDate,
                         IdCategory = v.IdCategory,
                         NameCategory = c.NameCategory

                     }).ToList();
            //dataVM = GetMapper().Map<List<VMVariant>>(dataModel);
            return dataVM;
        }
        
        public bool Create(VMVariant dataVM)
        {
            bool isSuccess = false;
            TblVariant dataModel = GetMapper().Map<TblVariant>(dataVM);

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
        public VMVariant getById(int IdVariant)
        {
            VMVariant dataVM = new VMVariant();
            //TblVariant dataModel = db.TblVariant.Where(a => a.IdVariant == IdVariant).FirstOrDefault();
            dataVM = (from v in db.TblVariant
                      join c in db.TblCategory
                      on v.IdCategory equals c.IdCategory
                      where v.IdVariant == IdVariant
                      select new VMVariant
                      {
                          IdVariant = v.IdVariant,
                          NameVariant = v.NameVariant,
                          Description = v.Description,
                          IsDelete = v.IsDelete,
                          CreateBy = v.CreateBy,
                          CreateDate = v.CreateDate,
                          UpdateBy = v.UpdateBy,
                          UpdateDate = v.UpdateDate,
                          IdCategory = v.IdCategory,
                          NameCategory = c.NameCategory

                      }).FirstOrDefault();
            //dataVM = GetMapper().Map<VMVariant>(dataModel);
            return dataVM;

        }
        public bool Edit(VMVariant dataVM)
        {
            bool isSuccess = false;
            TblVariant dataModel = db.TblVariant.Where(a => a.IdVariant == dataVM.IdVariant).FirstOrDefault();
            dataModel.IdCategory = dataVM.IdCategory;
            dataModel.NameVariant = dataVM.NameVariant;
            dataModel.Description = dataVM.Description;
            dataModel.UpdateBy = dataVM.UpdateBy;
            dataModel.UpdateDate = dataVM.UpdateDate;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                isSuccess=true;
            }
            catch(Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public bool Delete(VMVariant dataVM)
        {
            bool isSuccess = false;
            TblVariant dataModel = db.TblVariant.
                        Where(a => a.IdVariant == dataVM.IdVariant)
                        .FirstOrDefault();

            dataModel.IsDelete = true;
            //dataModel.IsDelete = true;
            dataModel.UpdateBy = dataVM.UpdateBy;
            dataModel.UpdateDate = dataVM.UpdateDate;

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
