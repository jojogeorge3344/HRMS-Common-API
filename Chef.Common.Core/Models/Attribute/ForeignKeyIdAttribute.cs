using System;

namespace Chef.Common.Core
{
    public class ForeignKeyIdAttribute : Attribute
    {
        public Type ModelType { get; set; }

        public string KeyField { get; set; } = "id";

        public ForeignKeyIdAttribute(Type modelType)
        {
            ModelType = modelType;
        }
    }
}
