using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class BankCannotBeDeletedException : Exception
    {
        public BankCannotBeDeletedException()
        {
        }

        public BankCannotBeDeletedException(string message) : base(message)
        {
        }

        public BankCannotBeDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}