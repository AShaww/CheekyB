using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyServices.Mappers;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<Skill, SkillDto>()
            .ForMember(x => x.SkillType, opt => opt.MapFrom(x => x.SkillType)).ReverseMap();

        CreateMap<Skill, SkillModificationDto>().ReverseMap();
    }
}
