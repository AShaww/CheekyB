using CheekyData.Interfaces;
using CheekyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class ScrapedNewsRepository : Repository<ScrapedNews>, IScrapedNewsRepository
{
    public ScrapedNewsRepository(CheekyContext cheekyContext) : base(cheekyContext){}

    public async Task<IEnumerable<ScrapedNews>> GetAllScrapedNewsAsync()
    {
        return await _cheekyContext.ScrapedNews.ToListAsync();
    }
}