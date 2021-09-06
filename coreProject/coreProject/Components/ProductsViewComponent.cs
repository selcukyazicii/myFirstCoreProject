using coreProject.Contexts;
using coreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Components
{
    [ViewComponent]
    public class ProductsViewComponent:ViewComponent
    {
        private readonly IProductRepository _productRepository;
        public ProductsViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke(int? id,string q)
        {
            id = ViewBag.CategoryId;
            if (id!=null)
            {
                return View(_productRepository.GetWithId((int)id));
            }



            q = ViewBag.Search;
            YoutubeContext context = new YoutubeContext();
            var product = from m in context.Products
                          select m;
            if (!String.IsNullOrEmpty(q))
            {
                product = product.Where(x => x.Name.Contains(q));
                //product = product.Where(x => x.Name == q);
                return View(product.ToList());
            }
            return View(_productRepository.GetAlll());
        }
       
    }
}
