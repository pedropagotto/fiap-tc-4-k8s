using System.Diagnostics.CodeAnalysis;
using System.Text;
using Common.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ContactApi.Extensions;
[ExcludeFromCodeCoverage]
public static class AuthenticationConfigExtension
{
    /// <summary>
    ///     Metodo para configuração de autenticação JWT
    /// </summary>
    public static void JwtConfig(this IServiceCollection services, TechChallengeFiapConfiguration config)
    {
        var secret = config.AuthSecret;
        var key = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}