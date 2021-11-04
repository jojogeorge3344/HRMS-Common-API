using System;
using System.Linq;
using System.Reflection;

namespace Chef.Common.Core
{
    public class ForeignKeyCodeAttribute : Attribute
    {
        public Type ModelType { get; set; }

        public string KeyField { get; }

        public ForeignKeyCodeAttribute(Type modelType)
        {
            ModelType = modelType;

            KeyField = modelType.GetProperties().Where(x => x.GetCustomAttribute(typeof(CodeAttribute)) != null).Select(x => x.Name.ToLower()).FirstOrDefault();

            if (string.IsNullOrEmpty(KeyField))
                throw new Exception($"Foreignkey code reference not found. Add code attribute to the corresponding property in the primary table - {modelType.Name}");

        }
    }
}
