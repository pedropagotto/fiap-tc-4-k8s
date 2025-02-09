using Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Authentication;

[ExcludeFromCodeCoverage]
public class RegisterModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Cpf { get; set; }

    public EProfile Profile { get; set; }

    public string Password { get; set; }
}