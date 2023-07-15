using Chef.Common.Core;

namespace Chef.Common.Repositories;

public class QueryBuilderFactory : IQueryBuilderFactory
{
    public IQueryBuilder<T> QueryBuilder<T>()
        where T : IModel
    {
        return new QueryBuilder<T>();
    }

    public ISqlQueryBuilder SqlQueryBuilder()
    {
        return new SqlQueryBuilder();
    }
}
