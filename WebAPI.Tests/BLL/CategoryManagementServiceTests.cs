using ApplicationService.Implementations;
using Data.Entitites;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Repository.Implementations;
using Repository.Implementations.BaseRepo;
using System.Linq;
using System.Threading.Tasks;



namespace WebAPI.Tests.BLL
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

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
    }
}
