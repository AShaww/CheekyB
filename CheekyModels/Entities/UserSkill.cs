namespace CheekyModels.Entities;

public class UserSkill
{
    public Guid SkillId { get; set; }

    public Guid UserId { get; set; }

    public int RatingId { get; set; }

    public DateTime LastEvaluated { get; set; }

    public virtual Skill Skill { get; set; }

    public virtual User User { get; set; }
    public virtual Rating Rating { get; set; }
}
