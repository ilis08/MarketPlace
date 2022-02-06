using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RequestFeatures
{
    public enum Ordering
    {
        OrderByHighestPrice = 0,
        OrderByLowestPrice = 1
    }

    public class ProductParameters : RequestParameters
    {
        [Required]
        public string Category { get; set; }

        public Ordering Ordering { get; set; }
        
        [Required]
        public double MinPrice { get; set; }
        [Required]
        public double MaxPrice { get; set; } = double.MaxValue;

        public bool ValidPriceRange => MinPrice < MaxPrice;
    }
}
