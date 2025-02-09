using Application.Models.Authentication;
using Domain.Entities;

namespace Application.Services.UserServices;

public interface IUserService
{
    public User Create(RegisterModel model);
}