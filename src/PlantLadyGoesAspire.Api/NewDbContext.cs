using Microsoft.EntityFrameworkCore;

public class NewDbContext(DbContextOptions<NewDbContext> options) : DbContext(options)
{
    public DbSet<Plant> Plants { get; set; }
}