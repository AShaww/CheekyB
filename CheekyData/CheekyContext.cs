using System.Reflection;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData;

public class CheekyContext : DbContext
{
    public CheekyContext(DbContextOptions<CheekyContext> options) : base(options)
    { }
        
    public DbSet<User> Users { get; set; }
    
    
    public DbSet<SkillType> SkillTypes { get; set; }
    public DbSet<Skill> Skills { get; set; }
    
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<UserPortfolio> UserPortfolios { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<ScrapedNews> ScrapedNews { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}