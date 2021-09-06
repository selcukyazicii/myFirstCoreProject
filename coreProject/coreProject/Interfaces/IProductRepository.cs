using coreProject.Entities;
using coreProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        List<Categories> GetCategories(int productId);
        public void Add(ProductCategories productCategories);
        public void Delete(ProductCategories productCategories);
        public List<Product> GetWithId(int productId);
    }
}
