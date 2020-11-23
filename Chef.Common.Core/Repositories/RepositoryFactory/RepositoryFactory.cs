using Chef.Common.Core;

namespace Chef.Common.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        readonly IDatabaseSession databaseSession;
        readonly ISqlQueryBuilder sqlqueryBuilder;
        public RepositoryFactory(IDatabaseSession databaseSession)
        {
            this.databaseSession = databaseSession;
            this.sqlqueryBuilder = new SqlQueryBuilder();
        }

        public ICommonRepository<TModel> GenericRepository<TModel>()
           where TModel : IModel =>
        new CommonRepository<TModel>(this.databaseSession, this.sqlqueryBuilder, new QueryBuilder<TModel>());
    }
}
