using System;
using System.Data.Common;

namespace Chef.Common.Repositories;

public abstract class SimpleUnitOfWork : ISimpleUnitOfWork, IDisposable
{
    private readonly IConnectionFactory connectionFactory;
    private bool disposed = false;

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
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                connectionFactory.Transaction?.Dispose();
            }

            disposed = true;
        }
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