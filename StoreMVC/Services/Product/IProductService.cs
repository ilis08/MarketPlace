using StoreMVC.ViewModels;
using StoreMVC.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Service
{
    public interface IProductService
    {
        Task<ProductVM> GetProductAsync(int id);

        Task<List<ProductVM>> GetProductsAsync(string query);

        Task<ProductListVM> GetProductsByParams(ProductParameters productsParams);
    }
}
