using Application.ViewModels.Authentication;

namespace API.Services.Token;

public interface IGenerateJwtToken
{
    Task<AuthenticationResponseModel?> GenerateToken(LoginRequestModel user);
}