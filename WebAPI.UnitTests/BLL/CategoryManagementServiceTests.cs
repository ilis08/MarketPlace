using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Data.Entitites;
using Exceptions.NotFound;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Repository.Implementations;
using Repository.Implementations.BaseRepo;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace WebAPI.UnitTests.BLL
{
    [TestFixture]
    public class CategoryManagementServiceTests : TestWithSqlite
    {
        private Mock<IRepository> mockUnitOfWork;

        [SetUp]
        public async Task SetUp()
        {
            await CreateDatabaseAsync();
            mockUnitOfWork = new Mock<IRepository>();
        }

        [Test]
        public async Task Get_When_DatabaseContainsSpecificRecords_Returns_ListOfCategoryDTO_Async()
        {
            using var context = CreateContext();

            var categories = context.Categories.AsQueryable().BuildMock();

            mockUnitOfWork.Setup(x => x.FindAll<Category>()).Returns(categories);

            var service = new CategoryManagementService(mockUnitOfWork.Object);

            var result = await service.Get("");

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetById_When_DatabaseContainsCategoryWithSpecificId_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            int id = 3;

            var categories = await context.FindAsync<Category>(id);

            mockUnitOfWork.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(categories);

            var sut = new CategoryManagementService(mockUnitOfWork.Object);

            var result = await sut.GetById(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Test]
        public void GetById_When_DatabaseDoesNotContainsCategoryWithSpecificId_Throws_NotFoundException()
        {
            mockUnitOfWork.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ThrowsAsync(new NotFoundException(It.IsAny<int>(), nameof(Category)));

            var sut = new CategoryManagementService(mockUnitOfWork.Object);

            Assert.ThrowsAsync<NotFoundException>(() => sut.GetById(It.IsAny<int>()));
        }

        [Test]
        public async Task Save_When_NewCategoryWasAddedToDatabase_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            var category = new Category()
            {
                Id = 5,
                Title = "Monitors",
                Description = "Description"
            };

            mockUnitOfWork.Setup(x => x.CreateAsync(It.IsAny<Category>())).Callback(() => context.Add(category));
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockUnitOfWork.Object);

            var categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            var result = await sut.Save(categoryDTO);

            mockUnitOfWork.Verify(x => x.CreateAsync(It.IsAny<Category>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once());

            result.Should().NotBeNull();
        }

        [Test]
        public async Task Update_When_CategoryWasUpdates_Returns_CategoryDTO_Async()
        {
            using var context = CreateContext();

            var category = new Category()
            {
                Id = 4,
                Title = "Monitors",
                Description = "Description"
            };

            mockUnitOfWork.Setup(x => x.Update(It.IsAny<Category>())).Callback(() => context.Update(category));
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockUnitOfWork.Object);

            var categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            var result = await sut.Update(categoryDTO);

            mockUnitOfWork.Verify(x => x.Update(It.IsAny<Category>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once());

            result.Should().NotBeNull();
        }

        [Test]
        public async Task Delete_When_DatabaseContainsCategoryWithSpecificId_Should_DeleteThisCategory()
        {
            using var context = CreateContext();

            var id = 3;

            var category = await context.Categories.FindAsync(id);

            mockUnitOfWork.Setup(x => x.FindByIdAsync<Category>(It.IsAny<int>())).ReturnsAsync(category);
            mockUnitOfWork.Setup(x => x.Delete(It.IsAny<Category>())).Callback(() => context.Remove(category));
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Callback(() => context.SaveChangesAsync());

            var sut = new CategoryManagementService(mockUnitOfWork.Object);

            await sut.Delete(id);

            mockUnitOfWork.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }



        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
    }
}
