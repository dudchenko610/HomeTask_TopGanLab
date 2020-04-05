using HomeTask_TopGanLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask_TopGanLab.Interfaces
{
    public interface ISessionService
    {
        void SaveEditedProduct(Product product);
        void AddProductToSession(Product product);
        void RemoveProductFromSession(int productId);
        void EditProduct(int productId, Product product);
        Product GetProductById(int productId);
        List<Product> GetAllProducts();
    }
}
