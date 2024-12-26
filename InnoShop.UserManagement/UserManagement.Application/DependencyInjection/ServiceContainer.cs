using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Serilog;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;

namespace UserManagement.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorizationServices, AuthorizationServices>();
            services.AddHttpClient<IUserServices, UserServices>(options =>
            {
                options.BaseAddress = new Uri(configuration["ApiGateway:BaseAdress"]!);
                options.Timeout = TimeSpan.FromSeconds(2);
            });

            var retryStartegy = new RetryStrategyOptions()
            {
                ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
                BackoffType = DelayBackoffType.Constant,
                UseJitter = true,
                MaxRetryAttempts = 4,
                Delay = TimeSpan.FromMilliseconds(500),
                OnRetry = args =>
                {
                    string message = $"OnRetry, Attempt: {args.AttemptNumber} Outcome {args.Outcome}";
                    Log.Warning(message);
                    return ValueTask.CompletedTask;
                }
            };

            services.AddResiliencePipeline("retry-pipeline", builder =>
            {
                builder.AddRetry(retryStartegy);
            });

            return services;
        }
    }
}
