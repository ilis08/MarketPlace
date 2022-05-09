using Data.Entitites;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebAPI.IntegrationTests.Controllers
{
    public class ProductControllerTests : ControllerTestsBase
    {
        public ProductControllerTests() : base()
        {

        }

        [Test]
        public async Task Get_WhenDatabaseContainsProducts_Returns_ListOfProduct()
        {
            var products = await client.GetFromJsonAsync<List<Product>>("product/get");

            products.Should().NotBeNull();
            products.Should().HaveCountGreaterThan(0);
        }
    }
}
