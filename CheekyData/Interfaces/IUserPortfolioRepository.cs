using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface IUserPortfolioRepository : IRepository<UserPortfolio>
{
    Task<UserPortfolio> GetUserPortfolioIncludeUser(Guid userId);
}
