namespace CheekyModels.Dtos;

public record SkillTypeRatingDto
{
    public int SkillTypeId { get; set; }
    public string SkillName { get; set; }
    public int RatingId { get; set; }
    public string RatingName { get; set; }
}

