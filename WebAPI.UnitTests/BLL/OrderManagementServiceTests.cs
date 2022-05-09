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
        public async Task Get_When_DatabaseContainsOrders_Returns_ListOfOrderGetDTO()
        {
            using var context = CreateContext();

            var orders = context.Orders.AsQueryable().BuildMock();

            mockRepository.Setup(x => x.FindAll<Order>()).Returns(orders);

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.Get();

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Get_When_DatabaseContainsOrderWithSpecificId_Returns_OrderGetByIdDTO()
        {
            using var context = CreateContext();

            int id = 1;

            var order = context.Orders.Where(x => x.Id == id)
                                                .Include(x => x.OrderDetailUser)
                                                .Include(x => x.OrderDetailProduct)
                                                .ThenInclude(p => p.Product)
                                                .ToList();

            mockRepository.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order.BuildMock());

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.GetById(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Test]
        public void Get_When_DatabaseDoesNotContainsOrderWithSpecificId_Throws_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Order, bool>>>()))
                                                    .Throws(new NotFoundException(It.IsAny<int>(), nameof(Order)));


            var sut = new OrderManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.GetById(It.IsAny<int>()));
        }

        [Test]
        public async Task CompleteOrder_DatabaseContainsOrderWithSpecificId_MarkIsCompletedPropertyAsTrue()
        {
            using var context = CreateContext();

            int id = 1;

            mockRepository.Setup(x => x.CompleteOrder(id))
                .ReturnsAsync(await context.Orders.FirstOrDefaultAsync(x => x.Id == id))
                .Callback(async () => await CompleteOrder(id, context));

            var sut = new OrderManagementService(mockRepository.Object);

            var result = await sut.CompleteOrderAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        private async Task CompleteOrder(int id, RepositoryContext context)
        {
            var order = await context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            order.IsCompleted = true;

            context.Orders.Attach(order);
            context.Entry(order).Property(x => x.IsCompleted).IsModified = true;
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task DeleteOrder_WhenDatabaseContainsOrderWithSpecificId_DeleteThisRecord()
        {
            using var context = CreateContext();

            int id = 1;

            var orders = await context.Orders.Where(x => x.Id == id).ToListAsync();

            var orderToDelete = orders.Where(x => x.Id == id).FirstOrDefault();

            mockRepository.Setup(x => x.FindByConditionAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                                        .ReturnsAsync(orders);
            mockRepository.Setup(x => x.Delete(orderToDelete))
                                        .Callback(() => context.Orders.Remove(orderToDelete));
            mockRepository.Setup(x => x.SaveChangesAsync())
                                        .Callback(async() => await context.SaveChangesAsync());

            var sut = new OrderManagementService(mockRepository.Object);

            await sut.Delete(id);

            mockRepository.Verify(x => x.Delete(orderToDelete), Times.Once);
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void DeleteOrder_WhenDatabaseDoesNotContainsOrderWithSpecificId_Throw_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByConditionAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                                                    .ThrowsAsync(new NotFoundException(It.IsAny<int>(), nameof(Order)));


            var sut = new OrderManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.Delete(It.IsAny<int>()));
        }
    }
}
