using Application.Models.Authentication;
using Application.Services.UserServices;
using Infra;
using System.Diagnostics.CodeAnalysis;

namespace Application.Services.AuthenticationServices;
[ExcludeFromCodeCoverage]
public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;

    public AuthService(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public RegisterModel Register(RegisterModel reg)
    {
        _userService.Create(reg);

        return reg;
    }
}