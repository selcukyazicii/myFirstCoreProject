using coreProject.CustomExtensions;
using coreProject.Entities;
using coreProject.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BasketRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddToBasket(Product product)
        {
            var inComingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
            if (inComingList==null)
            {
                inComingList = new List<Product>();
                inComingList.Add(product);
            }
            else
            {
                inComingList.Add(product);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("basket", inComingList);
        }
        public void DeleteToBasket(Product product)
        {
            var inGoingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
            inGoingList.Remove(product);
            _httpContextAccessor.HttpContext.Session.SetObject("basket",inGoingList);
        }
        public List<Product> GetBasketProduct()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
        }
        public void EmptyToBasket()
        {
            _httpContextAccessor.HttpContext.Session.Remove("basket");
        }
    }
}
