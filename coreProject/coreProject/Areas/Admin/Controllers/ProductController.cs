using coreProject.Entities;
using coreProject.Interfaces;
using coreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AllowAnonymous]

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public ProductController(IProductRepository productRepository, ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAlll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateProductModel());
        }
        [HttpPost]
        public IActionResult Create(CreateProductModel createProductModel)
        {

            if (ModelState.IsValid)
            {
                Product product = new Product();

                if (createProductModel.Photo != null)
                {
                    var uzanti = Path.GetExtension(createProductModel.Photo.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResimAd);
                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    createProductModel.Photo.CopyTo(stream);
                    product.Photo = yeniResimAd;
                }
                product.Name = createProductModel.Name;
                product.Price = createProductModel.Price;
                _productRepository.Add(product);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View(createProductModel);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var gelenUrun = _productRepository.GetId(id);
            UpdateProductModel updateProductModel = new UpdateProductModel
            {
                Id = gelenUrun.Id,
                Name = gelenUrun.Name,
                Price = gelenUrun.Price,
            };
            return View(updateProductModel);
        }
        [HttpPost]
        public IActionResult Update(UpdateProductModel updateProductModel)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _productRepository.GetId(updateProductModel.Id);

                if (updateProductModel.Photo != null)
                {
                    var uzanti = Path.GetExtension(updateProductModel.Photo.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResimAd);
                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    updateProductModel.Photo.CopyTo(stream);
                    guncellenecekUrun.Photo = yeniResimAd;
                }
                //guncellenecekUrun.Id = updateProductModel.Id;
                guncellenecekUrun.Name = updateProductModel.Name;
                guncellenecekUrun.Price = updateProductModel.Price;


                _productRepository.Update(guncellenecekUrun);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View(updateProductModel);
        }

        public IActionResult Delete(int id)
        {
            _productRepository.Delete(new Product { Id = id });
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

       
        [HttpGet]
        public IActionResult AssignCategory(int id)
        {
            var categoryOfProduct = _productRepository.GetWithId(id).Select(x=>x.Name);
            var categories = _categoriesRepository.GetAlll();
            TempData["ProductId"] = id;
            List<AssignCategoryModel> list = new List<AssignCategoryModel>();
            foreach (var item in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = item.Id;
                model.CategoryName = item.Name;
                model.IsThere = categoryOfProduct.Contains(item.Name);
                list.Add(model);
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult AssignCategory(List<AssignCategoryModel> list)
        {
            int productId = (int)TempData["ProductId"];
            foreach (var item in list)
            {
                if (item.IsThere)
                {
                    _productRepository.Add(new ProductCategories
                    {
                        CategoryId = item.CategoryId,
                        ProductId = productId
                    });
                }
                else
                {
                    _productRepository.Delete(new ProductCategories
                    {
                        CategoryId = item.CategoryId,
                        ProductId = productId
                    });
                }
            }
            return RedirectToAction("Index");
        }

    }

}
