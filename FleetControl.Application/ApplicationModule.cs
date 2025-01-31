using FleetControl.Application.Commands.Projects.InsertProject;
using Microsoft.Extensions.DependencyInjection;

namespace FleetControl.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices()
                    .AddHandlers();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>();
            });

            return services;
        }
    }
}
