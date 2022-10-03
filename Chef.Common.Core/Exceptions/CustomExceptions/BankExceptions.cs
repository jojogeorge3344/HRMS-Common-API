using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class BankNotFoundException : ApplicationException
    {
        public BankNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class BankBranchNotFoundException : ApplicationException
    {
        public BankBranchNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class BankCannotBeDeletedException : ApplicationException
    {
        public BankCannotBeDeletedException(string message) : base(message)
        {
        }
    }
}