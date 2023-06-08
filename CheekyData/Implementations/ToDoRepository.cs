using CheekyData.Interfaces;
using CheekyModels.Entities;
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

    public async Task<IEnumerable<ToDo>> GetAllToDoAsync()
    {
        return await _cheekyContext.ToDos.ToListAsync();
    }
}