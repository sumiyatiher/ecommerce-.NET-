using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Toko_Sumi.DATAMODELS;
using Toko_Sumi.REPO;
using Toko_Sumi.VIEWMODELS;
using TokoSumi.WEB.AddOns;

namespace TokoSumi.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly Toko_SumiContext db;
        private readonly ProductRepo Repo;
        private readonly VariantRepo RepoV;
        private readonly CategoryRepo RepoC;
        private readonly IWebHostEnvironment webHostEnvironment;


        public ProductController(Toko_SumiContext db, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            Repo = new ProductRepo(db);
            RepoV = new VariantRepo(db);
            RepoC = new CategoryRepo(db);
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index(string sortOrder,
                                   int IdProduct,
                                   string searchName,
                                   string currentFilter,
                                   int?pageNumber)
        {
            ViewBag.nameProduct = string.IsNullOrEmpty(sortOrder) ? "name_prod_desc" : "";
            ViewBag.IdProduct = sortOrder == "IdProduct" ? "id_prod_desc" : "IdProduct";
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
            List<VMProduct> list = Repo.AllData();

            if (!String.IsNullOrEmpty(searchName))
            {
                list = list.Where(p => p.NameProduct.ToLower().Contains(searchName)).ToList();

            }

            switch (sortOrder)
            {
                case "name_prod_desc":
                    list = list.OrderByDescending(p => p.NameProduct).ToList();
                    break;
                case "id_prod_desc":
                    list = list.OrderByDescending(p => p.IdProduct).ToList();
                    break;
                case "IdProduct":
                    list = list.OrderBy(p => p.IdProduct).ToList();
                    break;
                default:
                    list = list.OrderBy(p => p.NameProduct).ToList();
                    break;
            }
            int pageSize = 3;
            return View(PaginatedList<VMProduct>.Create(list,pageNumber ?? 1,pageSize));

        }

        [HttpGet]
        public IActionResult Create()
        {
            List<VMCategory> listCategory = RepoC.AllData();
            ViewBag.ListCategory = listCategory;
            List<VMVariant> listVariant = RepoV.AllData();
            ViewBag.ListVariant = listVariant;

            return View();
        }

        [HttpPost]
        public IActionResult Create(VMProduct dataVM)
        {
            dataVM.IsDelete = false;
            dataVM.CreateBy = 1;
            dataVM.CreateDate = DateTime.Now;
            string uniqueFileName = UploadedFile(dataVM);
            dataVM.Image = uniqueFileName;

            bool respon = Repo.Create(dataVM);

            if (respon)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int IdProduct)
        {
            VMProduct dataVM = Repo.getById(IdProduct);
            List<VMCategory> listCategory = RepoC.AllData();
            ViewBag.ListCategory = listCategory;
            List<VMVariant> listVariant = RepoV.getVariantByIdCategory(dataVM.IdCategory);
            ViewBag.ListVariant = listVariant;
           

            return View(dataVM);
        }

        [HttpPost]
        public IActionResult Edit(VMProduct dataVM)
        {
            dataVM.UpdateBy = 1;
            dataVM.UpdateDate = DateTime.Now;

            if(dataVM.ProductImage != null)
            {
                string uniqueName = UploadedFile(dataVM);
                dataVM.Image = uniqueName;
            }
            bool respon = Repo.Edit(dataVM);
            if (respon)
            {
                return RedirectToAction("Index");
            }
            return View(dataVM);
        }

        public IActionResult Detail(int IdProduct)
        {
            VMProduct dataVM = Repo.getById(IdProduct);
            return View(dataVM);

        }

        [HttpGet]
        public IActionResult Delete(int IdProduct)
        {
            VMProduct dataVM = Repo.getById(IdProduct);
            return View(dataVM);
        }

        [HttpPost]
        public IActionResult Delete(VMProduct dataVM)
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
        public JsonResult DataVariantAjAX(int idCategory)
        {
            List<VMVariant> list = RepoV.getVariantByIdCategory(idCategory);
            return Json(list);
        }

        private string UploadedFile(VMProduct model)
        {
            string uniqueFileName = null;
            if(model.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        }

    }

