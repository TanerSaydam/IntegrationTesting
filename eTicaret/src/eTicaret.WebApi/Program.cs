using eTicaret.WebApi.Context;
using eTicaret.WebApi.Dtos;
using eTicaret.WebApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

if (!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("SqlServer"));
    });
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getall", (ApplicationDbContext context) => context.Products.ToList());

app.MapGet("/getbyid", (Guid id, ApplicationDbContext context) => context.Products.FirstOrDefault(p => p.Id == id));

app.MapPost("/create", (CreateProductDto request, ApplicationDbContext context) =>
{
    Product product = new()
    {
        Name = request.Name,
        Price = request.Price,
        Stock = request.Stock
    };

    context.Products.Add(product);
    context.SaveChanges();

    return Results.Ok(product);
});

app.MapPut("/update", (UpdateProductDto request, ApplicationDbContext context) =>
{
    var product = context.Products.FirstOrDefault(p => p.Id == request.Id);
    if (product is null)
    {
        return Results.NotFound();
    }

    product.Name = request.Name;
    product.Price = request.Price;
    product.Stock = request.Stock;

    context.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("/deletebyid", (Guid id, ApplicationDbContext context) =>
{
    var product = context.Products.FirstOrDefault(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound();
    }

    context.Remove(product);
    context.SaveChanges();

    return Results.NoContent();
});

using (var scoped = app.Services.CreateScope())
{
    var db = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
