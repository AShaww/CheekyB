using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class SkillTypeRatingConfiguration : IEntityTypeConfiguration<SkillTypeRating>
{
    public void Configure(EntityTypeBuilder<SkillTypeRating> builder)
    {
        builder.HasKey(x => new { x.SkillTypeId, x.RatingId });
        builder.HasOne(a => a.SkillType)
           .WithMany(a => a.SkillTypeRating)
           .HasForeignKey(a => a.SkillTypeId);
        builder.HasOne(a => a.Ratings)
           .WithMany(a => a.SkillTypeRating)
           .HasForeignKey(a => a.RatingId);
    }
}
