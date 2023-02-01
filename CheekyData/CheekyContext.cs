using Microsoft.EntityFrameworkCore;

namespace CheekyData;

public class CheekyContext : DbContext
{
    public CheekyContext(DbContextOptions<CheekyContext> options) : base(options)
    {
        
    }
}