using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class TrainedSkillConfiguration : IEntityTypeConfiguration<TrainedSkill>
{
    public void Configure(EntityTypeBuilder<TrainedSkill> builder)
    {
        builder.HasKey(ts => ts.TrainedSkillId);
        builder.Property(ts => ts.Name).HasMaxLength(100).IsRequired();

        builder.HasOne(ts => ts.CoreSkill)
            .WithMany(cs => cs.TrainedSkills)
            .HasForeignKey(ts => ts.CoreSkillId);

        builder.HasOne(ts => ts.User)
            .WithMany(u => u.TrainedSkills)
            .HasForeignKey(ts => ts.UserId);
        
        builder.HasOne(ts => ts.SkillType)
            .WithMany(st => st.TrainedSkills)
            .HasForeignKey(ts => ts.SkillTypeId);

        builder.ToTable("TrainedSkill");
    }
}