using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyServices.Mappers;

/// <summary>
/// Mapper for dealing with UserSkill data entry 
/// </summary>
public class UserSkillProfile : Profile
{
    public UserSkillProfile()
    {
        CreateMap<UserSkill, UserSkillDto>()
            .ForMember(x => x.User, opt => opt.MapFrom(s => s.User))
            .ForMember(x => x.Skill, opt => opt.MapFrom(s => s.Skill))
            .ForPath(x => x.Rating.RatingId, opt => opt.MapFrom(s => s.Rating.RatingId))
            .ForPath(x => x.Rating.RatingName, opt => opt.MapFrom(s => s.Rating.RatingName))
            .ReverseMap();

        CreateMap<UserSkill, UserSkillModificationDto>().ReverseMap();
        CreateMap<User, UserSkillModificationDto>().ReverseMap();
        CreateMap<Skill, UserSkillModificationDto>().ReverseMap();
        CreateMap<Rating, UserSkillModificationDto>().ReverseMap();
    }
}
