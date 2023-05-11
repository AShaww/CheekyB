using CheekyData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class ScrapedNewsConfiguration : IEntityTypeConfiguration<ScrapedNews>
{
    public void Configure(EntityTypeBuilder<ScrapedNews> builder)
    {
        builder.HasKey(g => g.NewsId);
        
        builder.ToTable("ScrapedNews").HasData(ScrapedNewsSeed());
    }

    private static IEnumerable<ScrapedNews> ScrapedNewsSeed()
    {
        return new List<ScrapedNews>
        {
            new()
            {
                NewsId = Guid.Parse("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"),
                Title = "First Random Title for News",
                PageUrl = "Pretend Page URL",
                ImageUrl = "Pretend IMG URL"
            }
        };
    }
}