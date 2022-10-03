using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class CurrencyNotFoundException : ApplicationException
    {
        public CurrencyNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class CurrencyExchangeNotFoundException : ApplicationException
    {
        public CurrencyExchangeNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class DuplicateCurrencyException : Exception
    {
        public DuplicateCurrencyException(string message) : base(message)
        {
        }
    }
}