using CheekyData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(g => g.UserId);
        builder.Property(p => p.GoogleUserId).HasMaxLength(255);
        builder.Property(p => p.UserId).ValueGeneratedOnAdd();
        builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(p => p.Archived).HasDefaultValue(false);
        builder.Property(p => p.CreatedOn).HasDefaultValueSql("getutcdate()");
        builder.Property(p => p.ModifiedOn).HasDefaultValueSql("getutcdate()");
        
        builder.ToTable("User").HasData(UserSeed());
    }
    
    private static IEnumerable<User> UserSeed()
    {
        return new List<User>
        {
            new()
            {
                UserId =  Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                FirstName = "Amir",
                LastName = "Shaw",
                Email = "AmirShaw@hotmail.co.uk",
                Archived = false
            }
        };
    }
}