using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using Data.Entitites;

namespace WebAPI.IntegrationTests.Controllers;

[TestFixture]
public class CategoryControllerTests : ControllerTestsBase
{
    public CategoryControllerTests() : base()
    {

    }



    [Test]
    public async Task Get_When_DatabaseContainsCategories_Returns_ExpectedJson()
    {
        var categories = await client.GetFromJsonAsync<List<Category>>("category/get");

        categories.Should().NotBeNull();
        categories.Should().HaveCountGreaterThan(0);
    }

    [Test]
    public async Task GetById_WhenDatabaseContainsCategoryWithSpecificId_Returns_Category()
    {
        var category = await client.GetFromJsonAsync<Category>("category/getById/3");

        category.Should().NotBeNull();
        category.Id.Should().Be(3);
    }

}
