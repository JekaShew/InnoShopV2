using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using ProductManagement.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( x =>
    {
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Prodcut Management Web API" });
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
    
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);
//builder.Services.AddMediatR(cfg=> cfg
//                            .RegisterServicesFromAssembly(typeof(TakeSubCategoryDTOListHandler)
//                            .Assembly));



var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ApplyMigrations();

app.UseInfrqastructurePolicy();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
