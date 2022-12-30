using Chef.Common.Repositories;

namespace Chef.Common.Data.Services;

public interface IMasterDataService : IBaseService
{
    Task<IEnumerable<CountryDto>> GetCountries();
    Task<IEnumerable<State>> GetStates(int countryId);

    Task<IEnumerable<Employee>> GetActiveEmployees();
    Task<IEnumerable<Employee>> GetCurrentCompanyActiveEmployees();
    Task<Employee> GetEmployee(int id);

    Task<IEnumerable<JournalBookType>> GetJournalBookTypes();
    Task<IEnumerable<JournalBookType>> GetJournalBookTypeByGroup(int groupNumber);

    Task<IEnumerable<BusinessPartner>> GetActiveBusinessPartners();
    Task<BusinessPartner> GetBusinessPartner(int id);

    Task<IEnumerable<Currency>> GetCurrencies();
    Task<Currency> GetCurrency(int id);

    Task<IEnumerable<CurrencyExchangeRate>> GetExchangeRates(
    string baseCurrencyCode,
    string transactionCurrency,
    DateTime transactionDate);
    Task<CurrencyExchangeRate> GetLatestExchangeRate(string currencyCode);


    Task<IEnumerable<FinancialYear>> GetFinancialYears();
    Task<FinancialYear> GetFinancialYear(int id);

    Task<IEnumerable<ItemSegment>> GetItemSegments();
    Task<IEnumerable<ItemFamily>> GetItemFamilies(int segmentId);
    Task<IEnumerable<ItemClass>> GetItemClasses(int familyId);
    Task<IEnumerable<ItemCommodity>> GetItemCommodities(int itemClassId);

    Task<IEnumerable<TimeZone>> GetTimeZones();

    Task<Currency> GetCurrencyByCode(string code);

    Task<Company> GetBaseCompany();

	Task<IEnumerable<Bank>> GetAllBank();
	Task<IEnumerable<BankBranch>> GetBranchByBank(int id);
    Task<IEnumerable<TaxClass>> GetAllTaxSetupAsync();
    Task<IEnumerable<BusinessPartner>> GetAllActiveBP(string? top, string? fil, string? skip);   
    Task<BankBranch> getBankBranchById(int id);
    Task<IEnumerable<BankBranch>> getAllBranches();
    Task<Currency>GetByCurrency(string transactionCurrency);
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<IEnumerable<BankBranch>> GetAllBankBranchesByBank(int bankId);
    Task<IEnumerable<FinancialYearPeriod>> GetFinancialYearPeriod(int finacialyearid);
    Task<BusinessPartner> GetCustomerDetails(int CustomerID);
    Task<IEnumerable<City>> GetCityByStateId(int stateId);
}