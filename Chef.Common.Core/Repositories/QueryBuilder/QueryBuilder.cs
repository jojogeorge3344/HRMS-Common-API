using Chef.Common.Core;
using System;
using System.Collections.Generic;
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
            { "Enum[]", "integer[]" },
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

        private IEnumerable<string> UniqueCompositeFieldNames => typeof(T)
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    CompositeAttribute = (CompositeAttribute)Attribute.GetCustomAttribute(x, typeof(CompositeAttribute), true),
                    UniqueAttribute = (UniqueAttribute)Attribute.GetCustomAttribute(x, typeof(UniqueAttribute), true)
                })
                .Where(x => x.UniqueAttribute != null && x.CompositeAttribute != null)
                .OrderBy(x => x.CompositeAttribute != null ? x.CompositeAttribute.Index : Int32.MaxValue)
                .Select(x => x.Property.Name.ToLower())
                .ToList();

        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();

        public string TableName => SchemaName + "." + typeof(T).Name.ToLower();
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
                    IsRequired = prop.GetCustomAttributes(typeof(RequiredAttribute)).Count() > 0,
                    IsUnique = prop.GetCustomAttributes(typeof(UniqueAttribute)).Count() > 0 && prop.GetCustomAttributes(typeof(CompositeAttribute)).Count() == 0
                };

                if (prop.GetCustomAttribute(typeof(ForeignKeyAttribute)) is ForeignKeyAttribute foreignkeyAttribute)
                {
                    var schema = prop.DeclaringType.FullName.Split('.')[1].ToLower();
                    column.ForeignKey = string.Format("{0}.{1}", schema, foreignkeyAttribute.Name.ToLower());
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

                    if (column.ForeignKey != null)
                    {
                        createTableQuery.Append($" REFERENCES {column.ForeignKey}(id)");
                    }

                    if (column.IsUnique)
                    {
                        createTableQuery.Append($" UNIQUE");
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
                    createTableQuery.Append(string.Format("unique({0})", string.Join(",", UniqueCompositeFieldNames)));
                else
                    createTableQuery.Remove(createTableQuery.Length - 1, 1); //remove last comma
            }

            createTableQuery.Append(Environment.NewLine);
            createTableQuery.Append(");");
            createTableQuery.Append(Environment.NewLine + $" ALTER TABLE {TableName} OWNER to postgres; ");
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


        //string GetFilterQuery(string[] andFilterColumnNames = null)
        //{
        //    if (andFilterColumnNames != null && andFilterColumnNames.Count() > 0)
        //    {
        //        StringBuilder sb = new StringBuilder(" WHERE 1=1");
        //        for (int i = 0; i < andFilterColumnNames.Count(); i++)
        //        {
        //            sb.Append($" AND {andFilterColumnNames[i]} = @{andFilterColumnNames[i]}");
        //        }
        //        return sb.ToString();
        //    }
        //    return string.Empty;
        //}

        //public  string GenerateSelectiveColumnsQuery(string[] selectColumnNames, string[] andFilterColumnNames = null)
        //{
        //    var baseQuery = $"SELECT {{0}} FROM {TableName}";
        //    if (selectColumnNames == null || selectColumnNames.Count() == 0)
        //        baseQuery = string.Format(baseQuery, "*");
        //    if (selectColumnNames.Count() > 0)
        //        baseQuery = string.Format(baseQuery, String.Join(", ", selectColumnNames)); 
        //    return baseQuery + GetFilterQuery(andFilterColumnNames);
        //}

        //string GetColumnNamesAsCSV()
        //{
        //    var columns = GenerateColumnsForTable(GetProperties);
        //    return string.Join(",", columns.Select(x => x.Name));
        //}
        //public string GenerateSelectQuery(bool includeColumnNames = false)
        //{
        //    return string.Format($"SELECT {{0}} FROM {TableName}", (includeColumnNames ? GetColumnNamesAsCSV() : "*"));
        //}

        //public string GenerateSelectAllQuery(params string[] andFilterColumnNames)
        //{
        //    var baseQuery = $"SELECT * FROM {TableName}";
        //    if (andFilterColumnNames.Count() == 0)
        //        return baseQuery;
        //    else
        //        return baseQuery + GetFilterQuery(andFilterColumnNames);
        //}
        //public string GenerateSelectAllQuery(int limitNoOfRecords)
        //{
        //    return $"SELECT * FROM {TableName}  ORDER BY Id desc LIMIT {limitNoOfRecords}";
        //}

        //public string GenerateDeleteQuery()
        //    => $"DELETE FROM {TableName} WHERE id=@id";


        //string[] GetTableOrderedByReferences()
        //{ 

        //}
    }
}