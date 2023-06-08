namespace CheekyModels.Dtos;

public record UserSkillFilterDto(int PageNumber, int PageSize, string[] SkillNames)
{
    public string[] SkillNames { get; private  set; } = SkillNames;
    public int PageNumber { get; private set; } = PageNumber;
    public int PageSize { get; private set; } = PageSize;
}
