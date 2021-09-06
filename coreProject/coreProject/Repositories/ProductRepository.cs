using coreProject.Contexts;
using coreProject.Entities;
using coreProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        public ProductRepository(IProductCategoriesRepository productCategoriesRepository)
        {
            _productCategoriesRepository = productCategoriesRepository;
        }
        //Details sayfasında ilgili ürünün kategorilerini getireceğiz
        //Birden fazla kategoriye sahip olabileceği için List of Categories döndüreceğiz
        //Sen bana productId ver,ben ona göre ilgili kategorileri getireyim.
        public List<Categories> GetCategories(int productId)
        {
            using var context = new YoutubeContext();
            return context.Products.Join(context.ProductCategories, product => product.Id,
                 prodCategory => prodCategory.ProductId, (p, pc) => new
                 {
                     product = p,
                     prodCategory = pc
                 }).Join
                 (context.Categories, twin => twin.prodCategory.CategoryId,
                 category => category.Id, (pc, c) => new
                 {
                     product = pc.product,
                     category = c,
                     prodCategory = pc.prodCategory
                 }).Where(x => x.product.Id == productId).Select(x => new Categories
                 {
                     Name = x.category.Name,
                     Id = x.category.Id,

                 }).ToList();
        }
        public void Add(ProductCategories productCategories)
        {
            var check = _productCategoriesRepository.GetFilter(x => x.CategoryId == productCategories.CategoryId
              && x.ProductId == productCategories.ProductId);
            if (check == null)
            {
                _productCategoriesRepository.Add(productCategories);
            }
        }

        public void Delete(ProductCategories productCategories)
        {
            var check = _productCategoriesRepository.GetFilter(x => x.ProductId == productCategories.ProductId
              && x.CategoryId == productCategories.CategoryId);
            if (check != null)
            {
                _productCategoriesRepository.Delete(productCategories);
            }
        }

        public List<Product> GetWithId(int productId)
        {
            using var context = new YoutubeContext();
            return context.Products.Join(context.ProductCategories, u => u.Id, uc => uc.ProductId, (urun, urunCategory) => new
            {
                urun,
                urunCategory
            }).Where(x => x.urunCategory.CategoryId == productId).Select(x => new Product
            {
                Id=x.urun.Id,
                Name=x.urun.Name,
                Price=x.urun.Price,
                Photo=x.urun.Photo
            }).ToList();
        }
    }
}
