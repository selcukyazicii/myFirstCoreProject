using coreProject.Contexts;
using coreProject.Entities;
using coreProject.Interfaces;
using coreProject.Models;
using coreProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Controllers
{
    public class HomeController : Controller
    {


        private readonly SignInManager<AppUser> _signInManager;
        private readonly IProductRepository _productRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IBasketRepository _basketRepository;
        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager, ICategoriesRepository categoriesRepository, IBasketRepository basketRepository)
        {
            _signInManager = signInManager;
            _productRepository = productRepository;
            _categoriesRepository = categoriesRepository;
            _basketRepository = basketRepository;
        }
        public IActionResult Index(int? id, string q)
        {
            ViewBag.CategoryId = id;
            ViewBag.Search = q;
            return View("Index");
        }

        //home/index/id
        public IActionResult ProductDetails(int id)
        {
            return View(_productRepository.GetId(id));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                    //return RedirectToPageResult("/Home/Product", new { area = "Admin" });
                }

                ModelState.AddModelError("", "Try Again");
            }
            return View(model);
        }

        public IActionResult Basket()
        {
            return View(_basketRepository.GetBasketProduct());
        }
        public IActionResult EmtpyToBasket(decimal fiyat)
        {
            _basketRepository.EmptyToBasket();
            return RedirectToAction("Thanks",new { fiyat=fiyat});
        }
        public IActionResult Thanks(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }
        [HttpGet]
        public IActionResult AddToBasket(int id)
        {
            var product = _productRepository.GetId(id);
            _basketRepository.AddToBasket(product);
            TempData["alert"] = "Ürün Sepete Eklendi";
            return RedirectToAction("Index");
        }
        public IActionResult TakeOutBasket(int id)
        {
            var product = _productRepository.GetId(id);
            _basketRepository.DeleteToBasket(product);
            return RedirectToAction("Index");
        }
        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }
        [Route("/Error")]
        public IActionResult Error()
        {

            return View();
        }
    }


}
