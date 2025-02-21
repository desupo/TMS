using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Persistence;

//TODO: Register Database here.
internal static class Startup
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<dbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("SqliteConnection") ?? "Data Source=TMSTest.db";
            options.UseSqlite(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information); 

            // TODO: For best practice, consider retrieving the connection string from Azure Key Vault
            // var keyVaultConnectionString = serviceProvider.GetService<IConfiguration>()["KeyVaultConnectionString"];
            // options.UseSqlServer(keyVaultConnectionString);
        });

        return services;
    }



    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<dbContext>
    {
        public dbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<dbContext>();

            // For best practice, consider retrieving the connection string from Azure Key Vault
            // var connectionString = Configuration["KeyVaultConnectionString"];
            // optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.UseSqlite("Data Source=TMSTest.db");

            return new dbContext(optionsBuilder.Options);
        }
    }

}

