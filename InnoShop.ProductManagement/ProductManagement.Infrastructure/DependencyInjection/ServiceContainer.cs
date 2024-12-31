using InnoShop.CommonLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // last variable is for DB Connection String Key that in configuraation
            CommonServiceContainer.AddCommonServices<ProductManagementDBContext>(services, configuration, configuration["PMSerilog:FileName"]!, "Home" );

            services.AddMediatR(cfg => cfg
                            .RegisterServicesFromAssembly(typeof(TakeSubCategoryDTOListHandler)
                            .Assembly));

            return services;
        }

        public static IApplicationBuilder UseInfrqastructurePolicy(this IApplicationBuilder app)
        {
            CommonServiceContainer.UseCommonPolicies(app);

            return app;
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ProductManagementDBContext pmDBContext = 
                              scope.ServiceProvider.GetRequiredService<ProductManagementDBContext>();

            //pmDBContext.Database.Migrate();
        }
    }
}
