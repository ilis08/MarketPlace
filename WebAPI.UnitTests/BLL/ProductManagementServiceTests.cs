using ApplicationService.Implementations;
using Data.Entitites;
using Exceptions.NotFound;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.BLL
{
    public class ProductManagementServiceTests : TestWithSqlite
    {
        private Mock<IProductRepository> mockRepository;

        [SetUp]
        public async Task SetUp()
        {
            await CreateDatabaseAsync();
            mockRepository = new Mock<IProductRepository>();
        }

        [Test]
        public async Task GetAsync_When_DatabaseContainsProducts_Returns_IEnumerableOfProductDTO_Async()
        {
            using var context = CreateContext();

            var products = context.Products.AsQueryable().BuildMock();

            mockRepository.Setup(x => x.FindAll<Product>()).Returns(products);

            var service = new ProductManagementService(mockRepository.Object);

            var result = await service.GetAsync("");

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetAsync_When_DatabaseContainsProductsWithSpecificName_Returns_IEnumerableOfProductDTO_Async()
        {
            using var context = CreateContext();

            Expression<Func<Product, bool>> findProductByNameExpression = x => x.ProductName.Contains("ASUS");

            var products = await context.Products.Where(findProductByNameExpression).ToListAsync();

            mockRepository.Setup(x => x.FindByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(products);

            var service = new ProductManagementService(mockRepository.Object);

            var result = await service.GetAsync("ASUS");

            result.Should().Contain(x => x.ProductName.Contains("ASUS"));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetByIdAsync_When_DatabaseContainsProductWithSpecificId_Returns_ProductDTO_Async(int id)
        {
            using var context = CreateContext();

            Expression<Func<Product, bool>> findProductByIdExpression = x => x.Id == id;

            var product = await context.Products.Where(findProductByIdExpression).FirstOrDefaultAsync();

            mockRepository.Setup(x => x.FindByIdAsync<Product>(It.IsAny<int>())).ReturnsAsync(product);

            var service = new ProductManagementService(mockRepository.Object);

            var result = await service.GetByIdAsync(id);

            result.Id.Should().Be(id);
        }

        [Test]
        public void GetByIdAsync_When_DatabaseDoesNotContainsProductWithSpecificId_Throws_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByIdAsync<Product>(It.IsAny<int>())).ThrowsAsync(new NotFoundException(It.IsAny<int>(), nameof(Product)));

            var sut = new ProductManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.GetByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task DeleteAsync_When_DatabaseContainsProductWithSpecificId_Should_RemoveThisProductFromDatabase_Async()
        {
            using var context = CreateContext();

            var id = 1;

            var product = await context.Products.FindAsync(id);

            mockRepository.Setup(x => x.FindByIdAsync<Product>(It.IsAny<int>())).ReturnsAsync(product);
            mockRepository.Setup(x => x.Delete(It.IsAny<Product>())).Callback(() => context.Remove(product));
            mockRepository.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new ProductManagementService(mockRepository.Object);

            await sut.DeleteAsync(id);

            mockRepository.Verify(x => x.Delete(It.IsAny<Product>()), Times.Once);
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
