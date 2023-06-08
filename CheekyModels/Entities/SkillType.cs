namespace CheekyModels.Entities;

public class SkillType
{
    public int SkillTypeId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Skill> Skills { get; set; }
    public virtual ICollection<SkillTypeRating> SkillTypeRating { get; set; }
}