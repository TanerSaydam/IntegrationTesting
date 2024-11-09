using eTicaret.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicaret.WebApi.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}