
using Microsoft.EntityFrameworkCore;
using TMS.infra;
using TMS.infra.Persistence.Context;

var SwaggerVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register services in Infrastructure layer including the persistence (database)
builder.Services.AddInfrastructure(config);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"AltaGas TMS {SwaggerVersion}");
    c.DefaultModelsExpandDepth(-1);
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UserInfrastructure(config);

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();


