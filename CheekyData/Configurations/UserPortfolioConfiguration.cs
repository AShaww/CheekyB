using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations
{
    public class UserPortfolioConfiguration : IEntityTypeConfiguration<UserPortfolio>
    {
        public void Configure(EntityTypeBuilder<UserPortfolio> builder)
        {
            builder.HasKey(x => x.UserPortfolioId);
            builder.HasOne(a => a.User).WithOne(a => a.UserPortfolio).HasForeignKey<UserPortfolio>(a => a.UserId);
            builder.Property(p => p.UserPortfolioId).ValueGeneratedOnAdd();
            builder.Property(p => p.Capacity).HasColumnType("decimal(18,2)");
            builder.Property(p => p.UserId).IsRequired();
            builder.ToTable("UserPortfolio").HasData(InitialUserProfiles());
        }
        private static IEnumerable<UserPortfolio> InitialUserProfiles()
        {
            return new List<UserPortfolio>
            {
                new(){
                    UserPortfolioId = Guid.NewGuid(),
                    UserId = Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                    JobTitle = "",
                    DateJoined = DateTime.UtcNow,IntroductionMessage = "",
                    CV = "",
                    Capacity = 40
                }
            };
        }
    }
}
