using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using Chef.Common.Core;

namespace Chef.Common.Repositories
{
    public class QueryBuilder<T> : IQueryBuilder<T> where T : IModel
    {
        private readonly Dictionary<string, string> PropertyMap = new()
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
            { "System.Decimal", "numeric(20,6)"},
            { "System.String[]", "text[]"},
            { "System.Int32[]", "int[]"},
            { "System.TimeSpan", "timestamp"},
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


        private Dictionary<int, IEnumerable<string>> UniqueCompositeFieldNames => typeof(T)
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    CompositeAttributes = (IEnumerable<CompositeAttribute>)Attribute.GetCustomAttributes(x, typeof(CompositeAttribute), true),
                    UniqueAttributes = (IEnumerable<UniqueAttribute>)Attribute.GetCustomAttributes(x, typeof(UniqueAttribute), true)
                })
            .Where(x => x.UniqueAttributes != null && x.CompositeAttributes != null && x.CompositeAttributes.Count() > 0)
            .SelectMany(x => x.CompositeAttributes.Select(y => new { y.Index, y.GroupNumber, Name = x.Property.Name.ToLower() }))
            .GroupBy(y => y.GroupNumber)
            .Select(group => new { group.Key, Enumerable = group.Select(x => x.Name) })
            .Where(x => x.Enumerable.Count() > 1)
            .ToDictionary(t => t.Key, t => t.Enumerable);

        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();

        public string TableName => SchemaName + "." + typeof(T).Name.ToLower();

        private string GetTableName(Type type)
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
            List<Column> table = new();

            foreach (PropertyInfo prop in listOfProperties)
            {

                if (prop.GetCustomAttribute(typeof(SkipAttribute)) is SkipAttribute)
                {
                    continue;
                }
                Column column = new()
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
                    DefaultValueAttribute attr = (DefaultValueAttribute)prop.GetCustomAttribute(typeof(DefaultValueAttribute));
                    object value = prop.PropertyType.IsEnum ? Convert.ChangeType(attr.Value, Enum.GetUnderlyingType(attr.Value.GetType())) : attr.Value;
                    column.DefaultValue = prop.PropertyType == typeof(string) ? "'" + value + "'" : value;
                }
                if (prop.GetCustomAttribute(typeof(ForeignKeyAttribute)) is ForeignKeyAttribute foreignkeyAttribute)
                {
                    string schema = prop.DeclaringType.FullName.Split('.')[1].ToLower();
                    column.ForeignKey = string.Format("{0}.{1}", schema, foreignkeyAttribute.Name.ToLower());
                    column.ForeignKeyReference = $" REFERENCES {column.ForeignKey}(id)";
                }
                else if (prop.GetCustomAttribute(typeof(ForeignKeyIdAttribute)) is ForeignKeyIdAttribute idForeignKeyAttribute)
                {
                    column.ForeignKey = GetTableName(idForeignKeyAttribute.ModelType);
                    column.ForeignKeyReference = $" REFERENCES {column.ForeignKey}(id)";
                }
                else if (prop.GetCustomAttribute(typeof(ForeignKeyCodeAttribute)) is ForeignKeyCodeAttribute codeForeignKeyAttribute)
                {
                    column.ForeignKey = GetTableName(codeForeignKeyAttribute.ModelType);
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
            StringBuilder indexQuery = new();
            int indexCounter = 0;

            StringBuilder createTableQuery = new($"CREATE TABLE IF NOT EXISTS {TableName} (");
            //var columns = GenerateColumnsForTable(GetProperties);
            List<Column> columns = GenerateColumnsForTable(SortedProperties);

            columns.ForEach(column =>
            {
                if (column.IsKey && !IsJunctionTable)
                {
                    _ = createTableQuery.Append(Environment.NewLine + tab + $"{column.Name} SERIAL PRIMARY KEY,");
                }
                else
                {
                    _ = createTableQuery.Append(Environment.NewLine + tab + $"{column.Name} {column.Type}");

                    if (column.IsRequired)
                    {
                        _ = createTableQuery.Append(" NOT NULL");
                    }

                    if (column.HasDefaultValue)
                    {
                        _ = createTableQuery.Append(" DEFAULT " + column.DefaultValue);
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
                List<Column> foreignKeyColumns = columns.Where(c => !string.IsNullOrEmpty(c.ForeignKey)).ToList();

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
                    foreach (KeyValuePair<int, IEnumerable<string>> record in UniqueCompositeFieldNames)
                    {
                        indexQuery.AppendLine($"CREATE INDEX idx_{TableNameWOSchema}_{++indexCounter} ON {TableName}({string.Join(",", record.Value)});");
                        createTableQuery.Append(Environment.NewLine + tab + string.Format("constraint composite_{0}_{1} unique({2})", TableNameWOSchema, record.Key, string.Join(",", record.Value)));
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
            StringBuilder insertQuery = new($"INSERT INTO {TableName} ");

            _ = insertQuery.Append('(');

            List<string> properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { _ = insertQuery.Append($"{prop},"); });

            _ = insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { _ = insertQuery.Append($"@{prop},"); });

            _ = insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")" + (returnId ? " RETURNING Id" : string.Empty));

            return insertQuery.ToString();
        }

        public string GenerateInsertQueryForAudit(string operation, int partentTableID, int auditId = 0, bool returnId = true)
        {
            StringBuilder insertQuery = new($"INSERT INTO {TableName} ");

            insertQuery.Append("(");

            List<string> properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop =>
            {
                if (prop == "ParentTableId")
                {
                    insertQuery.Append($"{partentTableID},");
                }
                else if (prop == "AuditId")
                {
                    insertQuery.Append($"'{auditId}',");
                }
                else if (prop == "AuditOperation")
                {
                    insertQuery.Append($"'{operation}',");
                }
                else
                {
                    insertQuery.Append($"@{prop},");
                }
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")" + (returnId ? " RETURNING Id" : string.Empty));

            return insertQuery.ToString();
        }

        public string GenerateUpdateQuery()
        {
            StringBuilder updateQuery = new($"UPDATE {TableName} SET ");
            List<string> properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(prop =>
            {
                if ((!prop.Equals("Id")) && (!prop.Equals("CreatedDate")) && (!prop.Equals("CreatedBy")))
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
            StringBuilder updateQuery = new($"UPDATE SET ");
            List<string> properties = GenerateListOfProperties(GetProperties);

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
            string keyword = cascade ? " CASCADE" : "";
            return $"DROP TABLE IF EXISTS {TableName}{keyword};";
        }

        public string GenerateTruncateTableQuery(bool cascade = false, bool restartIdentity = false)
        {
            string keyword = string.Format("{0}{1}", restartIdentity ? " RESTART IDENTITY" : "", cascade ? " CASCADE" : "");
            return $"TRUNCATE TABLE {TableName}{keyword};";
        }
    }
}