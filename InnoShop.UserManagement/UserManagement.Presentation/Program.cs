using UserManagement.Infrastructure.DependencyInjection;
using UserManagement.Application.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UserManagement Web API",
        Description = "Usermanagement Web API",
    });
    c.CustomSchemaIds(type => type.FullName);
});


builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);


var app = builder.Build();

app.UseInfrastructurePolicy();

app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement Web API");

    // Read from file including static Files
    c.SwaggerEndpoint("/swagger-original.json", "UserManagement Web API");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
