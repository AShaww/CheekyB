using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface IScrapedNewsRepository : IRepository<ScrapedNews>
{
    Task<IEnumerable<ScrapedNews>> GetAllScrapedNewsAsync();
}