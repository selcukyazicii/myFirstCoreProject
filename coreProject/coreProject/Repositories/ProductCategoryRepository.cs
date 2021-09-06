using coreProject.Contexts;
using coreProject.Entities;
using coreProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    //bir ürüne birden fazla kategori ekliyoruz
    //x id ürüne y id kategori atanacak/silinecek
    public class ProductCategoryRepository : GenericRepository<ProductCategories>, IProductCategoriesRepository
    {
        //kendisine verilen filtre ile tek bir kayıt getiren metot
        public ProductCategories GetFilter(Expression<Func<ProductCategories, bool>> GetFilter)
        {
            using var context = new YoutubeContext();
            return context.ProductCategories.FirstOrDefault(GetFilter);
        }
    }
}
