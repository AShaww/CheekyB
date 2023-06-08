namespace CheekyModels.Entities;

public class ScrapedNews
{
    public Guid NewsId { get; set; }
    public string Title { get; set; }
    public string PageUrl { get; set; }
    public string ImageUrl { get; set; }
}