using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ProductApi.Tests;

public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Root_ReturnsHelloDevOps()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Hello DevOps", content);
    }

    [Fact]
    public async Task Get_Products_ReturnsListOfProducts()
    {
        var response = await _client.GetAsync("/products");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
    }
}

// Model zgodny z Program.cs
public record Product(int Id, string Name, DateTime CreatedAt);
