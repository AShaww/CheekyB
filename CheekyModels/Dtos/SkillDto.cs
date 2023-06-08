namespace CheekyModels.Dtos;

public record SkillDto
{
    public Guid SkillId { get; set; }

    public string SkillName { get; set; }

    public int SkillTypeId { get; set; }

    public SkillTypeDto SkillType { get; set; }

}
