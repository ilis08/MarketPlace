using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels.Product;

public class ProductListVM
{
    public ProductListVM()
    {

    }

    public ProductListVM(ProductParameters parameters)
    {
        Params = parameters;
    }

    public IEnumerable<ProductVM> Products { get; set; }

    public IEnumerable<CategoryVM> Categories { get; set; }

    public ProductParameters Params { get; set; }
}
