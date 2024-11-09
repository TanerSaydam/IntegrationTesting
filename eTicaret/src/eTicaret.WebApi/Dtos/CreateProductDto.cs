namespace eTicaret.WebApi.Dtos;

public sealed record CreateProductDto(
    string Name,
    decimal Price,
    int Stock);