using Chef.Common.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;

namespace Chef.Common.Repositories
{
    public static class SqlKataExtension
    {
        public static string FieldName<T>(Expression<Func<T, object>> fieldName) 
            => string.Format("{0}.{1}", TableNameWOSchema<T>(), GetPropertyName(fieldName));
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

        //public static IDictionary<string, object> ToDictionary(this object data)
        //{
        //    BindingFlags publicAttributes = BindingFlags.Public | BindingFlags.Instance;
        //    Dictionary<string, object> dictionary = new Dictionary<string, object>();

        //    foreach (PropertyInfo property in data.GetType().GetProperties(publicAttributes))
        //    {
        //        if (property.CanRead)
        //            dictionary.Add(property.Name, property.GetValue(data, null));
        //    }

        //    return dictionary;
        //}

        //static IDictionary<string, object> GetDictionary(object obj)
        //{
        //    var type = obj.GetType();
        //    var props = type.GetProperties();
        //    return props.ToDictionary(x => x.Name.ToLower(), x => x.GetValue(obj, null));
        //}
        static string[] GetColumns(IDictionary<string, object> obj)
        {
            return obj.Select(x => x.Key).ToArray();
        }

        static void UpdateDefaultProperties(ref IDictionary<string, object> expando)
        {
            if (expando.ContainsKey("createdby"))
                expando.Remove("createdby");
            if (expando.ContainsKey("createddate"))
                expando.Remove("createddate");
            expando["modifiedby"] = "system"; 
            expando["modifieddate"] = DateTime.UtcNow;
        }
        static void InsertDefaultProperties(ref IDictionary<string, object> expando)
        {
            if (expando.ContainsKey("id"))
                expando.Remove("id");
            expando["createdby"] = "system";
            expando["modifiedby"] = "system";
            expando["createddate"] = DateTime.UtcNow;
            expando["modifieddate"] = DateTime.UtcNow;
            expando["isarchived"] = false;
        }

        //public static string LowerCaseSql(this SqlResult query)
        //{
        //    return query.Sql.ToLower();
        //}


