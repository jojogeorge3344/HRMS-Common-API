using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Chef.Common.Repositories
{
    public static class SqlKataExtension
    {
        public static string FieldName<T>(Expression<Func<T, object>> fieldName)
            => string.Format("{0}.{1}", TableNameWOSchema<T>(), GetPropertyName(fieldName));

        internal static string[] GetColumns(IDictionary<string, object> obj)
        {
            return obj.Select(x => x.Key).ToArray();
        }

        /// <summary>
        /// This method is an extension to Sql kata update query with generic update fields added
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// 

        internal static Query AsUpdateExt(this Query query, IDictionary<string, object> dictionary)
        {
            //IDictionary<string, object> expando = obj.ToDictionary();
            //UpdateDefaultProperties(ref expando);
            return query.AsUpdate(new ReadOnlyDictionary<string, object>(dictionary));
        }

        /// <summary>
        /// This method is an extension to Sql kata insert query with generic insert fields added
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <param name="returnId"></param>
        /// <returns></returns>
        internal static Query AsInsertExt(this Query query, IDictionary<string, object> dictionary, bool returnId = false)
        {
            //IDictionary<string, object> expando = obj.ToDictionary();
            //InsertDefaultProperties(ref expando);
            return query.AsInsert(new ReadOnlyDictionary<string, object>(dictionary), returnId: returnId);
        }

        /// <summary>
        /// This method is an extension to Sql kata insert query to support bulk insert
        /// </summary>
        /// <param name="query"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        internal static Query AsBulkInsertExt(this Query query, IEnumerable<IDictionary<string, object>> dictionaries)
        {
            //if (objects.Count() == 0)
            //    throw new Exception("input objects is empty");
            //List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            //foreach (object record in objects)
            //{
            //    IDictionary<string, object> expando = record.ToDictionary();
            //    InsertDefaultProperties(ref expando);
            //    list.Add(expando);
            //}
            IDictionary<string, object> firstObject = dictionaries.FirstOrDefault();
            string[] columns = GetColumns(firstObject);
            IEnumerable<ICollection<object>> data = dictionaries.Select(x => x.Values);

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
        internal static Query AsBulkInsertExt(this Query query, IDictionary<string, object> dictionary, Query selectQuery)
        {
            //IDictionary<string, object> expando = obj.ToDictionary();
            //InsertDefaultProperties(ref expando);
            string[] columns = GetColumns(dictionary);
            return query.AsInsert(columns: columns, selectQuery);
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

        public static string GetFieldName(this Query query, SqlSearchRule condition)
        {
            if (query.Variables.ContainsKey("Aliases"))
            {
                Dictionary<string, string> dictionary = query.Variables["Aliases"] as Dictionary<string, string>;
                return dictionary.ContainsKey(condition.Field.ToLower().Trim())
                    ? dictionary[condition.Field.ToLower().Trim()]
                    : condition.Field.ToLower().Trim();
            }

            return condition.Field.ToLower().Trim();
        }

        public static Query AndSearchCondition(this Query query, SqlSearchRule condition)
        {
            string fieldName = query.GetFieldName(condition);

            switch (condition.Operator)
            {
                case SqlSearchOperator.Contains:
                    query.WhereContains(fieldName, condition.Value);
                    break;

                case SqlSearchOperator.In:
                    if (condition.Value is IEnumerable<string> enumerableString)
                    {
                        query.WhereIn(fieldName, enumerableString);
                    }
                    else if (condition.Value is IEnumerable<int> enumerableInt)
                    {
                        query.WhereIn(fieldName, enumerableInt);
                    }
                    else if (condition.Value is IEnumerable<object> enumerableObject)
                    {
                        query.WhereIn(fieldName, enumerableObject);
                    }
                    else
                    {
                        query.Where(fieldName, SqlSearchOperator.Equal.ToSqlString(), condition.Value);
                    }

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

                    if (condition.FieldType == "date")
                    {
                        //fieldName = "to_char(" + fieldName + ", 'yyyy-MM-dd')";
                        query.WhereDate(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    }
                    else
                    {
                        query.Where(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    }

                    //query.Where(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;

                case SqlSearchOperator.NotEqual:
                    query.WhereNot(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;

                case SqlSearchOperator.IsNull:
                    query.WhereNull(fieldName);
                    break;

                case SqlSearchOperator.IsNotNull:
                    query.WhereNotNull(fieldName);
                    break;
            }

            return query;
        }

        public static Query OrSearchCondition(this Query query, SqlSearchRule condition)
        {
            string fieldName = query.GetFieldName(condition);

            switch (condition.Operator)
            {
                case SqlSearchOperator.Contains:
                    query.OrWhereContains(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.In:
                    if (condition.Value is IEnumerable<string> enumerableString)
                    {
                        query.OrWhereIn(fieldName, enumerableString);
                    }
                    else if (condition.Value is IEnumerable<int> enumerableInt)
                    {
                        query.OrWhereIn(fieldName, enumerableInt);
                    }
                    else if (condition.Value is IEnumerable<object> enumerableObject)
                    {
                        query.OrWhereIn(fieldName, enumerableObject);
                    }
                    else
                    {
                        query.OrWhere(fieldName, SqlSearchOperator.Equal.ToSqlString(), condition.Value);
                    }

                    break;
                case SqlSearchOperator.StartsWith:
                    query.OrWhereStarts(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.EndsWith:
                    query.OrWhereEnds(fieldName, condition.Value);
                    break;
                case SqlSearchOperator.Equal:
                case SqlSearchOperator.GreaterThan:
                case SqlSearchOperator.LessThan:
                case SqlSearchOperator.GreaterThanEqual:
                case SqlSearchOperator.LessThanEqual:
                    query.OrWhere(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.NotEqual:
                    query.OrWhereNot(fieldName, condition.Operator.ToSqlString(), condition.Value);
                    break;
                case SqlSearchOperator.IsNull:
                    query.WhereNull(fieldName);
                    break;
                case SqlSearchOperator.IsNotNull:
                    query.WhereNotNull(fieldName);
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
            PostgresCompiler compiler = new();

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
            {
                return query;
            }

            query.ApplySqlSearchRules(sqlSearch.Condition, sqlSearch.Rules);
            query.ApplySqlSearchGroups(sqlSearch.Groups);

            if (sqlSearch.Page != null)
            {
                query.ForPage(sqlSearch.Page.PageNo, sqlSearch.Page.PageLimit);
            }
            else if (sqlSearch.Limit.HasValue)
            {
                query.Limit(sqlSearch.Limit.Value);
            }

            if (sqlSearch.Offset.HasValue)
            {
                query.Offset(sqlSearch.Offset.Value);
            }

            return query;
        }

        public static Query ApplySqlSearchGroups(this Query query,
            IEnumerable<SqlSearchGroup> groups)
        {
            foreach (SqlSearchGroup group in groups)
            {
                Query queryGroup = new();

                ApplySqlSearchRules(queryGroup, group.Condition, group.Rules);

                if (group.Condition == SqlConditionOperator.AND)
                {
                    query.Where(q => queryGroup);
                }
                else
                {
                    query.OrWhere(q => queryGroup);
                }

                if (group.Groups.Count > 0)
                {
                    ApplySqlSearchGroups(queryGroup, group.Groups);
                }
            }

            return query;
        }

        public static Query ApplyRule(this Query query, SqlConditionOperator sqlConditionOperator,
         SqlSearchRule sqlSearchRule)
        {
            if (sqlConditionOperator == SqlConditionOperator.AND)
            {
                query.AndSearchCondition(sqlSearchRule);
            }
            else
            {
                query.OrSearchCondition(sqlSearchRule);
            }

            return query;
        }

        public static Query ApplySqlSearchRules(this Query query, SqlConditionOperator sqlConditionOperator,
            IEnumerable<SqlSearchRule> rules)
        {
            if (!rules.Any())
            {
                return query;
            }

            SqlSearchRule first = rules.First();
            if (first is SqlSearchRule rule)
            {
                if (rules.Count() == 1)
                {
                    return query.ApplyRule(sqlConditionOperator, rules.Single());
                }

                SqlSearchRule last = rules.Last();
                IEnumerable<SqlSearchRule> others = rules.Skip(1).Take(rules.Count() - 2);

                query.ApplyRule(sqlConditionOperator, rule);

                foreach (SqlSearchRule c in others)
                {
                    query.ApplyRule(sqlConditionOperator, c);
                }

                query.ApplyRule(sqlConditionOperator, last);
            }

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
        /// This method returns database table name
        /// </summary>
        /// <typeparam name="T">Model object</typeparam>
        /// <returns>string</returns>
        private static string TableName<T>()
        {
            Type type = typeof(T);
            string schemaName = type.Namespace.Split('.')[1].ToLower();

            return schemaName + "." + type.Name.ToLower();
        }

        private static string TableNameWOSchema<T>() => typeof(T).Name.ToLower();

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
            return query.LeftJoin(TableName<TLeft>(), FieldName<TLeft>(leftField), FieldName<TRight>(rightField), op);
        }

        public static Join On<TLeft, TRight>(this Join join, Expression<Func<TLeft, object>> leftField,
            Expression<Func<TRight, object>> rightField, string op = "=")
        {
            return join.On(FieldName<TLeft>(leftField), FieldName<TRight>(rightField), op);
        }

        public static Query From<T>(this Query query) =>
            query.From(TableName<T>());

        private static string GetPropertyName(LambdaExpression propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            return GetPropertyName(ExpressionHelper.GetMemberExpression(propertyExpression));
        }

        private static string GetPropertyName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException(nameof(memberExpression));
            }

            return memberExpression.Member.Name.ToLower();
        }

        private static IEnumerable<string> GetPropertyNames(NewExpression newExpression)
        {
            if (newExpression == null)
            {
                throw new ArgumentNullException(nameof(newExpression));
            }

            IEnumerable<MemberExpression> expressions = ExpressionHelper.GetMemberExpressions(newExpression);
            string[] aliases = ExpressionHelper.GetMemberNames(newExpression).ToArray();

            return expressions.Select((x, i) => string.Format("{0}{1}", x.Member.Name.ToLower(), (x.Member.Name.ToLower() == aliases[i].ToLower()) ? "" : " as " + aliases[i]));
        }

        private static IEnumerable<string> GetAliases(NewExpression newExpression)
        {
            return ExpressionHelper.GetMemberNames(newExpression).ToArray();
        }

        private static IEnumerable<string> NewExpressionToSelectFieldsWithAlias<T>(this Query query, Expression<Func<T, object>>[] fields)
        {
            IEnumerable<string> selectfields;
            string tableNameWOSchema = TableNameWOSchema<T>();
            // New Expression
            NewExpression newExpression = fields.First().Body as NewExpression;
            IEnumerable<MemberExpression> expressions = ExpressionHelper.GetMemberExpressions(newExpression);
            string[] aliases = ExpressionHelper.GetMemberNames(newExpression).ToArray();
            Dictionary<string, string> keyValuePairs = expressions.Select((x, index) => new { Key = aliases[index].ToLower(), Value = x.Member.Name.ToLower() })
                .Where(x => x.Key != x.Value)
                 .ToDictionary(x => x.Key, x => tableNameWOSchema + "." + x.Value);

            if (keyValuePairs.Count > 0)
            {
                if (query.Variables.ContainsKey("Aliases"))
                {
                    Dictionary<string, string> dictionary = query.Variables["Aliases"] as Dictionary<string, string>;

                    foreach (KeyValuePair<string, string> keyValue in keyValuePairs)
                    {
                        dictionary[keyValue.Key] = keyValue.Value;
                    }

                    query.Variables["Aliases"] = dictionary;
                }
                else
                {
                    query.Variables["Aliases"] = keyValuePairs;
                }
            }

            selectfields = expressions.Select((x, i) => string.Format("{0}{1}", x.Member.Name.ToLower(), (x.Member.Name.ToLower() == aliases[i].ToLower()) ? "" : " as " + aliases[i]));

            return selectfields;
        }

        private static IEnumerable<string> NewExpressionToSelectFieldsWithoutAlias<T>(this Query query, Expression<Func<T, object>>[] fields)
        {
            IEnumerable<string> selectfields;
            string tableNameWOSchema = TableNameWOSchema<T>();
            // New Expression
            NewExpression newExpression = fields.First().Body as NewExpression;
            IEnumerable<MemberExpression> expressions = ExpressionHelper.GetMemberExpressions(newExpression);

            selectfields = expressions.Select((x) => x.Member.Name.ToLower());

            return selectfields;
        }

        public static Query Select<T>(this Query query, params Expression<Func<T, object>>[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            string tableNameWOSchema = TableNameWOSchema<T>();
            IEnumerable<string> selectfields;

            if (fields.Count() == 1 && fields.First().Body is NewExpression)
            {
                selectfields = NewExpressionToSelectFieldsWithAlias<T>(query, fields);
            }
            else
            {
                selectfields = fields.Select(f => GetPropertyName(f)).ToArray();
            }

            return query.Select(string.Format("{0}.{{{1}}}", tableNameWOSchema, string.Join(", ", selectfields)));
        }

        public static Query Select<T>(this Query query)
        => query.Select(string.Format("{0}.*", TableNameWOSchema<T>()));

        //TODO: Match it with postgres datetime setting 
        private const string DATE_FORMAT = "MM/dd/yyyy";

        public static Query WhereDateGreaterThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateGreaterThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} > ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateLessThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateLessThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} < ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} = ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateNotEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} != ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATE_FORMAT) });

        public static Query WhereDateBetween<T>(this Query query, Expression<Func<T, object>> column, DateTime startDate, DateTime endDate) =>
             query.Where(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()),
                 new[] { startDate.ToString(DATE_FORMAT) }).Where(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { endDate.ToString(DATE_FORMAT) });

        //TODO: Match it with postgres datetime setting
        private const string DATETIME_FORMAT = "MM/dd/yyyy hh:mm:ss.fff tt";

        public static Query WhereDatetimeGreaterThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeGreaterThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} > ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeLessThanOrEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeLessThan<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} < ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} = ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeNotEqual<T>(this Query query, Expression<Func<T, object>> column, DateTime value) =>
            query.WhereRaw(string.Format("{0}.{1} != ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { value.ToString(DATETIME_FORMAT) });

        public static Query WhereDatetimeBetween<T>(this Query query, Expression<Func<T, object>> column, DateTime startDate, DateTime endDate) =>
           query.Where(string.Format("{0}.{1} >= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()),
               new[] { startDate.ToString(DATETIME_FORMAT) }).Where(string.Format("{0}.{1} <= ?::timestamp", TableNameWOSchema<T>(), column.GetPropertyName()), new[] { endDate.ToString(DATETIME_FORMAT) });

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

        public static Query WhereExt(this Query query, IDictionary<string, object> expando)
        => query.Where(new ReadOnlyDictionary<string, object>(expando));

        public static Query Where<T>(this Query query, Expression<Func<T, object>> fieldName, string op, object value)
        => query.Where(FieldName<T>(fieldName), op, value);

        public static Query Where<T>(this Query query, Expression<Func<T, object>> fieldName, object value)
        => query.Where(FieldName<T>(fieldName), value);
        public static Query WhereInExt<T>(this Query query, Expression<Func<T, object>> fieldName, Func<Query, Query> callback)
        => query.WhereIn(FieldName<T>(fieldName), callback);

        public static Query WhereIn<TModel, T>(this Query query, Expression<Func<TModel, object>> fieldName, IEnumerable<T> values)
        => query.WhereIn(FieldName<TModel>(fieldName), values);

        public static Query WhereColumns<TFirst, TSecond>(this Query query, Expression<Func<TFirst, object>> first, string op, Expression<Func<TSecond, object>> second)
        => query.WhereColumns(FieldName<TFirst>(first), op, FieldName<TSecond>(second));

        public static Query WhereColumns<TFirst, TSecond>(this Query query, Expression<Func<TFirst, object>> first, Expression<Func<TSecond, object>> second)
        => query.WhereColumns(FieldName<TFirst>(first), "=", FieldName<TSecond>(second));

        public static Query WhereNotNull<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.WhereNotNull(FieldName<T>(fieldName));

        public static Query OrWhereNotNull<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.OrWhereNotNull(FieldName<T>(fieldName));

        public static Query WhereNull<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.WhereNull(FieldName<T>(fieldName));

        public static Query OrWhereNull<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.OrWhereNull(FieldName<T>(fieldName));

        public static Join Where<T>(this Join join, Expression<Func<T, object>> fieldName, string op, object value)
        => join.Where(FieldName<T>(fieldName), op, value);

        public static Join Where<T>(this Join join, Expression<Func<T, object>> fieldName, object value)
        => join.Where(FieldName<T>(fieldName), "=", value);

        public static Query OrWhere<T>(this Query query, Expression<Func<T, object>> fieldName, object value)
        => query.OrWhere(FieldName<T>(fieldName), value);

        public static Query AsMax<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.SelectRaw($"MAX ({FieldName<T>(fieldName)}) as \"{GetPropertyName(fieldName)}\"");

        public static Query AsMin<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.SelectRaw($"MIN ({FieldName<T>(fieldName)}) as \"{GetPropertyName(fieldName)}\"");

        public static Query AsSum<T>(this Query query, Expression<Func<T, object>> fieldName)
        => query.SelectRaw($"SUM ({FieldName<T>(fieldName)}) as \"{GetPropertyName(fieldName)}\"");

        public static Query GroupBy<T>(this Query query, params Expression<Func<T, object>>[] fields)
        {
            string tableNameWOSchema = TableNameWOSchema<T>();
            IEnumerable<string> selectfields;

            if (fields.Count() == 1 && fields.First().Body is NewExpression)
            {
                selectfields = NewExpressionToSelectFieldsWithoutAlias<T>(query, fields);
            }
            else
            {
                selectfields = fields.Select(f => GetPropertyName(f)).ToArray();
            }

            return query.GroupBy(selectfields.Select(x => string.Format("{0}.{1}", tableNameWOSchema, x)).ToArray());
            //return query.GroupBy(string.Format("{0}.{{{1}}}", tableNameWOSchema, string.Join(", ", selectfields)));
        }

        public static Query WhereGreaterThan<T>(this Query query, Expression<Func<T, object>> fieldName, object value)
         => query.Where(FieldName<T>(fieldName), ">", value);
    }
}