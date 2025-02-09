using Application.Models.Authentication;
using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Infra;

namespace Application.Services.UserServices;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public User Create(RegisterModel model)
    {
        var user = _mapper.Map<User>(model);

        //Preenchendo informacoes de autenticacao
        var auth = new Authentication()
        {
            Email = model.Email,
            Password = model.Password.EncryptPassword(),
            Profile = model.Profile,
            ChangePassword = false,
            EmailValidated = false,
        };

        user.Authentication = auth;

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }
}