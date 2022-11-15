
namespace Chef.Common.Authentication.Extensions;

public static class AutofacRegisterExtension
{
    public static void RegisterAuthentication(this ContainerBuilder builder)
    {
        builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerLifetimeScope();
        builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

        //Configure automapper
        builder.RegisterAutoMapper(typeof(MappingProfile).Assembly);
    }
}

