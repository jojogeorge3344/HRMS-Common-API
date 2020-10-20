using Chef.Common.Core;
using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class ResourceAlreadyExistsException<T> : Exception
        where T : Model
    {
        public ResourceAlreadyExistsException()
        {
        }

        public ResourceAlreadyExistsException(string message) : base(message)
        {
        }

        public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}