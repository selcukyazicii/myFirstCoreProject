using coreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreProject.Entities;
using coreProject.Repositories;

namespace coreProject.Components
{
    [ViewComponent]
    public class CategoryViewComponent:ViewComponent
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        private ICategoriesRepository _categoriesRepository;
        public CategoryViewComponent(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IViewComponentResult Invoke()
        {      
            return View(_categoriesRepository.GetAlll());
        }
    }
}
