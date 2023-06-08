namespace CheekyModels.Dtos;

public record UserSkillViewDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public IEnumerable<string> Skills { get; set; }
    public DateTime LastEvaluated { get; set; }
}
