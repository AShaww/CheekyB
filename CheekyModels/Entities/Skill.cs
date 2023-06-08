namespace CheekyModels.Entities;

public class Skill
{
    public Guid SkillId { get; set; }

    public string? SkillName { get; set; }

    public int SkillTypeId { get; set; }

    public virtual SkillType SkillType { get; set; }

    public virtual ICollection<UserSkill> Users { get; set; }
}
