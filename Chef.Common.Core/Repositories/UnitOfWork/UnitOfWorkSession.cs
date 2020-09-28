using System;
using System.Data;
using System.Runtime.InteropServices;

namespace Chef.Common.Repositories
{
    internal class UnitOfWorkSession : IUnitOfWorkSession
    {
        readonly IUnitOfWork unitOfWork;
        private bool disposedValue;
        //readonly IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        Guid transactionId = Guid.Empty;
        public Guid TransactionId => transactionId;

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
                    if (this.unitOfWork.TransactionState == TransactionState.Started)
                        if (Marshal.GetExceptionPointers() != IntPtr.Zero)
                            unitOfWork.Rollback();
                        else
                            unitOfWork.Complete(transactionId);
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
            if (this.unitOfWork.TransactionState != TransactionState.Started)
                transactionId = unitOfWork.Start(isolationLevel);
        }

        public void Complete()
        {
            if (this.unitOfWork.TransactionState == TransactionState.Started)
                unitOfWork.Complete(transactionId);
        }

        //public void Rollback(string message = null)
        //{
        //    if (this.unitOfWork.TransactionState == TransactionState.Started)
        //        unitOfWork.Rollback(message);
        //}
    }
}
