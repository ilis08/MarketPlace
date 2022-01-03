using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public enum Ordering
    {
        OrderByHighestPrice = 0,
        OrderByLowestPrice = 1
    }

    public class GetProductsParameters
    {
        public string Category { get; set; }

        public Ordering Ordering { get; set; }

        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 4;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

    }
}
