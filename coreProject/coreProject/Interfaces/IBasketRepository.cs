using coreProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Interfaces
{
    public interface IBasketRepository
    {
        public void AddToBasket(Product product);
        public void DeleteToBasket(Product product);
        public List<Product> GetBasketProduct();
        public void EmptyToBasket();

    }
}
