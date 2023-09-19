using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.Helpers;

public class DatabaseInitializer
{
    private static readonly string path = @"../../../DatabaseMoq\";
    public static async Task InitializeAsync(RepositoryContext context)
    {
        await context.Categories.AddRangeAsync(await Load<Category>(path + "categories.json"));
        await context.Products.AddRangeAsync(await Load<Product>(path + "products.json"));

        await context.OrderDetailProducts.AddRangeAsync(await Load<OrderDetailProduct>(path + "orderDetailProducts.json"));
        await context.Orders.AddRangeAsync(await Load<Order>(path + "orders.json"));
    }

    private static async Task<IEnumerable<T>> Load<T>(string path) where T : class
    {
        try
        {
            var items = await JsonHelper.GetItemsFromFile<T>(path);

            return items;
        }
        catch (Exception)
        {
            return new List<T>();
        }
    }
}
