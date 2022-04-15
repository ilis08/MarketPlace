using ApplicationService.Implementations;
using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Repository.Implementations;
using Repository.Implementations.CategoryRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Tests.Helpers;

namespace WebAPI.Tests.BLL
{
    [TestFixture]
    public class CategoryManagementServiceTests
    {
        private ServiceProvider serviceProvider;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private RepositoryContext context;

        [SetUp]
        public void SetUp()
        {
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection.AddScoped<RepositoryContext>().BuildServiceProvider();

            context = serviceProvider.GetRequiredService<RepositoryContext>();

            mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task Get_When_DatabaseContainsSpecificRecords_Returns_ListOfCategoryDTO_Async()
        {
            var categories = await JsonDatabaseHelper<Category>.GetItems(@"C:\projects\DistributedStore-ASP.NET-Core\WebAPI.Tests\DatabaseMoq\categories.json");

            mockUnitOfWork.Setup(x => x.CategoryRepository).Returns(new CategoryRepository(context));

            var service = new CategoryManagementService(mockUnitOfWork.Object);

            var result = await service.Get("");

            CollectionAssert.AreEqual(categories, result);
        }
    }
}
