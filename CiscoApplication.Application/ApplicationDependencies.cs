using CiscoApplication.Application.Behaviours;
using CiscoApplication.Application.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CiscoApplication.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // Add File service
            services.AddScoped<IFileService, FileHelper>();
            // Add MediatR Service
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            // Add Fluent Validation Services 
            services.AddValidatorsFromAssembly(assembly: Assembly.GetExecutingAssembly());
            // Add Fluent Validation Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
