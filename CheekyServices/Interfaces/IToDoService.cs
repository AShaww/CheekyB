using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IToDoService
{
    Task<IEnumerable<ToDoDto>> GetTodoByUserId(Guid userId);
    Task<IEnumerable<ToDoDto>> GetAllToDos(int pageNumber, int pageSize);
    Task<ToDoDto> InsertTodo(ToDoDto toDoToInsert);
    Task<ToDoDto> UpdateTodo(ToDoDto todo);
}