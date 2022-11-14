namespace Chef.Common.Repositories
{
    public abstract class SimpleUnitOfWork : ISimpleUnitOfWork
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

    public sealed class ConsoleSimpleUnitOfWork : SimpleUnitOfWork, IConsoleSimpleUnitOfWork
    {
        public ConsoleSimpleUnitOfWork(IConsoleConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }

    public sealed class TenantSimpleUnitOfWork : SimpleUnitOfWork, ITenantSimpleUnitOfWork
    {
        public TenantSimpleUnitOfWork(ITenantConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }

}