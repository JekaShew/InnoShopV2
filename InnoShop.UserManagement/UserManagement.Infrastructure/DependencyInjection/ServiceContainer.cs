using InnoShop.CommonLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers;

namespace UserManagement.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(IServiceCollection services, IConfiguration configuration)
        {
            CommonServiceContainer.AddCommonServices<UserManagementDBContext>(services, configuration, configuration["Serolog:FileNAme"]);

            services.AddMediatR(cfg => cfg
                        .RegisterServicesFromAssembly(typeof(TakeRoleDTOListHandler)
                        .Assembly));

            return services;
        }

        public static IServiceCollection AddInfrastructureService(IServiceCollection services, IConfiguration configuration)
        {
            CommonServiceContainer.AddCommonServices<UserManagementDBContext>(services, configuration, configuration["Serilog:FileName"]);

            services.AddMediatR(cfg => cfg
                            .RegisterServicesFromAssembly(typeof(TakeRoleDTOListHandler)
                            .Assembly));

            return services;

        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            CommonServiceContainer.UseCommonPolicies(app);

            return app;
        }
    }
}
