using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CheekyData.Interfaces;
using CheekyModels.Entities;

namespace CheekyData.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CheekyContext cheekyContext)
        : base(cheekyContext)
    {
    }

    public async Task<IEnumerable<User>> GetNotLoggedUsersAsync(Expression<Func<User, bool>> predicate)
        => await _cheekyContext.Set<User>().Where(predicate).ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<User>> GetAllUsersAsync(Expression<Func<User, bool>> predicate, int pageNumber, int pageSize)
    {
        var pagedData = await _cheekyContext.Users
            .Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return pagedData;
    }
}
