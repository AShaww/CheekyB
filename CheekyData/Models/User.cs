namespace CheekyData.Models;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? GoogleUserId { get; set; }
    public bool Archived { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    
    public virtual IEnumerable<ToDo>? ToDos { get; set; }
}