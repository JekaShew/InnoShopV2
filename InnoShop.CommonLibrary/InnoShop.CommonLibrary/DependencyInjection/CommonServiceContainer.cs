//using Hellang.Middleware.ProblemDetails;
using InnoShop.CommonLibrary.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.CommonLibrary.DependencyInjection
{
    public static class CommonServiceContainer
    {
        public static IServiceCollection AddCommonServices<TContext>
            (this IServiceCollection services, IConfiguration configuration, string serilogFile, string dbConnectionStringKey) where TContext : DbContext
        {
            // DB
            services.AddDbContext<TContext>(option =>
                option.UseSqlServer(
                    configuration.GetConnectionString(dbConnectionStringKey), sqlserverOption => sqlserverOption.EnableRetryOnFailure()));

            //Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File($"{serilogFile}.log")
                .CreateBootstrapLogger();

            services.AddSerilog((services, lc) => lc
                .ReadFrom.Configuration(configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
                .WriteTo.File($"{serilogFile}.log"));

            // Global Exception Handler
            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            // JWT Authentication Scheme
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, configuration);

            return services;
        }
        public static IApplicationBuilder UseCommonPolicies(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(opt => { });
            // Global Response Handler
            app.UseMiddleware<GlobalResponseHandler>();

            // Only for using ApiGateway 
            //app.UseMiddleware<ListenToOnlyApiGatewayRule>();

            return app;
        }
    }
}
