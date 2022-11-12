using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.VIEWMODELS;

namespace Toko_Sumi.REPO
{
    public class CategoryRepo
    {
        private readonly Toko_SumiContext db; //koneksi database,deklarasi, inisialisasi sebagai db
        public CategoryRepo(Toko_SumiContext db)
        {
            this.db = db;//db dari context
        }
        public static IMapper GetMapper() //method menerjemahkan dm dan vm
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblCategory, VMCategory>();
                //<Asal,tujuan>
                cfg.CreateMap<VMCategory, TblCategory>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public List<VMCategory> AllData()
        {
            List<VMCategory> dataVM = new List<VMCategory>();

            List<TblCategory> dataModel = db.TblCategory
                                            .Where(a => a.IsDelete == false)
                                            .ToList();

            dataVM = GetMapper().Map<List<VMCategory>>(dataModel);
            return dataVM;
        }

        public bool Create(VMCategory dataVM)
        {
            bool isSuccess = false;
            TblCategory dataModel = GetMapper().Map<TblCategory>(dataVM);


            try
            {
                db.Add(dataModel);
                db.SaveChanges();
                isSuccess = true;
            }
            catch(Exception)
            {
                throw;
            }

            return isSuccess;
        }
        public bool Edit(VMCategory dataVM)
        {
            bool isSuccess = false;
            TblCategory dataModel = db.TblCategory.Where(a=>a.IdCategory== dataVM.IdCategory).FirstOrDefault();
            dataModel.NameCategory = dataVM.NameCategory;
            dataModel.Description = dataVM.Description;
            dataModel.UpdateBy = dataVM.UpdateBy;
            dataModel.UpdateDate = dataVM.UpdateDate;

            try
            {
               db.Update(dataModel);
               db.SaveChanges();
               isSuccess=true;
            }
            catch (Exception)
            {
                throw;
            }

            return isSuccess;

        }
        public bool Delete(VMCategory dataVM)
        {
            bool isSuccess = false;
            TblCategory dataModel = db.TblCategory.Where(a => a.IdCategory == dataVM.IdCategory).FirstOrDefault();

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
        public VMCategory getById(int IdCategory)
        {

            VMCategory dataVM = new VMCategory();
            TblCategory dataModel = db.TblCategory.Where(a => a.IdCategory == IdCategory).FirstOrDefault();
            dataVM = GetMapper().Map<VMCategory>(dataModel);
            return dataVM;
        }
    }
}
