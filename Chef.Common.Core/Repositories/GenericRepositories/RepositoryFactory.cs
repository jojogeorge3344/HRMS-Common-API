using Chef.Common.Core;

namespace Chef.Common.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IDatabaseSession databaseSession;
        private readonly ISqlQueryBuilder sqlqueryBuilder;

        public RepositoryFactory(IDatabaseSession databaseSession)
        {
            this.databaseSession = databaseSession;
            sqlqueryBuilder = new SqlQueryBuilder();
        }

        public ICommonRepository<TModel> GenericRepository<TModel>()
           where TModel : IModel
        {
            return new CommonRepository<TModel>(databaseSession, sqlqueryBuilder, new QueryBuilder<TModel>());
        }
    }
}
