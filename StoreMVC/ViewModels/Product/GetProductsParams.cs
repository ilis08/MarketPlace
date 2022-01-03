using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels.Product
{
    public enum Ordering
    {
        OrderByHighestPrice = 0,
        OrderByLowestPrice = 1
    }
    public class GetProductsParams
    {
        public GetProductsParams(string category)
        {
            Category = category;
        }

        public GetProductsParams(string category, Ordering ordering)
        {
            Category = category;
            Ordering = ordering;
        }

        public GetProductsParams(string category, Ordering ordering, int pageSize)
        {
            Category = category;
            Ordering = ordering;
            PageSize = pageSize;
        }

        public GetProductsParams(string category, Ordering ordering, int pageSize, int pageNumber)
        {
            Category = category;
            Ordering = ordering;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public GetProductsParams()
        {

        }

        public string Category { get; set; }

        public Ordering Ordering { get; set; }

        public int PageSize { get; set; } = 4;

        public int PageNumber { get; set; } = 1;
    }
}
