using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IToDoService
{
    Task<IEnumerable<ToDoDto>> GetTodoByUserId(Guid userId);
    Task<IEnumerable<ToDoDto>> GetAllToDos();
    Task<ToDoDto> InsertTodo(ToDoDto toDoToInsert);
    Task<ToDoDto> UpdateTodo(ToDoDto todo);
    Task<ToDoDto> DeleteTodo(Guid toDoId);
}