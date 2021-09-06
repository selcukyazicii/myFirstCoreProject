using coreProject.Entities;
using coreProject.Interfaces;
using coreProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoryController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public IActionResult Index()
        {
            return View(_categoriesRepository.GetAlll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCategoryModel());
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryModel createCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Add(new Categories
                {
                    Name = createCategoryModel.Name
                });
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var gelenUrun = _categoriesRepository.GetId(id);
            UpdateCategoryModel updateCategoryModel = new UpdateCategoryModel
            {
                Id = gelenUrun.Id,
                Name = gelenUrun.Name
            };
            return View(updateCategoryModel);
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryModel updateCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _categoriesRepository.GetId(updateCategoryModel.Id);
                guncellenecekUrun.Name = updateCategoryModel.Name;
                _categoriesRepository.Update(guncellenecekUrun);
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(updateCategoryModel);
        }
        public IActionResult Delete(int id)
        {
            _categoriesRepository.Delete(new Categories { Id = id });
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
    }
}
