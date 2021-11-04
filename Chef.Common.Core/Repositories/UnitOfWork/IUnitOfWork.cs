using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Chef.Common.Repositories
{
    interface IUnitOfWork : IDisposable
    {
        IsolationLevel IsolationLevel { get; set; }
        TransactionState TransactionState { get; }

        Guid Start(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        void Rollback(string message = null);

        void Complete(Guid transactionId);
    }
}
