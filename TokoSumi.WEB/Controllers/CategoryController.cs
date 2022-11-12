using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.REPO;
using Toko_Sumi.VIEWMODELS;
using TokoSumi.WEB.AddOns;
using TokoSumi.WEB.Models;

namespace TokoSumi.WEB.Controllers
{
    public class CategoryController : Controller
    {
        //private static List<CategoryModel> list = new List<CategoryModel>()
        //{
        //    new CategoryModel() {Id = 1,nama_category="Food",description="Category of Food" },
        //    new CategoryModel() {Id = 2,nama_category="Drink",description="Category of Drink" }
        //};

        private readonly Toko_SumiContext db;
        private readonly CategoryRepo Repo;
        public CategoryController(Toko_SumiContext db)
        {
            this.db = db;
            Repo = new CategoryRepo(db);
        }
        public IActionResult Index(string sortOrder, int IdCategory, string searchName, string currentFilter, int? pageNumber)
        {
            ViewBag.nameCategory = String.IsNullOrEmpty(sortOrder) ? "name_cat_desc" : "";
            ViewBag.idCategory = sortOrder == "IdCategory" ? "id_cat_desc" : "IdCategory";
            ViewBag.CurrentSort = sortOrder;

            DateTime dtNow = DateTime.Now;
            ViewBag.Tanggal = dtNow;

            
            if (searchName != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchName = currentFilter;
            }
            ViewBag.nama = searchName;
            List<VMCategory> list = Repo.AllData();

            if (!String.IsNullOrEmpty(searchName))
            {
                list = list.Where(c => c.NameCategory.ToLower().Contains(searchName)).ToList();
                //ViewBag.nama = searchName;
            }
            
            switch (sortOrder)
            {
                case "name_cat_desc":
                    list = list.OrderByDescending(c => c.NameCategory).ToList();
                    break;
                case "id_cat_desc":
                    list = list.OrderByDescending(c => c.IdCategory).ToList();
                    break;
                case "IdCategory":
                    list = list.OrderBy(c => c.IdCategory).ToList();
                    break;
                default:
                    list = list.OrderBy(c => c.NameCategory).ToList();
                    break;
            }
            int pageSize = 3;
            //return View(list);
            return View(PaginatedList<VMCategory>.Create(list, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VMCategory dataVM)
        {
            dataVM.IsDelete = false;
            dataVM.CreateBy = 1;
            dataVM.CreateDate = DateTime.Now;

            bool respon = Repo.Create(dataVM);

            if (respon)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int IdCategory)
        {
            VMCategory dataVM = Repo.getById(IdCategory);

            return View(dataVM);
        }

        [HttpPost]

        public IActionResult Edit(VMCategory dataVM)
        {
            dataVM.UpdateBy = 1;
            dataVM.UpdateDate = DateTime.Now;

            bool respon = Repo.Edit(dataVM);
            if(respon)
            {
                return RedirectToAction("Index");
            }
            return View(dataVM);


        }

        public IActionResult Detail(int IdCategory)
        {
            VMCategory dataVM = Repo.getById(IdCategory);
            return View(dataVM);
        }

        //public bool Delete(VMCategory dataVM)
        //{

        //}
        [HttpGet]
        public IActionResult Delete(int IdCategory)
        {
            VMCategory dataVM = Repo.getById(IdCategory);
            return View(dataVM);
        }
        [HttpPost]
        public IActionResult Delete(VMCategory dataVM)
        {
            dataVM.UpdateBy = 1;
            dataVM.UpdateDate = DateTime.Now;

            bool respon = Repo.Delete(dataVM);
            if (respon)
            {
                return RedirectToAction("Index");
            }
            return View(dataVM);


        }
    }
}
