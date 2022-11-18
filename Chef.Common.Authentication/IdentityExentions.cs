using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Chef.Common.Authentication.Extensions;

public static class IdenityExentions
{
    public static void AddConsoleIdentity(this IServiceCollection services)
    {
        services.AddDbContext<ConsoleIdentityDbContext>();

        //Add Identity and JWT
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ConsoleIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigurePasswordPolicies();
    }

    public static void AddTenantIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        //Add services to the container.
        services.AddDbContext<TenantIdentityDbContext>(ServiceLifetime.Transient);

        //Add Identity and JWT
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TenantIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureJWT(configuration);
        services.ConfigurePasswordPolicies();
    }

    private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfigOptions>(configuration.GetSection(JwtConfigOptions.JwtConfig));
        services.AddJWTAuthentication(configuration["JwtConfig:Secret"]);
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
