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
    public class VariantController : Controller
    {
        private readonly Toko_SumiContext db;
        private readonly VariantRepo Repo;
        private readonly CategoryRepo RepoC;


        public VariantController(Toko_SumiContext db)
        {
            this.db = db;
            Repo = new VariantRepo(db);
            RepoC = new CategoryRepo(db);
        }


        public IActionResult Index(string sortOrder,
                                    int IdVariant,
                                    string searchName,
                                    string currentFilter,
                                    int?pageNumber  )
        {
            ViewBag.nameVariant = string.IsNullOrEmpty(sortOrder) ? "name_var_desc" : "";
            ViewBag.idVariant = sortOrder == "IdVariant" ? "id_var_desc" : "IdVariant";
            ViewBag.CurrentSort = sortOrder;

            DateTime dtNow = DateTime.Now;
            ViewBag.Tanggal = dtNow;

            if(searchName != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchName = currentFilter;
            }
            ViewBag.nama = searchName;
            List<VMVariant> list = Repo.AllData();

            if (!String.IsNullOrEmpty(searchName))
            {
                list = list.Where(v => v.NameVariant.ToLower().Contains(searchName)).ToList();
            }

            switch (sortOrder)
            {
                case "name_var_desc":
                    list = list.OrderByDescending(v => v.NameVariant).ToList();
                    break;
                case "id_var_desc":
                    list = list.OrderByDescending(v => v.IdVariant).ToList();
                    break;
                case "IdVariant":
                    list = list.OrderBy(v => v.IdVariant).ToList();
                    break;
                default:
                    list = list.OrderBy(v => v.NameVariant).ToList();
                    break;
            }

            int pageSize = 3;
            return View(PaginatedList<VMVariant>.Create(list,pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<VMCategory> listCategory = RepoC.AllData();
            ViewBag.ListCategory = listCategory;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VMVariant dataVM)
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
        public IActionResult Edit(int IdVariant)
        {
            List<VMCategory> listCategory = RepoC.AllData();
            ViewBag.ListCategory = listCategory;
            VMVariant dataVM = Repo.getById(IdVariant);

            return View(dataVM);
        }

        [HttpPost]
        public IActionResult Edit(VMVariant dataVM)
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

        public IActionResult Detail(int IdVariant)
        {
            VMVariant dataVM = Repo.getById(IdVariant);
            return View(dataVM);

        }

        [HttpGet]
        public IActionResult Delete(int IdVariant)
        {
            VMVariant dataVM = Repo.getById(IdVariant);
            return View(dataVM);
        }
        [HttpPost]
        public IActionResult Delete(VMVariant dataVM)
        {
            dataVM.UpdateBy = 1;
            dataVM.UpdateDate = DateTime.Now;

            bool respon = Repo.Delete(dataVM);
            if(respon)
            {
                return RedirectToAction("Index");
            }
            return View(dataVM);
        }
    }

}
