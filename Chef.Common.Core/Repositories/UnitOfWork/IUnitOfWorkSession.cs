using System;
using System.Data;

namespace Chef.Common.Repositories;

public interface IUnitOfWorkSession : IDisposable
{
    public Guid TransactionId { get; }
    void Start(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    ///void Rollback(string message = null);
    void Complete();
}
