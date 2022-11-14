using System;

namespace Chef.Common.Repositories
{
    public interface ISimpleUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }

    public interface IConsoleSimpleUnitOfWork : ISimpleUnitOfWork
    {
    }

    public interface ITenantSimpleUnitOfWork : ISimpleUnitOfWork
    {
    }
}