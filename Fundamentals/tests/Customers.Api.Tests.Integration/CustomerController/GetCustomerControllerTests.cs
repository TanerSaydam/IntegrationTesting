using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections;
using System.Net;
using System.Net.Http.Json;

namespace Customers.Api.Tests.Integration.CustomerController;
[Collection("CustomerApi Collection")]
public sealed class GetCustomerControllerTests //: IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;
    public GetCustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");


        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem!.Title.Should().Be("Not Found");
        problem!.Status.Should().Be(404);
        //response.Headers.Location.ToString().Should().Be("");
        await Task.Delay(3000);
    }



    [Theory]
    //[InlineData("e4f6a591-a0a9-41c0-a4ce-1a395f7a34bf")]
    //[InlineData("2f228282-69fd-469a-a095-07c48dd6ef9f")]
    //[InlineData("4b17ca3e-0484-4e51-8f36-dd4c4bc60dd3")]
    //[MemberData(nameof(Data))]
    [ClassData(typeof(ClassData))]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist_Theory(
        string guidAsText)
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");


        // Assert
        //Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        using (new AssertionScope())
        {
            var text = await response.Content.ReadAsStringAsync();
            text.Should().Contain("404");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }


    public static IEnumerable<object[]> Data { get; } = new[]
    {
        new[]{ "e4f6a591-a0a9-41c0-a4ce-1a395f7a34bf"},
        new[]{ "2f228282-69fd-469a-a095-07c48dd6ef9f"},
        new[]{ "4b17ca3e-0484-4e51-8f36-dd4c4bc60dd3"},
    };


    public class ClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "e4f6a591-a0a9-41c0-a4ce-1a395f7a34bf" };
            yield return new object[] { "2f228282-69fd-469a-a095-07c48dd6ef9f" };
            yield return new object[] { "4b17ca3e-0484-4e51-8f36-dd4c4bc60dd3" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
