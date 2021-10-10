using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        internal Store4DBContext context;

        public OrderRepository(Store4DBContext context)
        {
            this.context = context;
        }

        public void Create(Order order)
        {
            context.Add(order);
        }

        public void Delete(Task<Order> entity)
        {
            context.Remove(entity);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await context.Orders.Where(p => p.Id == id).Include(x => x.OrderDetailUser).Include(o => o.OrderDetailProduct).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        public List<OrderDetailProduct> GetOrderDetailProducts(int id)
        {
            return context.OrderDetailProducts.Where(p => p.OrderId == id).ToList();
        }

        public void Update(Order entity)
        {
            context.Update(entity);
        }

        public void ComputeTotalPrice(List<OrderDetailProduct> products)
        {
            foreach (var product in products)
            {
                var price = context.Products.Where(o => o.Id == product.Product.Id).Select(c => c.Price).FirstOrDefault();

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
            context.AttachRange(order.OrderDetailProduct.Select(c => c));
        }

        public virtual void CreateUserOrder(Order order)
        {
            context.Attach(order.OrderDetailUser).State = EntityState.Added;
        }

        public async Task CompleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);

            order.IsCompleted = true;

            context.Orders.Attach(order);
            context.Entry(order).Property(x => x.IsCompleted).IsModified = true;
            await context.SaveChangesAsync();
        }
    }
}
