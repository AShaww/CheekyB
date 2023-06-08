namespace CheekyModels.Entities;

public class Rating
{
    public int RatingId { get; set; }
    public string RatingName { get; set; }
    public virtual ICollection<UserSkill> UserSkills { get; set; }
    public virtual ICollection<SkillTypeRating> SkillTypeRating { get; set; }
}
