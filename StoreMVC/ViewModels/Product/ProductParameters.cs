using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels.Product;

public enum Ordering
{
    OrderByHighestPrice = 0,
    OrderByLowestPrice = 1,
    OrderByNewest = 2
}
public class ProductParameters : RequestParameters
{
    public ProductParameters(string category)
    {
        Category = category;
    }

    public ProductParameters(string category, Ordering ordering)
    {
        Category = category;
        Ordering = ordering;
    }

    public ProductParameters(string category, Ordering ordering, int pageSize)
    {
        Category = category;
        Ordering = ordering;
        PageSize = pageSize;
    }

    public ProductParameters(string category, Ordering ordering, int pageSize, int pageNumber)
    {
        Category = category;
        Ordering = ordering;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public ProductParameters(string? productName, string category, Ordering? ordering, int pageSize, int pageNumber)
    {
        ProductName = productName;
        Category = category;
        Ordering = ordering;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public ProductParameters()
    {

    }

    public string? ProductName { get; set; }

    public string Category { get; set; }

    public Ordering? Ordering { get; set; }

    public RequestMetaData RequestMetaData { get; set; }
}
