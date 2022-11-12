using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.REPO;
using Toko_Sumi.VIEWMODELS;

namespace TokoSumi.WEB.Controllers
{

    public class CatalogController : Controller
    {
        private readonly Toko_SumiContext db;
        private readonly ProductRepo repoP;
        private readonly OrderRepo repoO;

        public CatalogController(Toko_SumiContext db)
        {
            this.db = db;//db dari context
            repoP = new ProductRepo(db);
            repoO = new OrderRepo(db);
        }
        public IActionResult Index()
        {
            List<VMProduct> list = repoP.AllData();
            return View(list);
        }

        

        
       
        [HttpPost]
        public IActionResult OrderPreview(List<VMOrder> cart, int TotalProduct, int Estimate)
        {
            ViewBag.TotalProduct = TotalProduct;
            ViewBag.Estimate = Estimate;


            return View(cart);
        }

        [HttpPost]
        public JsonResult OrderJson(List<VMOrder> cart, int TotalProduct, int Estimate)
        {
            bool isSuccess = false;
            isSuccess = repoO.Order(cart,TotalProduct, Estimate);
            if(isSuccess)
            {
                return Json(new { status = "success", message = "success order, thank you for order" });
            }
            return Json(new { status = "failed", message = "something wrong" });
        }
    }
}
