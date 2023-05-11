using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IScrapedNewsService
{
    Task<IEnumerable<ScrapedNewsDto>> GetAllScrapedNews();
    Task<ScrapedNewsDto> DeleteScrapedNews(Guid scrapedNewsId);
}