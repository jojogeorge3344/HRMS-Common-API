using Newtonsoft.Json.Serialization;

namespace Chef.Common.Repositories
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string key)
        {
            return key.ToLower();
        }
    }
}
