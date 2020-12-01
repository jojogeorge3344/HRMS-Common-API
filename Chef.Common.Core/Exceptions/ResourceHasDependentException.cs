using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class ResourceHasDependentException : Exception
    {
        public ResourceHasDependentException()
        {
        }

        public ResourceHasDependentException(string message) : base(message)
        {
        }

        public ResourceHasDependentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceHasDependentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}