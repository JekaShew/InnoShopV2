using ProductManagement.Application.Queries.SubCategoryQueries;
using ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg=> cfg
                            .RegisterServicesFromAssembly(typeof(TakeSubCategoryDTOListHandler)
                            .Assembly));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
