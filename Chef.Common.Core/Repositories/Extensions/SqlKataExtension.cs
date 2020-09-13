using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
