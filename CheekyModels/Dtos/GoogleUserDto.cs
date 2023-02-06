namespace CheekyModels.Dtos;
public class GoogleUserDto
{
    public string GoogleUserId { get; set; }
    public string FamilyName { get; set; }
    public string GivenName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public long ExpirationTimeSeconds { get; set; }
}
