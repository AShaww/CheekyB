namespace CheekyModels.Dtos;

public class TrainedSkillDto
{
    public Guid TrainedSkillId { get; set; }
    public UserDto User { get; set; }
    public CoreSkillDto CoreSkill { get; set; }
    public int SkillLevel { get; set; }
}