using Chef.Common.Core;

namespace Chef.Common.Repositories;

public interface IQueryBuilderFactory
{
    /// <summary>
    /// This is a QueryBuilder object
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// <returns>IQueryBuilder</returns>
    IQueryBuilder<T> QueryBuilder<T>()
       where T : IModel;

    /// <summary>
    /// This is a SqlQueryBuilder object uses SqlKata QueryBuilder under the hood
    /// </summary>
    /// <returns>ISqlQueryBuilder</returns>
    ISqlQueryBuilder SqlQueryBuilder();
}
