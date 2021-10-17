using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public enum Ordering
    {
        OrderByDescending,
        OrderBy
    }

    public class GetProductsParameters
    {
        public string Category { get; set; }

        public Ordering Ordering { get; set; }

        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

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
