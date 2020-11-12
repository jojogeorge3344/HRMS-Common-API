﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace Chef.Common.Repositories
{
    public class IgnoreContractResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;

        public IgnoreContractResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this.ignoreProps.Contains(property.PropertyName))
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
