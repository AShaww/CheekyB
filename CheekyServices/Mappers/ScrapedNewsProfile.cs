using AutoMapper;
using CheekyModels.Entities;
using CheekyModels.Dtos;

namespace CheekyServices.Mappers;

public class ScrapedNewsProfile : Profile
{
    public ScrapedNewsProfile()
    {
        CreateMap<ScrapedNews, ScrapedNewsDto>().ReverseMap();
    }
}