using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(x => new { x.SkillId, x.UserId });
        builder.HasOne(x => x.Skill).WithMany(x => x.Users).HasForeignKey(x => x.SkillId);
        builder.HasOne(x => x.User).WithMany(x => x.UserSkills).HasForeignKey(x => x.UserId);
        builder.HasOne(a => a.Rating).WithMany(x => x.UserSkills).HasForeignKey(x => x.RatingId);
        builder.Property(p => p.LastEvaluated).HasDefaultValueSql("getutcdate()");
    }
}
