namespace Customers.Api.Tests.Integration.CustomerController;
public sealed class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }

    [Fact]
    public async Task Test()
    {
        await Task.Delay(5000);
    }
}
