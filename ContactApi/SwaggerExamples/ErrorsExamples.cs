using System.Diagnostics.CodeAnalysis;
using Domain.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class NotFoundResponseExample : IExamplesProvider<ErrorModel>
    {
        public ErrorModel GetExamples()
        {
            return new ErrorModel("001", "O contato não encontrado.");
        }
    }
    
    [ExcludeFromCodeCoverage]
    public class BadRequestResponseExample : IExamplesProvider<ValidationErrorModel>
    {
        public ValidationErrorModel GetExamples()
        {
            var fields = new List<Field>()
            {
                new Field
                {
                    Message = "Email informado inválido",
                    Name = "Email",
                    Value = "sanders.com"
                }
            };

            return new ValidationErrorModel("002", "Dados inválidos", "Tipo inválido.", fields);
        }
    }

    [ExcludeFromCodeCoverage]
    public class InternalServerErrorExample : IExamplesProvider<ErrorModel>
    {
        public ErrorModel GetExamples()
        {
            return new ErrorModel("005", "Ocorreu um erro inesperado.");
        }
    }
}
