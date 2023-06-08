namespace CheekyModels.Entities;

public class CoreSkill
{
    public Guid CoreSkillId { get; set; }
    public string Name { get; set; }

    public ICollection<TrainedSkill> TrainedSkills { get; set; }
}