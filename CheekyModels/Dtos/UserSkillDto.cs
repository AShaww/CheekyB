namespace CheekyModels.Dtos;
public class UserSkillDto
{
    public Guid SkillId { get; set; }

    public Guid UserId { get; set; }

    public int RatingId { get; set; }

    public DateTime LastEvaluated { get; set; }

    public SkillDto Skill { get; set; }

    public UserDto User { get; set; }

    public RatingDto Rating { get; set; }
}
