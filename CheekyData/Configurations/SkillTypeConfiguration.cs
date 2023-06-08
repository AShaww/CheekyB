using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class SkillTypeConfiguration : IEntityTypeConfiguration<SkillType>
{
    public void Configure(EntityTypeBuilder<SkillType> builder)
    {
        builder.HasKey(s => s.SkillTypeId);
        builder.Property(p => p.SkillTypeId).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasMaxLength(512);
        builder.ToTable("SkillType").HasData(InitialSkillTypes());
    }

    private static IEnumerable<SkillType> InitialSkillTypes()
    {
        return new List<SkillType> {
            new() { SkillTypeId = 1, Name = "Core" },
            new() { SkillTypeId = 2, Name = "Technical" } };
    }
}
