using Chef.Common.Core;
using MimeKit.Encodings;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chef.Common.Repositories
{
    public class QueryBuilder<T> : IQueryBuilder<T> where T : Model
    {
        private readonly Dictionary<string, string> PropertyMap = new Dictionary<string, string>()
        {
            { "System.Int32", "integer" },
            { "Enum", "integer" },
            { "System.Boolean", "boolean"},
            { "System.Int16", "smallint"},
            { "System.Int64", "bigint"},
            { "System.Single", "real"},
            { "System.Double", "real"},
            { "System.String", "text"},
            { "System.DateTime", "timestamp"},
            { "System.Decimal", "money"},
            { "System.String[]", "text[]"},
            { "System.Int32[]", "int[]"},
            //{ "Enum[]", "integer[]" },
        };

        private readonly bool IsJunctionTable = typeof(T).GetCustomAttribute<TableTypeAttribute>() != null;

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        private IEnumerable<PropertyInfo> SortedProperties => typeof(T)
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    FieldAttribute = (FieldAttribute)Attribute.GetCustomAttribute(x, typeof(FieldAttribute), true)
                })
                .OrderBy(x => x.FieldAttribute != null ? x.FieldAttribute.Order : Int32.MaxValue)
                .Select(x => x.Property)
                .ToList();

        //private Dictionary<string, IEnumerable<CompositeAttribute>> UniqueCompositeFieldNames => typeof(T)
        //        .GetProperties()
        //        .Select(x => new
        //        {
        //            Property = x,
        //            CompositeAttributes = (IEnumerable<CompositeAttribute>)Attribute.GetCustomAttributes(x, typeof(CompositeAttribute), true),
        //            UniqueAttributes = (IEnumerable<UniqueAttribute>)Attribute.GetCustomAttributes(x, typeof(UniqueAttribute), true)
        //        })
        //        .Where(x => x.UniqueAttributes != null && x.CompositeAttributes != null && x.CompositeAttributes.Count() > 0)
        //    .Select(x => new { name = x.Property.Name.ToLower(), attributes = x.CompositeAttributes }).ToDictionary(t => t.name, t => t.attributes);

        private Dictionary<int, IEnumerable<string>> UniqueCompositeFieldNames => typeof(T)
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    CompositeAttributes = (IEnumerable<CompositeAttribute>)Attribute.GetCustomAttributes(x, typeof(CompositeAttribute), true),
                    UniqueAttributes = (IEnumerable<UniqueAttribute>)Attribute.GetCustomAttributes(x, typeof(UniqueAttribute), true)
                })
                .Where(x => x.UniqueAttributes != null && x.CompositeAttributes != null && x.CompositeAttributes.Count() > 0)
            .SelectMany(x => x.CompositeAttributes.Select(y=> new { y.Index, y.GroupNumber, Name = x.Property.Name.ToLower()  }))
      .GroupBy(y => y.GroupNumber)
            .Select(group => new { group.Key, Enumerable = group.Select(x => x.Name) })
            .Where(x=>x.Enumerable.Count() > 1)
            .ToDictionary(t => t.Key, t => t.Enumerable);

        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();
        public string TableName => SchemaName + "." + typeof(T).Name.ToLower();
        string GetTableName(Type type) 
            => type.Namespace.Split('.')[1].ToLower() + "." + type.Name.ToLower();
        public string TableNameWOSchema => typeof(T).Name.ToLower();

        public string PrimaryKey
        {
            get
            {
                var property = typeof(T).GetProperties().Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() }).Where(x => x.Attribute != null).FirstOrDefault();
                return property?.Property.Name;
            }
        }


        private List<Column> GenerateColumnsForTable(IEnumerable<PropertyInfo> listOfProperties)
        {
            var table = new List<Column>();

            foreach (var prop in listOfProperties)
            {

                if (prop.GetCustomAttribute(typeof(SkipAttribute)) is SkipAttribute)
                {
                    continue;
                }
                var column = new Column
                {
                    Name = prop.Name.ToLower(),
                    Type = GetPropertyType(prop),
                    IsKey = prop.GetCustomAttributes(typeof(KeyAttribute)).Count() > 0,
                    HasDefaultValue = prop.GetCustomAttributes(typeof(DefaultValueAttribute)).Count() > 0, 
                    IsRequired = prop.GetCustomAttributes(typeof(RequiredAttribute)).Count() > 0,
                    IsUnique = prop.GetCustomAttributes(typeof(UniqueAttribute)).Count() > 0 && prop.GetCustomAttributes(typeof(CompositeAttribute)).Count() != prop.GetCustomAttributes(typeof(UniqueAttribute)).Count()
                };

                if (column.HasDefaultValue)
                {
                    DefaultValueAttribute attr = (DefaultValueAttribute) prop.GetCustomAttribute(typeof(DefaultValueAttribute));
                    column.DefaultValue = prop.PropertyType == typeof(string)? "'" + attr.Value + "'": attr.Value;
                }
                if (prop.GetCustomAttribute(typeof(ForeignKeyAttribute)) is ForeignKeyAttribute foreignkeyAttribute)
                {
                    var schema = prop.DeclaringType.FullName.Split('.')[1].ToLower();
                    column.ForeignKey = string.Format("{0}.{1}", schema, foreignkeyAttribute.Name.ToLower());
                    column.ForeignKeyReference = $" REFERENCES {column.ForeignKey}(id)";
                }
                else if(prop.GetCustomAttribute(typeof(ForeignKeyIdAttribute)) is ForeignKeyIdAttribute idForeignKeyAttribute)
                { 
                    column.ForeignKey = GetTableName(idForeignKeyAttribute.ModelType);
                    column.ForeignKeyReference = $" REFERENCES {column.ForeignKey}(id)";
                }
                else if (prop.GetCustomAttribute(typeof(ForeignKeyCodeAttribute)) is ForeignKeyCodeAttribute codeForeignKeyAttribute)
                {
                    column.ForeignKey = GetTableName(codeForeignKeyAttribute.ModelType);
                    //var codePropertyName = codeForeignKeyAttribute.ModelType.GetProperties().Where(x => x.GetCustomAttribute(typeof(CodeAttribute)) != null).Select(x => x.Name.ToLower()).FirstOrDefault();
                    //if (string.IsNullOrEmpty(codePropertyName))
                    //    throw new Exception("Foreignkey code reference not found. Add Code attribute to the corresponding primary table");
                    column.ForeignKeyReference = $" REFERENCES {column.ForeignKey}({codeForeignKeyAttribute.KeyField})";
                }
                table.Add(column);
            }

            return table;
        }

        private List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(WriteAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as WriteAttribute)?.Write != false
                    select prop.Name).ToList();
        }


        private string GetPropertyType(PropertyInfo prop)
        {
            if (prop.PropertyType.IsEnum)
            {
                return PropertyMap["Enum"];
            }
            if (prop.PropertyType.IsGenericType &&
         prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return PropertyMap[prop.PropertyType.GetGenericArguments()[0].ToString()];
            }

            return PropertyMap[prop.PropertyType.ToString()];
        }

        public string GenerateCreateTableQuery()
        {
            const string tab = "\t\t";
            var indexQuery = new StringBuilder();
            int indexCounter = 0;
            var createTableQuery = new StringBuilder($"CREATE TABLE IF NOT EXISTS {TableName} (");
            //var columns = GenerateColumnsForTable(GetProperties);
            var columns = GenerateColumnsForTable(SortedProperties);
            columns.ForEach(column =>
            {
                if (column.IsKey && !IsJunctionTable)
                {
                    createTableQuery.Append(Environment.NewLine + tab + $"{column.Name} SERIAL PRIMARY KEY,");
                }
                else
                {
                    createTableQuery.Append(Environment.NewLine + tab + $"{column.Name} {column.Type}");

                    if (column.IsRequired)
                    {
                        createTableQuery.Append(" NOT NULL");
                    }
                    if (column.HasDefaultValue)
                    {
                        createTableQuery.Append(" DEFAULT " + column.DefaultValue);
                    }
                    if (column.ForeignKeyReference != null)
                    {
                        createTableQuery.Append(column.ForeignKeyReference);
                        //indexQuery.AppendLine($"CREATE INDEX idx_{TableNameWOSchema}_{++indexCounter}  ON {TableName}({column.Name});");
                    }

                    if (column.IsUnique)
                    {
                        createTableQuery.Append($" UNIQUE");
                        indexQuery.AppendLine($"CREATE INDEX idx_{TableNameWOSchema}_{++indexCounter}  ON {TableName}({column.Name});");
                    }

                    if (IsJunctionTable)
                    {
                        //createTableQuery.Append(" ON DELETE CASCADE");
                    }

                    createTableQuery.Append(",");
                }
            });

            if (IsJunctionTable)
            {
                var foreignKeyColumns = columns.Where(c => !string.IsNullOrEmpty(c.ForeignKey)).ToList();

                if (foreignKeyColumns.Count() > 0)
                {
                    createTableQuery.Append(Environment.NewLine + "PRIMARY KEY(");

                    foreignKeyColumns.ForEach(column =>
                    {
                        createTableQuery.Append($"{column.Name},");
                    });

                    createTableQuery.Remove(createTableQuery.Length - 1, 1); //remove last comma
                    createTableQuery.Append(")");
                }
            }
            else
            {
                if (UniqueCompositeFieldNames.Count() > 0)
                { 
                    foreach (var record in UniqueCompositeFieldNames)
                    {
                        indexQuery.AppendLine($"CREATE INDEX idx_{TableNameWOSchema}_{++indexCounter} ON {TableName}({string.Join(",", record.Value)});");
                        createTableQuery.Append(Environment.NewLine + tab + string.Format("constraint composite_{0}_{1} unique({2})", TableNameWOSchema,record.Key, string.Join(",", record.Value)));
                        createTableQuery.Append(",");
                    }
                }
                createTableQuery.Remove(createTableQuery.Length - 1, 1); //remove last comma
            }

            createTableQuery.Append(Environment.NewLine);
            createTableQuery.Append(");");
            createTableQuery.Append(Environment.NewLine + $" ALTER TABLE {TableName} OWNER to postgres; ");
            createTableQuery.Append(Environment.NewLine);
            createTableQuery.Append(Environment.NewLine); 
            createTableQuery.Append(indexQuery.ToString());
            createTableQuery.Append(Environment.NewLine);
            createTableQuery.Append(Environment.NewLine);

            return createTableQuery.ToString();
        }

        public string GenerateInsertQuery(bool returnId = true)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {TableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")" + (returnId ? " RETURNING Id" : string.Empty));

            return insertQuery.ToString();
        }

        public string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {TableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(prop =>
            {
                if (!prop.Equals("Id"))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        public string GenerateUpdateQueryOnConflict()
        {
            var updateQuery = new StringBuilder($"UPDATE SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(prop =>
            {
                if (!prop.Equals("Id"))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma

            return updateQuery.ToString();
        }


        public string GenerateDropTableQuery(bool cascade = false)
        {
            var keyword = cascade ? " CASCADE" : "";
            return $"DROP TABLE IF EXISTS {TableName}{keyword};";
        } 
    }
}