using System.Diagnostics.CodeAnalysis;
using Common.Config;
using JWT.Algorithms;
using JWT.Builder;

namespace API.Services.Token;
[ExcludeFromCodeCoverage]
public class ValidateJwtToken: IValidateJwtToken
{
    private readonly ITechChallengeFiapConfiguration _configuration;

    public ValidateJwtToken(ITechChallengeFiapConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool ValidateToken(HttpRequest request, params string[] validRoles)
    {
        try
        {
            var token = GetToken(request);
            var claims = GetClaims(token);
            if (!claims.ContainsKey("fullName") || !claims.ContainsKey("role"))
                return false;

            var roles = claims.Where(x => x.Key == "role").Select(x=> x.Value);
            var isMember = roles.Intersect(validRoles).Any();
            return isMember;

        } catch (Exception exception) {
            return false;
        }
    }

    public int GetUserId(HttpRequest request)
    {
        throw new NotImplementedException();
    }

    public string GetUserEmail(HttpRequest request)
    {
        throw new NotImplementedException();
    }
    
    #region Private_Methods

    private string GetToken(HttpRequest request)
    {
        if (!request.Headers.ContainsKey("Authorization")) 
            throw new Exception("Authorization header not found");
        
        string authorizationHeader = request.Headers["Authorization"];
        
        if (string.IsNullOrEmpty(authorizationHeader))
            throw new Exception("Authorization token not found");
        
        return authorizationHeader.StartsWith("Bearer") ? authorizationHeader.Substring(7) : string.Empty;
    }
    
    private IDictionary<string,object> GetClaims(string token)
    {
        IDictionary<string,object>? claims;
        
        claims = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(_configuration.AuthSecret)
            .MustVerifySignature().Decode <IDictionary <string, object>>(token);
        
        return claims;
    }

    #endregion
}