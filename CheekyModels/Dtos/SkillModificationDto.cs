namespace CheekyModels.Dtos;
public record SkillModificationDto
{
    public Guid SkillId { get; set; }

    public string SkillName { get; set; }

    public int SkillTypeId { get; set; }

}
