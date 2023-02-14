using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IToDoService
{
    Task<ToDoDto> GetTodoByUserId(Guid userId);
    Task<ToDoDto> InsertTodo(ToDoDto toDoToInsert);
    Task<ToDoDto> UpdateTodo(ToDoDto todo);
}