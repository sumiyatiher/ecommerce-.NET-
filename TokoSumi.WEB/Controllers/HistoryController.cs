using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.REPO;
using Toko_Sumi.VIEWMODELS;
using System.Linq;

namespace TokoSumi.WEB.Controllers
{
	public class HistoryController : Controller
	{
		private readonly Toko_SumiContext db;
		private readonly OrderRepo repoO;

		public HistoryController(Toko_SumiContext db)
		{
			this.db = db;//db dari context
			
			repoO = new OrderRepo(db);
		}
		public IActionResult History()
		{

			List<VMOrder> list = repoO.AllData();
			return View(list);
		}

		public IActionResult Detail(int IdHeader)
		{
			
			List<VMOrder> dataVM = repoO.getById(IdHeader);
			VMOrder dataModel = dataVM.FirstOrDefault();
			ViewBag.Data = dataModel;
			
			return View(dataVM);

		}
	}
}
