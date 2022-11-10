namespace Chef.Common.Authentication.Repositories;

public class ConsoleIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ConsoleIdentityDbContext(DbContextOptions<ConsoleIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //set the default schema
        builder.HasDefaultSchema("common");

        base.OnModelCreating(builder);
    }
}
