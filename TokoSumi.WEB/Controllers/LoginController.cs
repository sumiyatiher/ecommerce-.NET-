using Microsoft.AspNetCore.Mvc;
using System;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.REPO;
using Toko_Sumi.VIEWMODELS;

namespace TokoSumi.WEB.Controllers
{
	public class LoginController : Controller
	{
		private readonly Toko_SumiContext db;
		private readonly LoginRepo repoL;

		public LoginController(Toko_SumiContext db)
        {
			this.db = db;
			repoL = new LoginRepo(db);
        }
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SignUp()
        {
			return View();
        }

		[HttpPost]
		public IActionResult Validation(VMCustomer dataVM)

        {
			VMCustomer customer = repoL.Validation(dataVM);
			return Json(customer);
        }
        [HttpPost]
        public IActionResult Register(VMCustomer dataVM)
        {
			dataVM.IsDelete = false;
			dataVM.CreateBy = 1;
			dataVM.CreateDate = DateTime.Now;

            bool respon = repoL.register(dataVM);
			if(respon)
            {
				return RedirectToAction("Index");
            }
			return View();
        }
		[HttpPost]
		public JsonResult RegisterCek(VMCustomer dataVM)
        {
			bool isSuccess = false;
			isSuccess = repoL.register(dataVM);
			if (isSuccess)
			{
				return Json(new { status = "success", message = "Register success" });
			}
			
				return Json(new { status = "failed", message = "Register failed" });
        }
    }
}
