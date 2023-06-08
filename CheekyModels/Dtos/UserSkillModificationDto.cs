namespace CheekyModels.Dtos;

public class UserSkillModificationDto
{
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public int RatingId { get; set; }
}
