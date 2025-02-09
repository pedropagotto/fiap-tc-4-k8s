using API.Services.Token;
using Application.Services;
using Domain.Abstractions;
using Infra.Repository;
using System.Diagnostics.CodeAnalysis;

namespace API.Config
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IValidateJwtToken, ValidateJwtToken>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ConctactRepository>();

            return services;
        }
    }
}
