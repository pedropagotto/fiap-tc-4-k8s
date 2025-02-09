using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared
{
    [ExcludeFromCodeCoverage]
    public class DataValidationException : Exception
    {
        public string Code;
        public string Message;
        public string? Details;
        public List<Field>? Fields = new();

        private DataValidationException(string errorCode, string message, string details, List<Field> fields)
        {
            Code = errorCode;
            Message = message;
            Details = details;
            Fields.AddRange(fields);
        }

        public static void Throw(string errorCode, string message, string details, List<Field> fields)
        {
            throw new DataValidationException(errorCode, message, details, fields);
        }
    }
}
