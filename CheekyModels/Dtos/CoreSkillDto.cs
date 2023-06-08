namespace CheekyModels.Dtos;

public class CoreSkillDto
{
    public Guid CoreSkillId { get; set; }
    public string SkillName { get; set; }
    public SkillTypeDto SkillType { get; set; }
}