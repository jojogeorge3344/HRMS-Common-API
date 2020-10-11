using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;

namespace Chef.Common.Test
{
    public class InjectPropertiesByDefaultModule : Autofac.Module
    { 
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.Activating += (s, e) =>
            {
                e.Context.InjectProperties(e.Instance);
            };
            base.AttachToComponentRegistration(componentRegistry, registration);
        }
    }
}
