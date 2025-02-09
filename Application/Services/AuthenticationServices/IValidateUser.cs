using Application.ViewModels.Authentication;

namespace Application.Services.AuthenticationServices;

public interface IValidateUser
{
    Task<Domain.Entities.Authentication?> ValidateUserPwd(LoginRequestModel user);
}