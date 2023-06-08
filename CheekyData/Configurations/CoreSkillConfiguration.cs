using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class CoreSkillConfiguration : IEntityTypeConfiguration<CoreSkill>
{
    public void Configure(EntityTypeBuilder<CoreSkill> builder)
    {
        builder.HasKey(cs => cs.CoreSkillId);
        builder.Property(cs => cs.Name).HasMaxLength(100).IsRequired();

        builder.ToTable("CoreSkill");
    }
}