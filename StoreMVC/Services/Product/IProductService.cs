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
        Task GetProductAsync(int id);

        Task GetProductsAsync(string query);

        Task<ProductListVM> GetProductsByParams(ProductParameters productsParams);

        public ProductVM Product { get; set; }

        public IEnumerable<ProductVM> Products { get; set; }

        public ProductListVM ProductListVM { get; set; }
    }
}
