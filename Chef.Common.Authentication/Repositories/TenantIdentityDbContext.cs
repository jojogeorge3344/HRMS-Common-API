using Chef.Common.Authentication.Models;
using Chef.Common.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chef.Common.Authentication.Repositories;

public class TenantIdenityDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ITenantProvider tenandProvider;
    public TenantIdenityDbContext(
        DbContextOptions<TenantIdenityDbContext> options,
        ITenantProvider tenandProvider)
        : base(options)
    {
        this.tenandProvider = tenandProvider;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var tenant = tenandProvider.GetCurrent();
            optionsBuilder.UseNpgsql(tenant.ConnectionString);
        }
    }
}

