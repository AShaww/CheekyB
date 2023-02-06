using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CheekyData.Models;

namespace CheekyData;

public class CheekyContext : DbContext
{
    public CheekyContext(DbContextOptions<CheekyContext> options) : base(options)
    { }
        
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}