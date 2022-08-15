using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Data.Entitites;
using Exceptions.NotFound;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Repository.Contracts;
using Repository.Implementations;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace WebAPI.UnitTests.BLL
{
    [TestFixture]
    public class CategoryManagementServiceTests : TestWithSqlite
    {
        private Mock<IRepository> mockRepository;

        [SetUp]
        public async Task SetUp()
        {
            await CreateDatabaseAsync();
            mockRepository = new Mock<IRepository>();
        }

        [Test]
        public async Task GetAsync_When_DatabaseContainsSpecificRecords_Returns_ListOfCategoryDTO_Async()
        {
            using var context = CreateContext();

            var categories = context.Categories.AsQueryable().BuildMock();

            mockRepository.Setup(x => x.FindAll<Category>()).Returns(categories);

            var service = new CategoryManagementService(mockRepository.Object);

            var result = await service.GetAsync("");

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetByIdAsync_When_DatabaseContainsCategoryWithSpecificId_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            int id = 3;

            var categories = await context.FindAsync<Category>(id);

            mockRepository.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(categories);

            var sut = new CategoryManagementService(mockRepository.Object);

            var result = await sut.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Test]
        public void GetByIdAsync_When_DatabaseDoesNotContainsCategoryWithSpecificId_Throws_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(() => null);

            var sut = new CategoryManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.GetByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task SaveAsync_When_NewCategoryWasAddedToDatabase_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            var category = new Category()
            {
                Id = 5,
                Title = "Monitors",
                Description = "Description"
            };

            mockRepository.Setup(x => x.CreateAsync(It.IsAny<Category>())).Callback(() => context.Add(category));
            mockRepository.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockRepository.Object);

            var categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            var result = await sut.SaveAsync(categoryDTO);

            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Category>()), Times.Once());
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once());

            result.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAsync_When_CategoryWasUpdates_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            var category = new Category()
            {
                Id = 4,
                Title = "Monitors",
                Description = "Description"
            };

            mockRepository.Setup(x => x.Update(It.IsAny<Category>())).Callback(() => context.Update(category));
            mockRepository.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockRepository.Object);

            var categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            var result = await sut.UpdateAsync(categoryDTO);

            mockRepository.Verify(x => x.Update(It.IsAny<Category>()), Times.Once());
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once());

            result.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteAsync_When_DatabaseContainsCategoryWithSpecificId_Should_DeleteThisCategory()
        {
            using var context = CreateContext();

            var id = 3;

            var category = await context.Categories.FindAsync(id);

            mockRepository.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(category);
            mockRepository.Setup(x => x.Delete(It.IsAny<Category>())).Callback(() => context.Remove(category));
            mockRepository.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockRepository.Object);

            await sut.DeleteAsync(id);

            mockRepository.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once);
            mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void DeleteAsync_When_DatabaseDoesNotContainsCategoryWithSpecificId_Throws_NotFoundException()
        {
            mockRepository.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(() => null);

            var sut = new CategoryManagementService(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteAsync(It.IsAny<int>()));
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
    }
}
