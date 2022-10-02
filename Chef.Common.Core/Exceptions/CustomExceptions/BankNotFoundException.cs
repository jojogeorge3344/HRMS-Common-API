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
}