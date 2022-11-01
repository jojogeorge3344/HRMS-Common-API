using Chef.Common.Repositories;
using SqlKata;
using SqlKata.Execution;

namespace Chef.Common.Core
{
    public static class SqlKataExtensionMethods
    {
        public static Query Query<T>(this QueryFactory qf)
        {
            //schema.tablename
            return qf.Query(typeof(T).Namespace.Split('.')[1].ToLower() + "." + typeof(T).Name.ToLower());
        }

        public static Query Join<R, T>(this Query q)
        {
            var rname = typeof(R).Name.ToLower();
            var tname = typeof(T).Name.ToLower();

            return q.Join($"{tname}", $"{rname}.Id", $"{tname}.{rname}Id");
        }

        public static Query JoinChild<R, T>(this Query q)
        {
            var rschema = typeof(R).Namespace.Split('.')[1].ToLower();
            var tschema = typeof(T).Namespace.Split('.')[1].ToLower();

            var rname = typeof(R).Name.ToLower();
            var tname = typeof(T).Name.ToLower();

            return q.Join($"{tschema}.{tname}",
                $"{tschema}.{tname}.Id",
                $"{rschema}.{rname}.{tname}.Id");
        }

        public static Query Where<T>(this Query q, string field, object val)
        {
            var schema = typeof(T).Namespace.Split('.')[1].ToLower();
            return q.Where($"{schema}.{typeof(T).Name.ToLower()}.{field}", val);
        }

        public static void AddArchiveFilter(this SqlSearch search)
        {
            search.Rules.Add(
                new SqlSearchRule()
                {
                    Field = "isarchived",
                    Operator = SqlSearchOperator.Equal,
                    Value = false
                });
        }

        public static void AddActiveFilter(this SqlSearch search)
        {
            search.Rules.Add(
                new SqlSearchRule()
                {
                    Field = "isactive",
                    Operator = SqlSearchOperator.Equal,
                    Value = false
                });
        }

        public static void AddBranchFiller(this SqlSearch search, int branchId)
        {
            search.Rules.Add(
                 new SqlSearchRule()
                 {
                     Field = "branchid",
                     Operator = SqlSearchOperator.Equal,
                     Value = branchId
                 });
        }

    }
}

