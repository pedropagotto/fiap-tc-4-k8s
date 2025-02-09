using Application.ViewModels.Authentication;
using Common.Extensions;
using Infra;
using System.Diagnostics.CodeAnalysis;

namespace Application.Services.AuthenticationServices;
[ExcludeFromCodeCoverage]
public class ValidateUser : IValidateUser
{
    private readonly AppDbContext _context;

    public ValidateUser(AppDbContext context)
    {
        _context = context;
    }

    public Task<Domain.Entities.Authentication?> ValidateUserPwd(LoginRequestModel user)
    {
        var pwdEncrypted = user.Password.EncryptPassword();

        var auth = _context.Authentications
            .FirstOrDefault(x => (x.Email == user.Email && x.Password == pwdEncrypted) || (x.Email == user.Email && x.ChangePassword == true));

        return Task.FromResult(auth);
    }
}