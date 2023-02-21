using System.Linq.Expressions;
using CheekyData.Models;

namespace CheekyData.Interfaces;

public interface IToDoRepository : IRepository<ToDo>
{
    Task<ToDo> GetToDoIncludingUser(Guid userId);
    Task<IEnumerable<ToDo>> GetAllToDoAsync(Expression<Func<ToDo, bool>> predicate, int pageNumber, int pageSize);
}