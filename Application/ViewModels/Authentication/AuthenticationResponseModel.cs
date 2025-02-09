using System.Diagnostics.CodeAnalysis;

namespace Application.ViewModels.Authentication;
[ExcludeFromCodeCoverage]
public class AuthenticationResponseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Authenticated { get; set; }
    public string Created { get; set; } = DateTime.Now.ToString();
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
    public bool HasChangePassword { get; set; }
}