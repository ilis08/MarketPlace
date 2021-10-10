using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Service
{
    public interface IProductService
    {
        Task GetProductAsync(int id);

        Task GetProductsAsync();

        public ProductVM Product { get; set; }

        public IEnumerable<ProductVM> Products { get; set; }
    }
}
