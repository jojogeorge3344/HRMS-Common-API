using Chef.Common.Core;
using System.Data;

namespace Chef.Common.Repositories;

/// <summary>
/// TODO: Depreciated. Eventually will be removed.
/// </summary>
public abstract class BaseRepository : IBaseRepository
{
    public IRepositoryFactory RepositoryFactory { get; set; }
    public ICommonRepository<TModel> GenericRepository<TModel>() where TModel : Model
        => RepositoryFactory.GenericRepository<TModel>();
    public IDatabaseSession DatabaseSession { get; set; }
    public IQueryBuilderFactory QueryBuilderFactory { get; set; }
    public ISqlQueryBuilder SqlQueryBuilder => QueryBuilderFactory.SqlQueryBuilder();
    public IUnitOfWorkSession UnitOfWorkSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => DatabaseSession.UnitOfWorkSession(isolationLevel);
}
