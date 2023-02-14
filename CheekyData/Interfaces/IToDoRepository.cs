using CheekyData.Models;

namespace CheekyData.Interfaces;

public interface IToDoRepository : IRepository<ToDo>
{
    Task<ToDo> GetToDoIncludingUser(Guid userId);
}