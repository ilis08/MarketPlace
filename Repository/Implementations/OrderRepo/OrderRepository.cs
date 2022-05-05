using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.BaseRepo;

namespace Repository.Implementations.OrderRepo
{
    public class OrderRepository : BaseRepo.Repository, IOrderRepository
    {
        public OrderRepository(RepositoryContext context) : base(context) { }


        public async Task<List<OrderDetailProduct>> GetOrderDetailProducts(int id) => 
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
            foreach (var item in order.OrderDetailProduct)
            {
                order.TotalPrice += item.Price;
            }
        }

        public virtual void CreateRangeOrder(Order order)
        {
            repositoryContext.AttachRange(order.OrderDetailProduct.Select(c => c));
        }

        public virtual void CreateUserOrder(Order order)
        {
            repositoryContext.Attach(order.OrderDetailUser).State = EntityState.Added;
        }

        public async Task CompleteOrder(int id)
        {
            var order = await FindByCondition<Order>(x => x.Id == id).SingleOrDefaultAsync();

            order.IsCompleted = true;

            repositoryContext.Orders.Attach(order);
            repositoryContext.Entry(order).Property(x => x.IsCompleted).IsModified = true;
            await repositoryContext.SaveChangesAsync();
        }
    }
}
