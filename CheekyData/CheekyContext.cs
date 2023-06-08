using System.Reflection;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData;

public class CheekyContext : DbContext
{
    public CheekyContext(DbContextOptions<CheekyContext> options) : base(options)
    { }
        
    public DbSet<User> Users { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<ScrapedNews> ScrapedNews { get; set; }
    
    public DbSet<CoreSkill> CoreSkills { get; set; }
    public DbSet<TrainedSkill> TrainedSkills { get; set; }
    public DbSet<SkillType> SkillTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}