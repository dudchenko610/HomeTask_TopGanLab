using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeTask_TopGanLab.Models;
using HomeTask_TopGanLab.Interfaces;
using HomeTask_TopGanLab.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeTask_TopGanLab.Controllers
{
    public class ProductController : Controller
    {

        private ISessionService _sessionService;

        public ProductController(ISessionService sessionService)
        {
  
            _sessionService = sessionService;
        }

        // list of products
        public IActionResult Index()
        {
            List<Product> allProducts = _sessionService.GetAllProducts();
            return View(allProducts);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {

            if (model != null)
            {
                _sessionService.AddProductToSession(model);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProductDetails(int productId)
        {
            Product product = _sessionService.GetProductById(productId);

            if (product == null)
            {
                return RedirectToAction("Error", new { errorType  = 0 });
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult RemoveProduct(int productId)
        {

            try 
            {
                _sessionService.RemoveProductFromSession(productId);
            } 
            catch (Exception e)
            {
                return RedirectToAction("Error", new { errorType = 0 });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            Product product = _sessionService.GetProductById(productId);

            if (product == null) 
            {
                return RedirectToAction("Error", new { errorType = 0 });
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {

            _sessionService.SaveEditedProduct(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Error(int errorType)
        {

            ViewModels.ErrorViewModel model = new ViewModels.ErrorViewModel();
            switch (errorType) 
            {
                case 0:
                    model.ErrorMessage = "Упс:( Указанного продукта не существует.Возможно он был удален ранее или время ожидания истекло!";
                    return View(model);
                    
            }

            return RedirectToAction("Index");
        }


    }
}
