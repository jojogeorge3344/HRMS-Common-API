using System.Text;
using Chef.Common.Authentication.Models;
using Chef.Common.Authentication.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Chef.Common.Authentication.Extensions;

public static class IdenityExentions
{
    public static void AddConsoleIdenity(this IServiceCollection services, IConfiguration configuration)
    {
        //Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
        services.AddDbContext<ConsoleIdentityDbContext>(options =>
            options.UseNpgsql(connectionString));

        //Add Identity and JWT
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ConsoleIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigurePasswordPolicies();
    }

    public static void AddTenantIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        //Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
        services.AddDbContext<TenantIdenityDbContext>(options =>
            options.UseNpgsql(connectionString));

        //Add Identity and JWT
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TenantIdenityDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigurePasswordPolicies();
    }

    private static void ConfigurePasswordPolicies(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
    }

    public static void AddJWTAuthentication(this IServiceCollection services, string secret)
    {
        //Adding Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
        });
    }
}
