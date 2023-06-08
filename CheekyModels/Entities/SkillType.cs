namespace CheekyModels.Entities;

public class SkillType
{
    public int SkillTypeId { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }

    public ICollection<TrainedSkill> TrainedSkills { get; set; }
}