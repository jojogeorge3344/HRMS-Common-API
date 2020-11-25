using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public static IDictionary<string, object> ToDictionary(this object values)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (values != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj = propertyDescriptor.GetValue(values);
                    dictionary.Add(propertyDescriptor.Name.ToLower(), obj);
                }
            }

            return dictionary;
        }
    }
}
