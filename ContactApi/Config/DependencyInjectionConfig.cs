using API.Services.Token;
using Application.Services;
using Application.Services.AuthenticationServices;
using Application.Services.UserServices;
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
            return services;
        }
    }
}
