using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.application.Interfaces;
using TMS.infra.Persistence;
using TMS.infra.Services;

namespace TMS.infra;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services.AddPersistence(config)
            .AddScoped<IEquipmentEventService, EquipmentEventService>()
            .AddScoped<ITripService,TripService>();
    }

    public static IApplicationBuilder UserInfrastructure(this  IApplicationBuilder app, IConfiguration config)
    {
        return app;
    }
}
