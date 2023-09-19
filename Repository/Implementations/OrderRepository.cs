using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.Implementations;

public class OrderRepository : Repository, IOrderRepository
{
    public OrderRepository(RepositoryContext context) : base(context) { }


    public async Task<List<OrderDetailProduct>> GetOrderDetailProducts(long id) =>
        await repositoryContext.OrderDetailProducts.Where(p => p.OrderId == id).ToListAsync();

    public async Task ComputeTotalPriceAsync(List<OrderDetailProduct> products)
    {
        foreach (var product in products)
        {
            var price = await repositoryContext.Products.Where(o => o.Id == product.Product.Id).Select(c => c.Price).SingleOrDefaultAsync();

            product.Price = price;

            product.Price = product.Count * product.Price;
        }
    }

    public void ComputeTotalPriceForOrder(Order order)
    {
       /* foreach (var item in order.OrderDetailProduct)
        {
            order.TotalPrice += item.Price;
        }*/

        order.TotalPrice = order.OrderDetailProduct.Select(c => c.Price * c.Count).Sum();
    }

    public virtual void CreateRangeOrder(Order order)
    {
        repositoryContext.AttachRange(order.OrderDetailProduct.Select(c => c));
    }

    public async Task<Order> CompleteOrder(long id)
    {
        var order = await FindByCondition<Order>(x => x.Id == id).SingleOrDefaultAsync();

        order.IsCompleted = true;

        repositoryContext.Orders.Attach(order);
        repositoryContext.Entry(order).Property(x => x.IsCompleted).IsModified = true;
        await repositoryContext.SaveChangesAsync();

        return order;
    }
}
