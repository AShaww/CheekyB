using AutoMapper;
using CheekyData.Models;
using CheekyModels.Dtos;

namespace CheekyServices.Mappers;

/// <summary>
/// Mapper for dealing with User data entry 
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<GoogleUserDto, UserDto>()
            .ForMember(x => x.FirstName, opt =>
                opt.MapFrom(x => x.GivenName))
            .ForMember(x => x.LastName, opt =>
                opt.MapFrom(x => x.FamilyName));
    }
}