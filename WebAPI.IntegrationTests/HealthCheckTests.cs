using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.IntegrationTests;

[TestFixture]
public class HealthCheckTests
{
    private readonly WebApplicationFactory<Program> applicationFactory;
    private readonly HttpClient httpClient;

    public HealthCheckTests()
    {
        applicationFactory = new();
        httpClient = applicationFactory.CreateDefaultClient();
    }

    [Test]
    public async Task HealthCheck_ReturnsOk()
    {
        var response = await httpClient.GetAsync("/healthcheck");

        response.EnsureSuccessStatusCode();
    }
}
