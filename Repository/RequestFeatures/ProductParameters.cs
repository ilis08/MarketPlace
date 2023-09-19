using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.RequestFeatures;

public enum Ordering
{
    OrderByHighestPrice,
    OrderByLowestPrice,
    OrderByNewest
}

public class ProductParameters : RequestParameters
{
    public string? ProductName { get; set; }
    [Required]
    public string Category { get; set; }

    public Ordering Ordering { get; set; } = Ordering.OrderByLowestPrice;

    public double MinPrice { get; set; } = 1.0;

    public double MaxPrice { get; set; } = double.MaxValue;

    internal bool ValidPriceRange => MinPrice < MaxPrice;
}
