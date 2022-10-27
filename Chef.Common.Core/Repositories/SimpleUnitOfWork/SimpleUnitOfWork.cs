namespace Chef.Common.Repositories
{
    public sealed class SimpleUnitOfWork : ISimpleUnitOfWork
    {
        private readonly IConnectionFactory connectionFactory;

        public SimpleUnitOfWork(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void BeginTransaction()
        {
            connectionFactory.Transaction = connectionFactory.Connection.BeginTransaction();
        }

        public void Commit()
        {
            connectionFactory.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            connectionFactory.Transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            connectionFactory.Transaction?.Dispose();
        }
    }
}