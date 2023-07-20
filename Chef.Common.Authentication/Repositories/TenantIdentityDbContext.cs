using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Chef.Common.Authentication.Repositories;

public class TenantIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ITenantProvider tenantProvider;

    public TenantIdentityDbContext(
        DbContextOptions<TenantIdentityDbContext> options,
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
            var tenant = tenantProvider.GetCurrent();
            optionsBuilder.UseNpgsql(tenant.ConnectionString);
        }
    }
}

