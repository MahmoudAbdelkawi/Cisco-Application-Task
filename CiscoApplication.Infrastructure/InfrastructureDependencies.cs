using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Infrastructure.Contexts;
using CiscoApplication.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CiscoApplication.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Establish Connection With Database
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Adding Services
            services.AddScoped<IItemRepository, ItemRepository>();;
            return services;
        }
    }
}