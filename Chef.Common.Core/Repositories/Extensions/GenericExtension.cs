using Newtonsoft.Json;

namespace Chef.Common.Repositories
{
    public static class GenericExtension
    {
        //public static IReadOnlyDictionary<string, object> ToLowerReadOnlyDictionary<T>(this T value) where T : class
        //{
        //    var serializerSettings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new IgnoreContractResolver((new[] { "id" }))
        //    };
        //    var json = JsonConvert.SerializeObject(value, serializerSettings);
        //    return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, object>>(json);
        //}
        public static string Serialize<T>(this T value) where T : class
        {
            return JsonConvert.SerializeObject(value);
        }

    }
}
