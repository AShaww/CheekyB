using AutoMapper;
using CheekyModels.Entities;
using CheekyModels.Dtos;

namespace CheekyServices.Mappers;

public class ToDoProfile : Profile
{
    public ToDoProfile()
    {
        CreateMap<ToDo, ToDoDto>().ReverseMap();
    }
}