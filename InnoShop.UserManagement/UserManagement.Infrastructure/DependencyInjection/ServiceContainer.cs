using InnoShop.CommonLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
