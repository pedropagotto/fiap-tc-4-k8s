using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Domain.Entities;
[ExcludeFromCodeCoverage]
public class Authentication: EntityBase
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public bool EmailValidated { get; set; }
    public bool ChangePassword { get; set; }
    
    public EProfile Profile { get; set; }
}