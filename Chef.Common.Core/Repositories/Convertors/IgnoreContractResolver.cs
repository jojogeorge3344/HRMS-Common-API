using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Chef.Common.Repositories
{
    public class IgnoreContractResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;

        public IgnoreContractResolver(IEnumerable<string> propNamesToIgnore)
        {
            ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }

            return property;
        }

        protected override string ResolvePropertyName(string key)
        {
            return key.ToLower();
        }
    }
}