        static string ToSqlString(this SqlSearchOperator contionalOperator)
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
            var fieldName = condition.Field.ToLower().Trim();
            switch (condition.Operator)
            {
                case SqlSearchOperator.Contains:
                    query.WhereContains(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.In:
                    if (condition.Value is IEnumerable<string> enumerableString)
                        query.WhereIn(fieldName, enumerableString);
                    else if (condition.Value is IEnumerable<int> enumerableInt)
                        query.WhereIn(fieldName, enumerableInt);
                    else if (condition.Value is IEnumerable<object> enumerableObject)
                        query.WhereIn(fieldName, enumerableObject);
                    else
                        query.Where(fieldName, SqlSearchOperator.Equal.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.StartsWith:
                    query.WhereStarts(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.EndsWith:
                    query.WhereEnds(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.Equal:
                case SqlSearchOperator.GreaterThan:
                case SqlSearchOperator.LessThan:
                case SqlSearchOperator.GreaterThanEqual:
                case SqlSearchOperator.LessThanEqual:
                    query.Where(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.NotEqual:
                    query.WhereNot(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;
            }
            return query;
        }

        /// <summary>
        /// This is a Sql Kata Compile method. It returns compiled sql statement based on the compiler set. Currently it is to Postgres
        /// </summary>
        /// <param name="query">Sql Kata Query Object</param>
        /// <returns>Sql Kata Result Object</returns>
        public static SqlResult Compile(this Query query)
        {
            var compiler = new PostgresCompiler();
            return compiler.Compile(query);
        }

        /// <summary>
        /// This method applies Sqlsearch filter and pagination to the sql kata select query. Use it only for search query
        /// </summary>
        /// <param name="query">Sql Kata Query Object</param>
        /// <param name="sqlSearch">Sql Kata Query Object</param>
        /// <returns></returns>
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
        /// <summary>
        /// This method converts key/value (alias/select field) pairs to a string array for usage in Sql kata select query
        /// </summary>
        /// <param name="keyValuePairs">Dictionary of alias/select fields</param>
        /// <returns>String array</returns>
        public static string[] GenerateFieldsWithAlias(this Dictionary<string, string> keyValuePairs)
        {
            return keyValuePairs.Select(x => string.Join(" as ", x.Value, x.Key)).ToArray();
        }
        /// <summary>
        /// This method can be used along with GenerateFieldsWithAlias method to pass alias object as search filter.
        /// This method is not necessary for select without alias
        /// </summary>
        /// <param name="sqlSearch">SqlSearch object to apply filter and pagination</param>
        /// <param name="aliases">Dictionary of alias/select fields</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method returns database table name
        /// </summary>
        /// <typeparam name="T">Model object</typeparam>
        /// <returns>string</returns>
        static string TableName<T>()
        {
            var schemaName = typeof(T).Namespace.Split('.')[1].ToLower();
            return schemaName + "." + typeof(T).Name.ToLower();
        }
        static string TableNameWOSchema<T>() => typeof(T).Name.ToLower(); 
        /// <summary>
        /// This method is an extension to Sql kata join to obtain tablename using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Query Join<T>(this Query query, Func<Join, Join> callback, string type = "inner join") =>
            query.Join(TableName<T>(), callback, type);
        /// <summary>
        /// This method is an extension to Sql kata join to obtain tablename using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="op"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Query Join<T>(this Query query, string first, string second, string op = "=", string type = "inner join") =>
            query.Join(TableName<T>(), first, second, op, type);
        /// <summary>
        /// This method is an extension to Sql kata join to obtain tablename using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        /// 
        public static Query Join<TLeft, TRight>(this Query query, Expression<Func<TLeft, object>> leftField,
            Expression<Func<TRight, object>> rightField, string op = "=")
        {
            return query.Join(TableName<TLeft>(), FieldName<TLeft>(leftField), FieldName<TRight>(rightField), op);
        }

        public static Query LeftJoin<T>(this Query query, Func<Join, Join> callback) =>
            query.LeftJoin(TableName<T>(), callback);
        /// <summary>
        /// This method is an extension to Sql kata join to obtain tablename using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public static Query LeftJoin<T>(this Query query, string first, string second, string op = "=") =>
            query.LeftJoin(TableName<T>(), first, second, op);
        /// <summary>
        /// 
        /// This method is an extension to Sql kata from to obtain tablename using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Query LeftJoin<TLeft, TRight>(this Query query, Expression<Func<TLeft, object>> leftField,
            Expression<Func<TRight, object>> rightField, string op = "=")
        {
            return query.LeftJoin(FieldName<TLeft>(leftField), FieldName<TRight>(rightField), op);
        }


        public static Join On<TLeft, TRight>(this Join join, Expression<Func<TLeft, object>> leftField,
            Expression<Func<TRight, object>> rightField, string op = "=")
        {
            return join.On(FieldName<TLeft>(leftField), FieldName<TRight>(rightField), op);
        }
        public static Query From<T>(this Query query) =>
            query.From(TableName<T>());

        /// <summary>
        /// This method is an extension to Sql kata update query with generic update fields added
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// 

        public static Query AsUpdateExt(this Query query, object obj)
        {
            IDictionary<string, object> expando = obj.ToDictionary();
            UpdateDefaultProperties(ref expando);
            return query.AsUpdate(new ReadOnlyDictionary<string, object>(expando));
        }
        /// <summary>
        /// This method is an extension to Sql kata insert query with generic insert fields added
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <param name="returnId"></param>
        /// <returns></returns>
        public static Query AsInsertExt(this Query query, object obj, bool returnId = false)
        {
            IDictionary<string, object> expando = obj.ToDictionary();
            InsertDefaultProperties(ref expando);
            return query.AsInsert(new ReadOnlyDictionary<string, object>(expando), returnId: returnId);
        }
        /// <summary>
        /// This method is an extension to Sql kata insert query to support bulk insert
        /// </summary>
        /// <param name="query"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static Query AsBulkInsertExt(this Query query, IEnumerable<object> objects)
        {
            if (objects.Count() == 0)
                throw new Exception("input objects is empty");
            List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            foreach (object record in objects)
            {
                IDictionary<string, object> expando = record.ToDictionary();
                InsertDefaultProperties(ref expando);
                list.Add(expando);
            }
            var firstObject = list.FirstOrDefault();
            var columns = GetColumns(firstObject);
            var data = list.Select(x => x.Values);
            return query.AsInsert(columns: columns, data);
        }
        /// <summary>
        /// This method is an extension to Sql kata insert query to support bulk insert
        ///  
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        public static Query AsBulkInsertExt(this Query query, object obj, Query selectQuery)
        {
            IDictionary<string, object> expando = obj.ToDictionary();
            InsertDefaultProperties(ref expando);
            var columns = GetColumns(expando);
            return query.AsInsert(columns: columns, selectQuery);
        }
        static string GetPropertyName(LambdaExpression propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            return GetPropertyName(ExpressionHelper.GetMemberExpression(propertyExpression));
        }

        static string GetPropertyName(MemberExpression memberExpression)
        {
            if (memberExpression == null) throw new ArgumentNullException(nameof(memberExpression));
            return memberExpression.Member.Name.ToLower();
        }

        public static Query Select<T>(this Query query, params Expression<Func<T, object>>[] fields)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));
            var selectfields = fields.Select(f => GetPropertyName(f)).ToArray(); 
            return query.Select(string.Format("{0}.{{{1}}}", TableNameWOSchema<T>(), string.Join(", ", selectfields))); 
        }

        public static Query Select<T>(this Query query)
        => query.Select(string.Format("{0}.*", TableNameWOSchema<T>()));



        //TODO: Match it with postgres datetime setting 
        const string DATE_FORMAT = "MM/dd/yyyy"; 
        
        public static Query WhereDateGreaterThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateGreaterThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} > ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateLessThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateLessThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} < ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} = ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateNotEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} != ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATE_FORMAT) });
        public static Query WhereDateBetween<T>(this Query query, Expression<Func<T, object>> column, DateTime startDate, DateTime endDate) =>
             query.Where(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), 
                 new[] { startDate.ToString(DATE_FORMAT) }).Where(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { endDate.ToString(DATE_FORMAT) });


        //TODO: Match it with postgres datetime setting
        const string DATETIME_FORMAT = "MM/dd/yyyy hh:mm:ss.fff tt";
        public static Query WhereDatetimeGreaterThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeGreaterThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} > ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeLessThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeLessThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} < ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} = ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeNotEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} != ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { value.ToString(DATETIME_FORMAT) });
        public static Query WhereDatetimeBetween<T>(this Query query, Expression<Func<T, object>> column, DateTime startDate, DateTime endDate) =>
           query.Where(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), 
               new[] { startDate.ToString(DATETIME_FORMAT) }).Where(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), GetPropertyName(column)), new[] { endDate.ToString(DATETIME_FORMAT) });

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

        public static Query WhereExt(this Query query, object obj)
        {
            IDictionary<string, object> expando = obj.ToDictionary(); 
            return query.Where(new ReadOnlyDictionary<string, object>(expando));
        }


        public static Query Where<T>(this Query query,Expression<Func<T, object>> fieldName, string op, object value)
        =>  query.Where(FieldName<T>(fieldName), op, value);
         
        public static Query Where<T>(this Query query, Expression<Func<T, object>> fieldName, object value)
        => query.Where(FieldName<T>(fieldName), value);
         
        public static Query WhereInExt<T>(this Query query, Expression<Func<T, object>> fieldName, Func<Query, Query> callback)
        => query.WhereIn(FieldName<T>(fieldName), callback);

        public static Query WhereIn<TModel,T>(this Query query, Expression<Func<TModel, object>> fieldName, IEnumerable<T> values)
        => query.WhereIn(FieldName<TModel>(fieldName), values);
    }
}
