using Autofac;
using Chef.Common.Authentication.Repositories;

namespace Chef.Common.Authentication;

public static class AutofacRegisterExtension
{
    public static void RegisterAuthenticationComponents(this ContainerBuilder builder)
    {
        builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerLifetimeScope();
    }
}

