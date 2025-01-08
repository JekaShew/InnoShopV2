using FluentValidation;
using InnoShop.CommonLibrary.DependencyInjection;
using InnoShop.CommonLibrary.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Validators.UserStatusValidators;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers;

namespace UserManagement.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // last variable is for DB Connection String Key that in configuration
            CommonServiceContainer.AddCommonServices<UserManagementDBContext>(services, configuration, configuration["UMSerolog:FileName"]!,"Home");

            services.AddMediatR(cfg => cfg
                        .RegisterServicesFromAssembly(typeof(TakeRoleDTOListHandler).Assembly));

            services.AddValidatorsFromAssembly(typeof(AddUserStatusCommandValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            CommonServiceContainer.UseCommonPolicies(app);

            return app;
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using UserManagementDBContext pmDBContext =
                              scope.ServiceProvider.GetRequiredService<UserManagementDBContext>();

            //pmDBContext.Database.Migrate();
        }
    }
}
