using AutoFixture.Xunit2;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Customers.Api.Tests.Integration.CustomerController;

[Collection("CustomerApi Collection")]
public sealed class CreateCuıstomerControllerTests : IAsyncLifetime//, IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;
    private readonly Faker<CustomerRequest> _cutomerGenerator =
        new Faker<CustomerRequest>()
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.GitHubUsername, faker => "tanersaydam")
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date)
        ;

    private readonly List<Guid> _createdIds = new();
    public CreateCuıstomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Theory, AutoData]
    public async Task Create_ReturnsCreated_WhenCustomerIsCreated(string fullName)
    {
        // Arrange
        var customer = _cutomerGenerator.Generate();

        // Act
        var response = await _httpClient.PostAsJsonAsync("customers", customer);

        // Assert
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        customerResponse.Should().BeEquivalentTo(customer);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        _createdIds.Add(customerResponse!.Id);
        await Task.Delay(3000);
    }

    public Task InitializeAsync() => Task.CompletedTask;


    public async Task DisposeAsync()
    {
        foreach (var id in _createdIds)
        {
            await _httpClient.DeleteAsync($"customers/{id}");
        }
    }
}
