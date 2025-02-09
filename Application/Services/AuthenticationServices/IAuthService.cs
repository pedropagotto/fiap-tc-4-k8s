using Application.Models.Authentication;

namespace Application.Services.AuthenticationServices;

public interface IAuthService
{
    public RegisterModel Register(RegisterModel reg);
}