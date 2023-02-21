using System.Linq.Expressions;
using CheekyData.Interfaces;
using CheekyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class ToDoRepository : Repository<ToDo>, IToDoRepository
{
    public ToDoRepository(CheekyContext cheekyContext) : base(cheekyContext) { }

    public async Task<ToDo> GetToDoIncludingUser(Guid userId)
    {
        var result = await _cheekyContext.ToDos.Include(x => x.User)
            .Where(z => z.UserId == userId).FirstOrDefaultAsync();

        return result;
    }

    public async Task<IEnumerable<ToDo>> GetAllToDoAsync(Expression<Func<ToDo, bool>> predicate, int pageNumber, int pageSize)
    {
        var pagedData = await _cheekyContext.ToDos
            .Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return pagedData;
    }
    
}