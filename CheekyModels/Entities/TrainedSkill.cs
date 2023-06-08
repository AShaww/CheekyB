namespace CheekyModels.Entities;

public class TrainedSkill
{
    public Guid TrainedSkillId { get; set; }
    public string Name { get; set; }
    
    public Guid CoreSkillId { get; set; }
    public CoreSkill CoreSkill { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int SkillTypeId { get; set; }
    public SkillType SkillType { get; set; }
}