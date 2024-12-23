using ProductManagement.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureService(builder.Configuration);

//builder.Services.AddMediatR(cfg=> cfg
//                            .RegisterServicesFromAssembly(typeof(TakeSubCategoryDTOListHandler)
//                            .Assembly));

var app = builder.Build();

app.UseInfrqastructurePolicy();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
