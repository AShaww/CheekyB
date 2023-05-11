namespace CheekyModels.Dtos;

public class ScrapedNewsDto
{
    public Guid NewsId { get; set; }
    public string Title { get; set; }
    public string PageUrl { get; set; }
    public string ImageUrl { get; set; }
}