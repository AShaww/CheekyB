using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyServices.Mappers;

/// <summary>
/// Mapper for dealing with UserPortfolio data entry 
/// </summary>
public class UserPortfolioProfile : Profile
{
    public UserPortfolioProfile()
    {
        CreateMap<UserPortfolio, UserPortfolioDto>().ReverseMap();
    }
}
