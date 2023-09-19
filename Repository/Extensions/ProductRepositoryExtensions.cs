using Data.Entitites;
using Repository.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> SearchByName(this IQueryable<Product> products, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return products.Where(e => e.ProductName.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Product> SearchByCategory(this IQueryable<Product> products, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return products.Where(e => e.Category.Title.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Product> FilterProducts(this IQueryable<Product> products,
        Ordering ordering, double? minPrice, double? maxPrice) => ordering switch
        {
            Ordering.OrderByHighestPrice => products.Where((p => p.Price >= minPrice && p.Price <= maxPrice))
                                                    .OrderByDescending(x => x.Price),
            Ordering.OrderByLowestPrice => products.Where((p => p.Price >= minPrice && p.Price <= maxPrice))
                                                    .OrderBy(x => x.Price),
            Ordering.OrderByNewest => products.Where((p => p.Price >= minPrice && p.Price <= maxPrice))
                                                    .OrderByDescending(x => x.Release),
            _ => throw new ArgumentOutOfRangeException(nameof(ordering), $"Not expected direction value: {ordering}")
        };

}

