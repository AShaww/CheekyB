namespace CheekyModels.Dtos;

public record ToDoDto
{
    public Guid ToDoId { get; set; }
    public string ToDoTitle { get; set; }
    public string ToDoMessage { get; set; }
    public DateTime ToDoDateCreated { get; set; }
}