using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.AuthenticationServices;
using Application.ViewModels.Authentication;
using Common.Config;
using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Token;
[ExcludeFromCodeCoverage]
public class GenerateJwtToken: IGenerateJwtToken
{
    private readonly ITechChallengeFiapConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly IValidateUser _validateUser;

    public GenerateJwtToken(ITechChallengeFiapConfiguration configuration, AppDbContext context, IValidateUser validateUser)
    {
        _configuration = configuration;
        _context = context;
        _validateUser = validateUser;
    }

    public async Task<AuthenticationResponseModel?> GenerateToken(LoginRequestModel user)
    {
        var userAuth = await _validateUser.ValidateUserPwd(user);
        if (userAuth is null)
            return null;

        var userEntity = _context.Users.AsNoTracking().FirstOrDefault(x => x.Email == userAuth.Email);
        var userFullName = userEntity.FullName;
        var userId = userEntity.Id.ToString();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.AuthSecret);
        var expirationDate = DateTime.UtcNow.AddDays(1);
        var handler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, userAuth.Profile.ToString()),
                new("fullName", userFullName),
                new("userId", userId)
            }),
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = handler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        if (userAuth.ChangePassword)
            token = null;

        var auth = new AuthenticationResponseModel()
        {
            Email = user.Email,
            Password = "",
            Authenticated = token != null,
            Created = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            HasChangePassword = userAuth.ChangePassword
        };

        return auth;
    }
}