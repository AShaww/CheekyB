using Microsoft.EntityFrameworkCore;

namespace CheekyDatads;

public class CheekyContext : DbContext
{
    public CheekyContext(DbContextOptions<CheekyContext> options) : base(options)
    {
        
    }
}