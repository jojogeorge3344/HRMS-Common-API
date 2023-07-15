using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
public class TenantNotFoundException : ApplicationException
{
    public TenantNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class CompanyNotFoundException : ApplicationException
{
    public CompanyNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class ResourceNotFoundException : ApplicationException
{
    public ResourceNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class ResourceHasDependentException : ApplicationException
{
    public ResourceHasDependentException(string message) : base(message)
    {
    }
}

[Serializable]
public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException(string message) : base(message)
    {
    }
}

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
public class BusinessPartnerDocumentsNotFoundException : ApplicationException
{
    public BusinessPartnerDocumentsNotFoundException(string message) : base(message)
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
public class DuplicateTaxJurisdicationException : ApplicationException
{
    public DuplicateTaxJurisdicationException(string message) : base(message)
    {
    }
}

[Serializable]
public class DuplicateTaxNameException : ApplicationException
{
    public DuplicateTaxNameException(string message) : base(message)
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
public class DocumentTypeNotFoundException : ApplicationException
{
    public DocumentTypeNotFoundException(string message) : base(message)
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

[Serializable]
public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class DuplicateUserException : ApplicationException
{
    public DuplicateUserException(string message) : base(message)
    {
    }
}

[Serializable]
public class RoleNotFoundException : ApplicationException
{
    public RoleNotFoundException(string message) : base(message)
    {
    }
}


[Serializable]
public class BranchNotFoundException : ApplicationException
{
    public BranchNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class BranchCannotBeDeletedException : ApplicationException
{
    public BranchCannotBeDeletedException(string message) : base(message)
    {
    }
}

[Serializable]
public class DuplicateBranchFoundException : ApplicationException
{
    public DuplicateBranchFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class RoleCannotBeDeletedException : ApplicationException
{
    public RoleCannotBeDeletedException(string message) : base(message)
    {
    }
}

[Serializable]
public class DuplicateEmailFoundException : ApplicationException
{
    public DuplicateEmailFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class DuplicateNameFoundException : ApplicationException
{
    public DuplicateNameFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class UserSignatureNotFoundException : ApplicationException
{
    public UserSignatureNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class CurrencyExchangeRateDuplicateException : Exception
{
    public CurrencyExchangeRateDuplicateException(string message) : base(message)
    {
    }
}

[Serializable]
public class BankAccountCannotBeDeletedException : Exception
{
    public BankAccountCannotBeDeletedException(string message) : base(message)
    {
    }
}

[Serializable]
public class BankChargeNotFoundException : ApplicationException
{
    public BankChargeNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class TaxJurisdictionNotFoundException : ApplicationException
{
    public TaxJurisdictionNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class TaxJurisdictionZoneNotFoundException : ApplicationException
{
    public TaxJurisdictionZoneNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class TaxRateNotFoundException : ApplicationException
{
    public TaxRateNotFoundException(string message) : base(message)
    {
    }
}

[Serializable]
public class TaxClassTaxRateNotFoundException : ApplicationException
{
    public TaxClassTaxRateNotFoundException(string message) : base(message)
    {
    }
}
[Serializable]
public class DepartmentCannotBeDeletedException : ApplicationException
{
    public DepartmentCannotBeDeletedException(string message) : base(message)
    {
    }
}
[Serializable]
public class DepartmentCodeCannotBeAddedException : ApplicationException
{
    public DepartmentCodeCannotBeAddedException(string message) : base(message)
    {
    }
}
[Serializable]
public class TaxJurisdictionCodeCannotBeAddedException : ApplicationException
{
    public TaxJurisdictionCodeCannotBeAddedException(string message) : base(message)
    {
    }
}
[Serializable]
public class TaxRateCodeCannotBeAddedException : ApplicationException
{
    public TaxRateCodeCannotBeAddedException(string message) : base(message)
    {
    }
}
[Serializable]
public class TaxJurisdictionZoneCannotBeAddedException : ApplicationException
{
    public TaxJurisdictionZoneCannotBeAddedException(string message) : base(message)
    {
    }
}
[Serializable]
public class DesignationCannotBeDeletedException : ApplicationException
{
    public DesignationCannotBeDeletedException(string message) : base(message)
    {
    }
}
[Serializable]
public class TaxJurisdictionZoneCannotBeDeletedException : ApplicationException
{
    public TaxJurisdictionZoneCannotBeDeletedException(string message) : base(message)
    {
    }
}
[Serializable]
public class TaxRateCannotBeDeletedException : ApplicationException
{
    public TaxRateCannotBeDeletedException(string message) : base(message)
    {
    }
}

[Serializable]
public class DesignationCodeCannotBeAddedException : ApplicationException
{
    public DesignationCodeCannotBeAddedException(string message) : base(message)
    {
    }
}

[Serializable]
public class DocumentTypeCannotBeAddedException : ApplicationException
{
    public DocumentTypeCannotBeAddedException(string message) : base(message)
    {
    }
}
[Serializable]
public class AlreadyProcessedException : ApplicationException
{
    public AlreadyProcessedException(string message) : base(message)
    {
    }

    public AlreadyProcessedException(string documnetNumber, string status) : base($"{documnetNumber} already is in {status} status.")
    {
    }
}