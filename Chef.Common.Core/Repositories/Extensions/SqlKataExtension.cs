using Chef.Common.Core;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;

namespace Chef.Common.Repositories
{
    public static class SqlKataExtension
    {
        ///<summary>
        ///This is a Sql Kata Compile method. It returns compiled sql statement based on the compiler set. Currently it is to Postgres
        ///</summary>
        public static SqlResult Compile(this Query query)
        {
            var compiler = new PostgresCompiler();
            return compiler.Compile(query);
        }

        public static string LowerCaseSql(this SqlResult query)
        {
            return query.Sql.ToLower();
        }


        public static string ToSqlString(this SqlSearchOperator contionalOperator)
        {
            string stringOperator = contionalOperator switch
            {
                SqlSearchOperator.LessThan => "<",
                SqlSearchOperator.LessThanEqual => "<=",
                SqlSearchOperator.GreaterThan => ">",
                SqlSearchOperator.GreaterThanEqual => ">=",
                _ => "=",
            };
            return stringOperator;
        }

        static Query AddSearchCondition(this Query query, SqlSearchConditon condition)
        {
            switch (condition.Operator)
            {
                case SqlSearchOperator.Contains:
                    query.WhereContains(condition.Field, condition.Value);
                    break;
                case SqlSearchOperator.In:
                    if (condition.Value is IEnumerable<string> enumerableString)
                        query.WhereIn(condition.Field, enumerableString);
                    else if (condition.Value is IEnumerable<int> enumerableInt)
                        query.WhereIn(condition.Field, enumerableInt);
                    else
                        query.Where(condition.Field, SqlSearchOperator.Equal.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.StartsWith:
                    query.WhereStarts(condition.Field, condition.Value);
                    break;
                case SqlSearchOperator.EndsWith:
                    query.WhereEnds(condition.Field, condition.Value);
                    break;
                case SqlSearchOperator.Equal:
                case SqlSearchOperator.GreaterThan:
                case SqlSearchOperator.LessThan:
                case SqlSearchOperator.GreaterThanEqual:
                case SqlSearchOperator.LessThanEqual:
                    query.Where(condition.Field, condition.Operator.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.NotEqual:
                    query.WhereNot(condition.Field, condition.Operator.ToSqlString(), condition.Value);
                    break;
            }
            return query;
        }

        public static Query ApplySqlSearch(this Query query, SqlSearch sqlSearch)
        {
            if (sqlSearch == null || SqlSearch.DefaultInstance.Equals(sqlSearch))
                return query;
            foreach (SqlSearchGroup group in sqlSearch.Groups)
            {
                var queryGroup = new Query();
                if (!group.Conditions.Any())
                    continue;
                if (group.Conditions.Count == 1)
                {
                    var single = group.Conditions.Single();
                    queryGroup.AddSearchCondition(single);
                    query.Where(q => queryGroup);
                    continue;
                }
                var first = group.Conditions.First();
                var last = group.Conditions.Last();
                var others = group.Conditions.Skip(1).Take(group.Conditions.Count - 2);
                queryGroup.AddSearchCondition(first);
                foreach (var c in others)
                    queryGroup.AddSearchCondition(c);
                queryGroup.AddSearchCondition(last);
                query.Where(q => queryGroup);
            }

            if (sqlSearch.Page != null)
                query.ForPage(sqlSearch.Page.PageNo, sqlSearch.Page.PageLimit);
            else if (sqlSearch.Limit.HasValue)
                query.Limit(sqlSearch.Limit.Value);
            return query;
        }

        public static string[] GenerateFieldsWithAlias(this Dictionary<string, string> keyValuePairs)
        {
            return keyValuePairs.Select(x => string.Join(" as ", x.Value, x.Key)).ToArray();
        }

        public static SqlSearch MapAliasToField(this SqlSearch sqlSearch, Dictionary<string, string> aliases)
        {
            if (sqlSearch != null && !SqlSearch.DefaultInstance.Equals(sqlSearch) && aliases.Count > 0)
            {
                var listConditons = sqlSearch.Groups.Where(x => x.Conditions.Any(y => aliases.ContainsKey(y.Field))).Select(z => z.Conditions);
                foreach (var conditions in listConditons)
                {
                    for (int i = 0; i < conditions.Count; i++)
                    {
                        conditions[i] = new SqlSearchConditon()
                        {
                            Field = aliases[conditions[i].Field],
                            Operator = conditions[i].Operator,
                            Value = conditions[i].Value
                        };
                    }
                }
            }
            return sqlSearch;
        }

        static string TableName<T>()
        {
            var schemaName = typeof(T).Namespace.Split('.')[1].ToLower();
            return schemaName + "." + typeof(T).Name.ToLower();
        }

        public static Query Join<T>(this Query query, Func<Join, Join> callback, string type = "inner join") =>
            query.Join(TableName<T>(), callback, type);
        public static Query Join<T>(this Query query, string first, string second, string op = "=", string type = "inner join") =>
            query.Join(TableName<T>(), first, second, op, type);
        public static Query LeftJoin<T>(this Query query, Func<Join, Join> callback) =>
            query.LeftJoin(TableName<T>(), callback);
        public static Query LeftJoin<T>(this Query query, string first, string second, string op = "=") =>
            query.LeftJoin(TableName<T>(), first, second, op);

        static IDictionary<string, object> GetDictionary(object obj)
        { 
            var type = obj.GetType();
            var props = type.GetProperties();
            return props.ToDictionary(x => x.Name.ToLower(), x => x.GetValue(obj, null));
        }
        static string[] GetColumns(object obj)
        {
            IDictionary<string, object> expando = GetDictionary(obj);
            return expando.Select(x => x.Key).ToArray();
        }

        static void UpdateDefaultProperties(ref IDictionary<string, object> expando)
        {
            if (expando.ContainsKey("createdby"))
                expando.Remove("createdby");
            if (expando.ContainsKey("createddate"))
                expando.Remove("createddate");
        }
        static void InsertDefaultProperties(ref IDictionary<string, object> expando)
        {
            if (expando.ContainsKey("id"))
                expando.Remove("id");
            if (!expando.ContainsKey("createdby"))
                expando.Add("createdby", "system");
            if (!expando.ContainsKey("modifiedby"))
                expando.Add("modifiedby", "system");
            if (!expando.ContainsKey("createddate"))
                expando.Add("createddate", DateTime.UtcNow);
            if (!expando.ContainsKey("modifieddate"))
                expando.Add("modifieddate", DateTime.UtcNow);
            if (!expando.ContainsKey("isarchived"))
                expando.Add("isarchived", false);
        }
        public static Query AsUpdateExt(this Query query, object obj)
        {
            IDictionary<string, object> expando = GetDictionary(obj);
            UpdateDefaultProperties(ref expando);
            return query.AsUpdate(new ReadOnlyDictionary<string, object>(expando));
        }
        public static Query AsInsertExt(this Query query, object obj, bool returnId = false)
        {
            IDictionary<string, object> expando = GetDictionary(obj);
            InsertDefaultProperties(ref expando);
            return query.AsInsert(new ReadOnlyDictionary<string, object>(expando), returnId: returnId);
        }
        
        public static Query AsBulkInsertExt(this Query query, IEnumerable<object> objects)
        {
            List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            foreach (object record in objects)
            {
                IDictionary<string, object> expando = GetDictionary(record);
                InsertDefaultProperties(ref expando);
                list.Add(expando);
            }
            var firstObject = list.FirstOrDefault();
            var columns = GetColumns(firstObject);
            var data = list.Select(x => x.Values);
            return query.AsInsert(columns: columns, data);
        }

        //public static Query WhereDateGreaterThanOrEqual(this Query query, string column, DateTime value) =>
        //    query.WhereDate(column, ">=", value.ToShortDateString());
        //public static Query WhereDateTimeGreaterThanOrEqual(this Query query, string column, DateTime value) =>
        //    query.WhereDate(column, ">=", value);
        //public static Query WhereDateLessThanOrEqual(this Query query, string column, DateTime value) =>
        //    query.WhereDate(column, "<=", value.ToShortDateString());
        //public static Query WhereDateTimeLessThanOrEqual(this Query query, string column, DateTime value) =>
        //    query.WhereDate(column, "<=", value);

        //public static Query AsInsertExt(this Query query, object data, bool returnId = false)
        //{
        //    var serializerSettings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new IgnoreContractResolver((new[] { "id" }))
        //    };
        //    //serializerSettings.Converters.Add(new ArrayConverter<string>());
        //    var json = JsonConvert.SerializeObject(data, serializerSettings); 
        //    var dictionary = JsonConvert.DeserializeObject<IReadOnlyDictionary<string, object>>(json);
        //    return query.AsInsert(dictionary, returnId);
        //}
    }
}
