using HomeTask_TopGanLab.Interfaces;
using HomeTask_TopGanLab.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask_TopGanLab.Services
{
    public class SessionService : ISessionService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
     //   private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            
        }

        public void AddProductToSession(Product product)
        {
            List<Product> products = GetAllProducts();
            product.Id = products.Count;
            products.Add(product);
            SavaProducts(products);
        }

        public void RemoveProductFromSession(int productId)
        {
            List<Product> products = GetAllProducts();

            Product product = products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception();
            }

            products.Remove(product);
            SavaProducts(products);


        }

        public void EditProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productId)
        {
            List<Product> products = GetAllProducts();
            return products.FirstOrDefault(x => x.Id == productId);
        }

        public List<Product> GetAllProducts() 
        {
            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("productlist"))
            {
                List<Product> products = _httpContextAccessor.HttpContext.Session.Get<List<Product>>("productlist");

                for (int i = 1; i <= products.Count; i ++) {
                    products[i - 1].Id = i;
                }

                return products;
            }
            else 
            {
                List<Product> products = new List<Product>();
                _httpContextAccessor.HttpContext.Session.Set<List<Product>>("productlist", products);

                return products;
            }
        }

        private void SavaProducts(List<Product> products) 
        {
            _httpContextAccessor.HttpContext.Session.Set<List<Product>>("productlist", products);
        }

        public void SaveEditedProduct(Product product)
        {
            List<Product> products = GetAllProducts();
            Product pr = products.FirstOrDefault(x => x.Id == product.Id);
        
            products.Insert(product.Id, product);
            products.Remove(pr);

            SavaProducts(products);

        }
    }
}
