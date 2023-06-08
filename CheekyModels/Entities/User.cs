namespace CheekyModels.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    
    public bool Archived { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string? GoogleUserId { get; set; }
    
    public virtual ICollection<UserSkill> UserSkills { get; set; }
    public virtual IEnumerable<ToDo>? ToDos { get; set; }
}