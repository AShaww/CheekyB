using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using Google.Apis.Auth;

namespace CheekyServices.Mappers;

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
        
        CreateMap<GoogleJsonWebSignature.Payload, GoogleUserDto>()
            .ForMember(x => x.GoogleUserId, opt => opt.MapFrom(s => s.Subject))
            .ForMember(x => x.FullName, opt => opt.MapFrom(s => s.Name));
    }
}
