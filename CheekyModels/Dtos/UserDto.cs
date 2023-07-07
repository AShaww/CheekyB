namespace CheekyModels.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public bool Archived { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public string? GoogleUserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}