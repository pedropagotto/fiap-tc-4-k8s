using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;
[ExcludeFromCodeCoverage]
public class User: EntityBase
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email {get; set;}
    
    public string Cpf { get; set; }
    
    public int AuthenticationId { get; set; }
    
    public virtual Authentication Authentication { get; set; }
    
    [NotMapped]
    public string? FullName => FirstName + ' ' + LastName;
}