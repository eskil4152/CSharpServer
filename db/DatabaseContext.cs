using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define your DbSet properties for database tables here
    public DbSet<Person> people { get; set; }
    public DbSet<User> users { get; set; }
}