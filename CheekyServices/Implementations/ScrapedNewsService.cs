using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class ScrapedNewsService : IScrapedNewsService
{
    private readonly IMapper _mapper;
    private readonly IScrapedNewsRepository _scrapedNewsRepository;

    public ScrapedNewsService(IScrapedNewsRepository scrapedNewsRepository, IMapper mapper)
    {
        _scrapedNewsRepository = scrapedNewsRepository ?? throw new ArgumentNullException(nameof(scrapedNewsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<IEnumerable<ScrapedNewsDto>> GetAllScrapedNews()
    {
        var news = await _scrapedNewsRepository.GetAllScrapedNewsAsync();
        return _mapper.Map<IEnumerable<ScrapedNewsDto>>(news);
    }
    
    public async Task<ScrapedNewsDto> DeleteScrapedNews(Guid scrapedNewsId)
    {
        var scrapedNewsToDelete = await _scrapedNewsRepository.GetFirstOrDefault(a => a.NewsId == scrapedNewsId);
        
        if (scrapedNewsToDelete == null)
        {
            throw new CheekyExceptions<ScrapedNewsNotFoundException>(ScrapedNewsExceptionMessages.ScrapedNewsNotFoundExceptionMessage);
        }

        await _scrapedNewsRepository.DeleteAsync(scrapedNewsToDelete);
        return _mapper.Map<ScrapedNewsDto>(scrapedNewsToDelete);
    }
}