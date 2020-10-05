using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text; 

namespace Chef.Common.Repositories
{
    public class DictionaryConverter : JsonConverter
    {
		public override bool CanWrite { get { return false; } }
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Dictionary<string, object>);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				reader.Read();
				if (reader.TokenType == JsonToken.EndArray)
					return new Dictionary<string, string>();
				else
					throw new JsonSerializationException("Non-empty JSON array does not make a valid Dictionary!");
			}
			else if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			else if (reader.TokenType == JsonToken.StartObject)
			{
				Dictionary<string, object> ret = new Dictionary<string, object>();
				reader.Read();
				while (reader.TokenType != JsonToken.EndObject)
				{
					if (reader.TokenType != JsonToken.PropertyName)
						throw new JsonSerializationException("Unexpected token!");
					string key = (string)reader.Value;
					reader.Read();
					//string value = (string)reader.Value;
					ret.Add(key, reader.Value);
					reader.Read();
				}
				return ret;
			}
			else
			{
				throw new JsonSerializationException("Unexpected token!");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}


    }
}
