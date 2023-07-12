using System;
using System.Data;
using System.Runtime.InteropServices;

namespace Chef.Common.Repositories;

internal class UnitOfWorkSession : IUnitOfWorkSession
{
    private readonly IUnitOfWork unitOfWork;
    private bool disposedValue;

    public Guid TransactionId { get; private set; } = Guid.Empty;

    public UnitOfWorkSession(IUnitOfWork unitOfWork, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        this.unitOfWork = unitOfWork;
        this.unitOfWork.IsolationLevel = isolationLevel;
        //this.isolationLevel = isolationLevel;
        Start(isolationLevel);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (unitOfWork.TransactionState == TransactionState.Started)
                {
                    if (Marshal.GetExceptionPointers() != IntPtr.Zero)
                    {
                        unitOfWork.Rollback();
                    }
                    else
                    {
                        unitOfWork.Complete(TransactionId);
                    }
                }
            }
            disposedValue = true;
        }
    }

    ~UnitOfWorkSession()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public void Start(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (unitOfWork.TransactionState != TransactionState.Started)
        {
            TransactionId = unitOfWork.Start(isolationLevel);
        }
    }

    public void Complete()
    {
        if (unitOfWork.TransactionState == TransactionState.Started)
        {
            unitOfWork.Complete(TransactionId);
        }
    }

    //public void Rollback(string message = null)
    //{
    //    if (this.unitOfWork.TransactionState == TransactionState.Started)
    //        unitOfWork.Rollback(message);
    //}
}
