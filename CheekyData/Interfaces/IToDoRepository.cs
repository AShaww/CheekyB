using System.Linq.Expressions;
using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface IToDoRepository : IRepository<ToDo>
{
    Task<ToDo> GetToDoIncludingUser(Guid userId);
    Task<IEnumerable<ToDo>> GetAllToDoAsync();
}