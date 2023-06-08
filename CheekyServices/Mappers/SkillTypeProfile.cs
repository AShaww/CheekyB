using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyServices.Mappers;
/// <summary>
/// Mapper for dealing with SkillType data entry 
/// </summary>
public class SkillTypeProfile : Profile
{
    public SkillTypeProfile()
    {
        CreateMap<SkillType, SkillTypeDto>().ReverseMap();
    }
}
