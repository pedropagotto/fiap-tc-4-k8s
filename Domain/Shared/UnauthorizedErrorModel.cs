using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared
{
    [ExcludeFromCodeCoverage]
    public class UnauthorizedErrorModel
    {
        public string Error { get; set; }

        public UnauthorizedErrorModel()
        {
            Error = "response status is 401";
        }
    }
}
