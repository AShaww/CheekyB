using CheekyData.Models;
using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IToDoService
{
    Task<IEnumerable<ToDoDto>> GetTodoByUserId(Guid userId);
    Task<IEnumerable<ToDoDto>> GetAllToDos();
    Task<ToDoDto> InsertTodo(ToDoDto todo);
    Task<ToDoDto> UpdateTodo(ToDoDto todo);
    Task<ToDoDto> DeleteTodo(Guid toDoId);
}