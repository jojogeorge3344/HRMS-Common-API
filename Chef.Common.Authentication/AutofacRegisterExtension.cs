using Autofac;

namespace Chef.Common.Authentication
{
    public static class AutofacRegisterExtension
    {
        public static void RegisterAuthenticationComponents(this ContainerBuilder builder)
        {
            //register token service
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

            //authentication service
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleService>().As<IUserRoleService>().InstancePerLifetimeScope();
        }
    }
}

