using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class SkillTypeConfiguration : IEntityTypeConfiguration<SkillType>
{
    public void Configure(EntityTypeBuilder<SkillType> builder)
    {
        builder.HasKey(st => st.SkillTypeId);
        builder.Property(st => st.Description).HasMaxLength(255).IsRequired();
        builder.Property(st => st.Rating).IsRequired();

        builder.ToTable("SkillType");
    }
}