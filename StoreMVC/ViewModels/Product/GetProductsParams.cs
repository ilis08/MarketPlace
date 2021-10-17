using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels.Product
{
    public enum Ordering
    {
        OrderByDescending,
        OrderBy
    }
    public class GetProductsParams
    {
        public GetProductsParams(string category, Ordering ordering)
        {
            Category = category;
            Ordering = ordering;
        }

        public GetProductsParams()
        {

        }

        public string Category { get; set; }

        public Ordering Ordering { get; set; } = Ordering.OrderBy;
    }
}
