namespace Customers.Api.Contracts.Requests;

public class CustomerRequest
{
    public string GitHubUsername { get; set; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; set; } = default!;

    public DateTime DateOfBirth { get; init; } = default!;
}
