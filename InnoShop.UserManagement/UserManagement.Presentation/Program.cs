using UserManagement.Infrastructure.DependencyInjection;
using UserManagement.Application.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management Web API", Version = "v1" });
    x.TagActionsBy(api =>
    {
        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null)
            throw new InvalidOperationException("Unable to determine tag for endpoint.");

        if (api.GroupName != null)
            return new[] { $"{api.GroupName} {controllerActionDescriptor.ControllerName}" };

        return new[] { controllerActionDescriptor.ControllerName };
    });
    x.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ApplyMigrations();

app.UseInfrastructurePolicy();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
