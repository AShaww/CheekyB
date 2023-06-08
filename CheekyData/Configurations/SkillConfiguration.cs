using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skill");
        builder.HasKey(s => s.SkillId);
        
        builder.Property(p => p.SkillId).ValueGeneratedOnAdd();
        builder.Property(p => p.SkillName).IsRequired();
        
        builder.HasOne(a => a.SkillType)
               .WithMany(a => a.Skills)
               .HasForeignKey(a => a.SkillTypeId);
        
        builder.ToTable("Skill").HasData(InitialRatingSeed());
    }
    private static IEnumerable<Skill> InitialRatingSeed()
    {
        return new List<Skill>
        {
            new() { SkillId = Guid.NewGuid(), SkillName = "Skill 1", SkillTypeId = 1 },
            new() { SkillId = Guid.NewGuid(), SkillName = "Skill 2", SkillTypeId = 2 },
            new() { SkillId = Guid.NewGuid(), SkillName = "Skill 3", SkillTypeId = 1 },
        };
    }
}  