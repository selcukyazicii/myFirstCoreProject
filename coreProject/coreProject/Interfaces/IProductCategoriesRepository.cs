using coreProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace coreProject.Interfaces
{
    //gönderilen filtreyi sağlayan kayıtları getirecek
    public interface IProductCategoriesRepository:IGenericRepository<ProductCategories>
    {
        public ProductCategories GetFilter(Expression<Func<ProductCategories, bool>> GetFilter);
    }
}
