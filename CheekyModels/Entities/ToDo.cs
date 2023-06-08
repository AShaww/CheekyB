namespace CheekyModels.Entities;

public class ToDo
{
    public Guid ToDoId { get; set; }
    public Guid UserId { get; set; }
    public string ToDoTitle { get; set; }
    public string ToDoMessage { get; set; }
    public DateTime ToDoDateModified { get; set; }
    public virtual User User { get; set; }
}