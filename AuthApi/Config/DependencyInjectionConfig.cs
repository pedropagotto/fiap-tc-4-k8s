using System.Diagnostics.CodeAnalysis;
using API.Services.Token;
using Application.Services;
using Application.Services.AuthenticationServices;
using Application.Services.UserServices;
using Domain.Abstractions;
using Infra.Repository;

namespace API.Config
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IGenerateJwtToken, GenerateJwtToken>();
            services.AddScoped<IValidateJwtToken, ValidateJwtToken>();
            services.AddScoped<IValidateUser, ValidateUser>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
