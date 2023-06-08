namespace CheekyModels.Entities;

public class SkillTypeRating
{
    public int SkillTypeId { get; set; }
    public int RatingId { get; set; }
    public virtual SkillType SkillType { get; set; }
    public virtual Rating Ratings { get; set; }
}

