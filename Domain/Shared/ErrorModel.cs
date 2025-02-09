using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared
{
    [ExcludeFromCodeCoverage]
    public class ErrorModel
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ErrorModel(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
