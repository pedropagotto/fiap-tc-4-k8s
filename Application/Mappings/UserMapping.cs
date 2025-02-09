using Application.Models.Authentication;
using AutoMapper;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Mappings;
[ExcludeFromCodeCoverage]
public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<RegisterModel, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(x => x.Cpf))
            .ReverseMap();
    }
}