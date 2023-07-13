using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Chef.Common.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private Guid transactionIdentifier = Guid.Empty;
    private Guid uniqueIdentifier = Guid.Empty;
    private bool disposed;

    public TransactionState TransactionState { get; private set; } = TransactionState.Initialized;

    public IDbTransaction Transaction { get; private set; }
    public IDbConnection Connection { get; private set; }

    public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
    public UnitOfWork(IDbConnection connection)
    {
        Connection = connection;
    }


    #region Internal Functions 
    ~UnitOfWork()
      => Dispose(false);

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }

            Transaction?.Dispose();
            Transaction = null;
        }
        Transaction = null;
        disposed = true;
    }
    #endregion

    public Guid Start(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        uniqueIdentifier = Guid.NewGuid();
        // Register only the first (Origin) transaction
        if (Transaction == null)
        {
            Connection.Open();
            Transaction = Connection.BeginTransaction(isolationLevel); TransactionState = TransactionState.Started;
            transactionIdentifier = uniqueIdentifier;
        }
        return uniqueIdentifier;
    }

    public void Rollback(string message = null)
    {
        if (Transaction == null)
        {
            throw new Exception("No active transaction to rollback, transaction object is null.Start a new transaction with start method");
        }

        try
        {
            Transaction?.Rollback();
            TransactionState = TransactionState.Rollbacked;
            //message = string.IsNullOrEmpty(message) ? string.Format("Transaction - {0} rollbacked", transactionIdentifier) : message;
            //throw new Exception(message);
        }
        catch
        {
            throw;
        }
        finally
        {
            Dispose(true);
        }
    }

    public void Complete(Guid transactionId)
    {
        //TODO: Log transaction record failed or succeeded with transaction id
        // include membername to identify each calls
        if (Transaction == null)
        {
            throw new Exception("No active transaction to submit, transaction object is null.Start a new transaction with start method");
        }
        // Commit or rollback only for the origin transaction. Other transaction initiators are ignored
        if (transactionId == transactionIdentifier)
        {
            try
            {
                //TODO: Apply Retry Logic based on database error codes 
                Transaction?.Commit();
                TransactionState = TransactionState.Succeeded;

            }
            catch
            {
                Transaction?.Rollback();
                TransactionState = TransactionState.Rollbacked;
                throw;
            }
            finally
            {
                Dispose(true);
            }
        }
    }


    public void Dispose()
    {
        //This object is disposed only at the final Submit call 
    }
}

internal enum TransactionState
{
    Initialized = 0,
    Started = 1,
    Succeeded = 2,
    Rollbacked = 3

}
