using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.infra.Persistence;

namespace TMS.infra;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services.AddPersistence(config);
    }

    public static IApplicationBuilder UserInfrastructure(this  IApplicationBuilder app, IConfiguration config)
    {
        return app;
    }
}
