using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared
{
    [ExcludeFromCodeCoverage]
    public class ValidationErrorModel : ErrorModel
    {
        public string? Details { get; set; }
        public List<Field>? Fields { get; set; }

        public ValidationErrorModel(string code, string message, string details, List<Field> fields) : base(code, message)
        {
            Details = details;
            Fields = fields;
        }
        
        public ValidationErrorModel(string code, string message) : base(code, message)
        {
        }
    }
}
