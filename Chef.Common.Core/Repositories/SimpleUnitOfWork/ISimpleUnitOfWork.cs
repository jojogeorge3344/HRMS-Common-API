using System;

namespace Chef.Common.Repositories
{
    public interface ISimpleUnitOfWork
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