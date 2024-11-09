namespace eTicaret.WebApi.Dtos;

public sealed record UpdateProductDto(
    Guid Id,
    string Name,
    decimal Price,
    int Stock);