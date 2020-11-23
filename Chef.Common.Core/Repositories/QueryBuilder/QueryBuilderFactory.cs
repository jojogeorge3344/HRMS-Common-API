using Chef.Common.Core;

namespace Chef.Common.Repositories
{
    public class QueryBuilderFactory : IQueryBuilderFactory
    {
        public IQueryBuilder<T> QueryBuilder<T>()
            where T : IModel =>
            new QueryBuilder<T>();

        public ISqlQueryBuilder SqlQueryBuilder() =>
            new SqlQueryBuilder();
    }
}
