using System.Diagnostics.CodeAnalysis;

namespace Application.ViewModels.Authentication;
[ExcludeFromCodeCoverage]
public class LoginRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}