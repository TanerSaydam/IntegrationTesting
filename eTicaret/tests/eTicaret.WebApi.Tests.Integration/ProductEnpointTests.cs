using eTicaret.WebApi.Dtos;
using eTicaret.WebApi.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace eTicaret.WebApi.Tests.Integration;
public class ProductEnpointTests : IAsyncLifetime, IClassFixture<eTicaretApiFactory>
{
    private readonly HttpClient _httpClient;
    private List<Guid> _ids = new();
    public ProductEnpointTests(eTicaretApiFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ReturnsCreated_When_Product_Is_Created()
    {
        // Arrange        
        CreateProductDto request = new("Ürün", 1, 10);

        // Act
        var response = await _httpClient.PostAsJsonAsync("create", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<Product>();
        _ids.Add(content!.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsProducts_When_Have_Products()
    {
        // Act
        var response = await _httpClient.GetAsync("getall");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    public async Task DisposeAsync()
    {
        foreach (var _id in _ids)
        {
            await _httpClient.DeleteAsync($"https://localhost:7047/deletebyid?id={_id}");
        }
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}