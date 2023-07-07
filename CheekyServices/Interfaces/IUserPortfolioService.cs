using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IUserPortfolioService
{
    /// <summary>
    /// Gets a user portfolio based on a user Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="CheekyException{UserPortfolioNotFoundException}"></exception>
    Task<UserPortfolioDto> GetUserPortfolioByUserId(Guid userId);
    /// <summary>
    /// Inserts a userportfolio 
    /// </summary>
    /// <param name="portfolioToInsert"></param>
    /// <returns></returns>
    /// <exception cref="CheekyException{UserPortfolioConflictException}"></exception>
    Task<UserPortfolioDto> InsertUserPortfolio(UserPortfolioDto portfolioToInsert);
    /// <summary>
    /// Updates user portfolio based on a specific Id 
    /// </summary>
    /// <param name="portfolio"></param>
    /// <returns></returns>
    /// <exception cref="CheekyException{UserPortfolioNotFoundException}"></exception>
    Task<UserPortfolioDto> UpdateUserPortfolio(UserPortfolioDto portfolio);
}

