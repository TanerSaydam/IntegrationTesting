using eTicaret.WebApi.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace eTicaret.WebApi.Tests.Integration;
public sealed class eTicaretApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder()
        .Build();
    public eTicaretApiFactory()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
    }

    public async Task InitializeAsync()
    {
        await postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                string connectionString = postgreSqlContainer.GetConnectionString();
                builder.UseNpgsql(connectionString);
            });
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await postgreSqlContainer.DisposeAsync();
    }
}
