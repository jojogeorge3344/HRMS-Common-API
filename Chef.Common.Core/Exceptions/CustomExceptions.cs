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

    [Serializable]
    public class BusinessPartnerNotFoundException : Exception
    {
        public BusinessPartnerNotFoundException(string message) : base(message)
        {
        }
    }

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

    [Serializable]
    public class CountryCannotBeDeletedException : Exception
    {
        public CountryCannotBeDeletedException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class FinancialYearNotFoundException : Exception
    {
        public FinancialYearNotFoundException(string message) : base(message)
        {
        }
    }
}