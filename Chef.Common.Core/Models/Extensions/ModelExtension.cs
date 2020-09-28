using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Core
{
    public static class ModelExtension
    {
        public static string TableName(this Model model) {
            var type = model.GetType();
            var schemaName = type.Namespace.Split('.')[1].ToLower();
            return schemaName + "." + type.Name.ToLower();
        }
        public static string TableNameWOSchema(this Model model)
        {
            return model.GetType().Name.ToLower();
        }
        public static string SchemaName(this Model model)
        {
            return model.GetType().Namespace.Split('.')[1].ToLower();
        } 
    }
}
