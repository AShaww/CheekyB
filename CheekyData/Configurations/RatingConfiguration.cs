using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.RatingId);
        builder.Property(p => p.RatingId).ValueGeneratedOnAdd();
        builder.Property(p => p.RatingName).HasMaxLength(52);
        builder.ToTable("Rating").HasData(InitialRatings());
    }

    private static IEnumerable<Rating> InitialRatings()
    {
        return new List<Rating>
        {
            new() { RatingId = 1, RatingName = "1 - Awareness" },
            new() { RatingId = 2, RatingName = "2 - Novice" },
            new() { RatingId = 3, RatingName = "3 - Professional" },
            new() { RatingId = 4, RatingName = "4 - Expert" },
            new() { RatingId = 5, RatingName = "5 - Leading-edge expert" },
            new() { RatingId = 6, RatingName = "1 - Beginner" },
            new() { RatingId = 7, RatingName = "2 - Want to improve" },
            new() { RatingId = 8, RatingName = "3 - Proffesional" },
            new() { RatingId = 9, RatingName = "4 - Expert" },
            new() { RatingId = 10, RatingName = "5 - Leading-edge expert" }
        };
    }
}
