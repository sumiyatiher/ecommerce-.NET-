using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.VIEWMODELS;

namespace Toko_Sumi.REPO
{
	public class LoginRepo
	{
        private readonly Toko_SumiContext db; 
        public LoginRepo(Toko_SumiContext db)
        {
            this.db = db;
        }

        public static IMapper GetMapper() //method menerjemahkan dm dan vm
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblCustomers, VMCustomer>();
                //<Asal,tujuan>
                cfg.CreateMap<VMCustomer, TblCustomers>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }


        public VMCustomer Validation(VMCustomer dataVM)
        {
            TblCustomers dataModel = db.TblCustomers
                                    .Where(a => a.Email == dataVM.Email && a.Password == dataVM.Password)
                                    .FirstOrDefault();
            dataVM = GetMapper().Map<VMCustomer>(dataModel);
            return dataVM;

        }

        public bool register(VMCustomer dataVM)
        {
            bool isSuccess = false;
            TblCustomers dataModel = GetMapper().Map<TblCustomers>(dataVM);

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
    }
}
