namespace API.Services.Token;

public interface IValidateJwtToken
{
    bool ValidateToken(HttpRequest request, params string[] validRoles);
    int GetUserId(HttpRequest request);
    string GetUserEmail(HttpRequest request);
}