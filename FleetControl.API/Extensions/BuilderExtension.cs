using Microsoft.Extensions.Configuration;

namespace FleetControl.API.Extensions
{
    public static class BuilderExtension
    {
        public static IServiceCollection AddBuilderExtensionServices(this IServiceCollection services)
        {
            services
                .AddCors();

            return services;
        }
        private static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(
                options => options
                    .AddPolicy(Environment.GetEnvironmentVariable("CORS_POLICY_NAME"),
                policy => policy
                .WithOrigins([Environment.GetEnvironmentVariable("BACKEND_URL_HTTP"), Environment.GetEnvironmentVariable("BACKEND_URL_HTTPS"), Environment.GetEnvironmentVariable("FRONTEND_URL")])
                .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials())
            );
            return services;
        }
    }
}
