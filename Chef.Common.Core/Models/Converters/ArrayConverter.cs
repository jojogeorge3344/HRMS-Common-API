using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Core
{
    public class ArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
            //return (objectType.IsArray && typeof(T).IsAssignableFrom(objectType.GetElementType()));
            //return (objectType == typeof(List<T>));
        } 
        public override bool CanRead { get { return false; } }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException(); 
            //JToken token = JToken.Load(reader);
            //if (token.Type == JTokenType.Array)
            //{
            //    return new[] { token.ToObject<List<T>>() };
            //}
            //throw new Exception($"This property {objectType.Name} is marked with ArrayConverter however it is not an array property");
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

     
    }
}
