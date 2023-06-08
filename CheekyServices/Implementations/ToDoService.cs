using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Entities;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class ToDoService : IToDoService
{
    private readonly IMapper _mapper;
    private readonly IToDoRepository _toDoRepository;
    private readonly IUserRepository _userRepository;
    
    public ToDoService(IToDoRepository toDoRepository, IUserRepository userRepository, IMapper mapper)
    {
        _toDoRepository = toDoRepository ?? throw new ArgumentNullException(nameof(toDoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<IEnumerable<ToDoDto>> GetAllToDos()
    {
        var users = await _toDoRepository.GetAllToDoAsync();
        return _mapper.Map<IEnumerable<ToDoDto>>(users);
    }
    
    public async Task<IEnumerable<ToDoDto>> GetTodoByUserId(Guid toDoId)
    {
        var response = await _toDoRepository.GetAllAsync(a => a.ToDoId == toDoId);
        
        if (response == null)
        {
            throw new CheekyExceptions<ToDoNotFoundException>("The ToDo with the specified ID does not exist.");
        }

        //var todo = _mapper.Map<ToDo>(response);
        //var user = await _userRepository.GetFirstOrDefault(a => a.UserId == userId);

        return _mapper.Map<IEnumerable<ToDoDto>>(response);
    }

    public async Task<ToDoDto> InsertTodo(ToDoDto todo)
    {
        ArgumentException.ThrowIfNullOrEmpty(todo.ToDoTitle);
        ArgumentException.ThrowIfNullOrEmpty(todo.ToDoMessage);
        
        if (await _toDoRepository.DoesExistInDb(x => x.ToDoId == todo.ToDoId))
        {
            throw new CheekyExceptions<ToDoNotFoundException>(ToDoExceptionMessages.ToDoDuplicateExceptionMessage);
        }

        try
        {
            var toDoToAdd = _mapper.Map<ToDo>(todo);
            toDoToAdd.UserId = todo.UserId;
            toDoToAdd.ToDoId = new Guid();
            var addedToDo = await _toDoRepository.AddAsync(toDoToAdd);
            todo = _mapper.Map<ToDoDto>(addedToDo);

            return todo;
        }
        catch (Exception ex)
        {
            var text = ex;
            
        }
        return default;
    }

    public async Task<ToDoDto> UpdateTodo(ToDoDto todo)
    {
        var toDoToUpdate = await _toDoRepository.GetFirstOrDefault(a => a.ToDoId == todo.ToDoId);
        
        if (todo == null)
        {
            throw new CheekyExceptions<ToDoNotFoundException>(ToDoExceptionMessages.ToDoNotFoundExceptionMessage);
        }

        toDoToUpdate = _mapper.Map(todo, toDoToUpdate);
        await _toDoRepository.UpdateAsync(toDoToUpdate);

        return todo;
    }

    public async Task<ToDoDto> DeleteTodo(Guid toDoId)
    {
        var toDoToDelete = await _toDoRepository.GetFirstOrDefault(a => a.ToDoId == toDoId);
        if (toDoToDelete == null)
        {
            throw new CheekyExceptions<ToDoNotFoundException>(ToDoExceptionMessages.ToDoNotFoundExceptionMessage);
        }

        await _toDoRepository.DeleteAsync(toDoToDelete);
        return _mapper.Map<ToDoDto>(toDoToDelete);
    }

}