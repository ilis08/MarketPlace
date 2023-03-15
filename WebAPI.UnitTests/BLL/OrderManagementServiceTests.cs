using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.Implementations;
using Data.Context;
using Data.Entitites;
using Exceptions.NotFound;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.BLL
{
    [TestFixture]
    public class OrderManagementServiceTests : TestWithSqlite
    {
        private Mock<IOrderRepository> mockRepository;

        [SetUp]
        public async Task SetUp()
        {
            await CreateDatabaseAsync();
            mockRepository = new Mock<IOrderRepository>();
        }

        [Test]
        public async Task GetAsync_When_DatabaseContainsOrders_Returns_ListOfOrderGetDTO()
        {
            using var context = CreateContext();

            var orders = context.Orders.AsQueryable().BuildMock();

            mockRepository.Setup(x => x.FindAll<Order>()).Returns(orders);

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.GetAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetByIdAsync_When_DatabaseContainsOrderWithSpecificId_Returns_OrderGetByIdDTO()
        {
            using var context = CreateContext();

            int id = 1;

            var order = context.Orders.Where(x => x.Id == id)
                                                .Include(x => x.OrderDetailProduct)
                                                .ThenInclude(p => p.Product)
                                                .ToList();

            mockRepository.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order.BuildMock());

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Test]
        public void GetByIdAsync_When_DatabaseDoesNotContainsOrderWithSpecificId_Throws_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Order, bool>>>()))
                                                    .Throws(new NotFoundException(It.IsAny<int>(), nameof(Order)));


            var sut = new OrderManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.GetByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task SaveAsync_Should_AddNewOrderToDatabase_Returns_CreatedOrder()
        {

        }

        [Test]
        public async Task UpdateAsync_Should_UpdateExistingOrder_Returns_UpdatedOrder()
        {
            using var context = CreateContext();

            var orderDTO = new OrderDTO()
            {
                OrderId = 4,
                PaymentType = PaymentType.ByCash,
                UserId = 1,
                OrderDetailProducts = new List<OrderDetailProductsDTO>()
                {
                    new OrderDetailProductsDTO
                    {
                        Id = 2,
                        Count = 2,
                        ProductId = 1
                    }
                }
            };

            var order = new Order()
            {
                Id = orderDTO.OrderId,
                PaymentType = orderDTO.PaymentType,
                UserId = orderDTO.UserId,
                OrderDetailProduct = (ICollection<OrderDetailProduct>)orderDTO.OrderDetailProducts.Select(x => new OrderDetailProduct
                {
                    Id = x.Id,
                    Count = x.Count,
                    ProductId = x.ProductId,
                })
            };

            mockRepository.Setup(x => x.Update(It.IsAny<Order>())).Callback(() => context.Update(order));
            mockRepository.Setup(x => x.SaveChangesAsync()).Callback(async () => await context.SaveChangesAsync());

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.UpdateAsync(orderDTO);

            result.Should().NotBeNull();
        }

        [Test]
        public async Task CompleteOrderAsync_DatabaseContainsOrderWithSpecificId_MarkIsCompletedPropertyAsTrue()
        {
            using var context = CreateContext();

            int id = 1;

            mockRepository.Setup(x => x.CompleteOrder(id))
                .ReturnsAsync(await context.Orders.FirstOrDefaultAsync(x => x.Id == id))
                .Callback(async () => await CompleteOrderAsync(id, context));

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.CompleteOrderAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        private async Task CompleteOrderAsync(int id, RepositoryContext context)
        {
            var order = await context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            order.IsCompleted = true;

            context.Orders.Attach(order);
            context.Entry(order).Property(x => x.IsCompleted).IsModified = true;
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task DeleteOrderAsync_WhenDatabaseContainsOrderWithSpecificId_DeleteThisRecord()
        {
            using var context = CreateContext();

            int id = 1;

            var orderToDelete = await context.Orders.Where(x => x.Id == id).SingleOrDefaultAsync();

            mockRepository.Setup(x => x.FindByIdAsync<Order>(It.IsAny<int>()))
                                        .ReturnsAsync(orderToDelete);
            mockRepository.Setup(x => x.Delete(orderToDelete))
                                        .Callback(() => context.Orders.Remove(orderToDelete));
            mockRepository.Setup(x => x.SaveChangesAsync())
                                        .Callback(async() => await context.SaveChangesAsync());

            var sut = new OrderManagementService(mockRepository.Object);

            await sut.DeleteAsync(id);

            mockRepository.Verify(x => x.Delete(orderToDelete), Times.Once);
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void DeleteOrderAsync_WhenDatabaseDoesNotContainsOrderWithSpecificId_Throw_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByIdAsync<Order>(It.IsAny<int>())).ReturnsAsync(() => null);

            var sut = new OrderManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteAsync(It.IsAny<int>()));
        }
    }
}
