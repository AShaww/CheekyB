using CheekyData.Interfaces;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class UserPortfolioRepository : Repository<UserPortfolio>, IUserPortfolioRepository
{
    public UserPortfolioRepository(CheekyContext cheekyContext) : base(cheekyContext) { }


    public async Task<UserPortfolio> GetUserPortfolioIncludeUser(Guid userId)
    {
        var result = await _cheekyContext.UserPortfolios.Include(x => x.User).Where(z => z.UserId == userId).FirstOrDefaultAsync();
        return result;
    }
}

