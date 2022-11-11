namespace Chef.Common.Authentication.Repositories;

public class ConsoleIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ITenantProvider tenantProvider;

    public ConsoleIdentityDbContext(
        DbContextOptions<ConsoleIdentityDbContext> options,
        ITenantProvider tenantProvider)
        : base(options)
    {
        this.tenantProvider = tenantProvider;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //set the default schema
        builder.HasDefaultSchema("common");

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(tenantProvider.GetConsoleConnectionString());
        }
    }
}
