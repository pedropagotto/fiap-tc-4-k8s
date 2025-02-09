using API.Middlewares.Exceptions;
using Domain.Shared;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string jsonResp;
                switch (ex)
                {
                    case DataValidationException validationException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        ValidationErrorModel validationError = new(validationException.Code, validationException.Message, validationException?.Details, validationException?.Fields);
                        jsonResp = JsonSerializer.Serialize(validationError, new JsonSerializerOptions
                        {
                            IncludeFields = true
                        });
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResp);
                        break;
                    case NotFoundException notFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        ErrorModel notFoundError = new(notFoundException.Code, notFoundException.Message);
                        jsonResp = JsonSerializer.Serialize(notFoundError);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResp);
                        break;
                    case ServerException serverException:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        ErrorModel serverError = new(serverException.Code, serverException.Message);
                        jsonResp = JsonSerializer.Serialize(serverError);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResp);
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        ErrorModel generalError = new("500", "Ocorreu um erro inesperado.");
                        jsonResp = JsonSerializer.Serialize(generalError);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResp);
                        break;
                }

            }
        }
    }
}
