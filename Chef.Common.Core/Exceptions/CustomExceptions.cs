﻿using System;
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
    public class DuplicateCurrencyException : ApplicationException
    {
        public DuplicateCurrencyException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class BusinessPartnerNotFoundException : ApplicationException
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
    public class CountryNotFoundException : ApplicationException
    {
        public CountryNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class CountryCannotBeDeletedException : ApplicationException
    {
        public CountryCannotBeDeletedException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class StateCannotBeDeletedException : ApplicationException
    {
        public StateCannotBeDeletedException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class StateNotFoundException : ApplicationException
    {
        public StateNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class CityNotFoundException : ApplicationException
    {
        public CityNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class CityCannotBeDeletedException : ApplicationException
    {
        public CityCannotBeDeletedException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class FinancialYearNotFoundException : ApplicationException
    {
        public FinancialYearNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class FinancialYearPeriodDuplicateException : Exception
    {
        public FinancialYearPeriodDuplicateException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class TaxJurisdictionCannotBeDeletedException : ApplicationException
    {
        public TaxJurisdictionCannotBeDeletedException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class DepartmentNotFoundException : ApplicationException
    {
        public DepartmentNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class DesignationNotFoundException : ApplicationException
    {
        public DesignationNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class EmployeeNotFoundException : ApplicationException
    {
        public EmployeeNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class JournalBookTypeNotFoundException : ApplicationException
    {
        public JournalBookTypeNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class LanguageNotFoundException : ApplicationException
    {
        public LanguageNotFoundException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class LocationNotFoundException : ApplicationException
    {
        public LocationNotFoundException(string message) : base(message)
        {
        }
    }
}