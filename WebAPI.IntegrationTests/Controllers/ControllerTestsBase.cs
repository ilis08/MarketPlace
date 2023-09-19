using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace WebAPI.IntegrationTests.Controllers;

[TestFixture]
public abstract class ControllerTestsBase
{
    private readonly WebApplicationFactory<Program> applicationFactory;
    protected readonly HttpClient client;

    public ControllerTestsBase()
    {
        applicationFactory = new();
        client = applicationFactory.CreateDefaultClient(new Uri("http://localhost/api/"));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        client.Dispose();
    }
}
