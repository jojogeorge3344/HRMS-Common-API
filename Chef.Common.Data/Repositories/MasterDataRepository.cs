namespace Chef.Common.Data.Repositories;

public class MasterDataRepository : ConsoleRepository<Model>, IMasterDataRepository
{
    public MasterDataRepository(
        IHttpContextAccessor httpContextAccessor,
        IConsoleConnectionFactory consoleConnectionFactory)
        : base(httpContextAccessor, consoleConnectionFactory)
    {
    }

    public async Task<IEnumerable<BusinessPartner>> GetActiveBusinessPartners()
    {
        return await QueryFactory
            .Query<BusinessPartner>()
            .Select("id", "name", "code")
            .Where("isactive", true)
            .WhereNotArchived()
            .GetAsync<BusinessPartner>();
    }

    public async Task<IEnumerable<Employee>> GetActiveEmployees()
    {
        return await QueryFactory
            .Query<Employee>()
            .Select("id", "name", "code")
            .Where("isactive", true)
            .WhereNotArchived()
            .GetAsync<Employee>();
    }

    public async Task<BusinessPartner> GetBusinessPartner(int id)
    {
        return await QueryFactory
            .Query<BusinessPartner>()
            .Select("id", "name", "code")
            .Where("id", id)
            .WhereNotArchived()
            .FirstOrDefaultAsync<BusinessPartner>();
    }

    public async Task<IEnumerable<CountryDto>> GetCountries()
    {
        return await QueryFactory
            .Query<Country>()
            .Select("id", "name", "code")
            .WhereNotArchived()
            .GetAsync<CountryDto>();
    }

    public async Task<IEnumerable<Currency>> GetCurrencies()
    {
        return await QueryFactory
            .Query<Currency>()
            .Select("id", "name", "code")
            .WhereNotArchived()
            .GetAsync<Currency>();
    }

    public async Task<Currency> GetCurrency(int id)
    {
        return await QueryFactory
            .Query<Currency>()
            .Select("id", "name", "code")
            .Where("id", id)
            .WhereNotArchived()
            .FirstOrDefaultAsync<Currency>();
    }

    public async Task<Employee> GetEmployee(int id)
    {
        return await QueryFactory
            .Query<Employee>()
            .Select("id", "name", "code")
            .Where("id", id)
            .WhereNotArchived()
            .FirstOrDefaultAsync<Employee>();
    }

    public async Task<IEnumerable<CurrencyExchangeRate>> GetExchangeRates(
        string baseCurrencyCode,
        string transactionCurrency,
        DateTime transactionDate)
    {
        return await QueryFactory
            .Query<CurrencyExchangeRate>()
            .Select("BaseCurrencyId", "BaseCurrencyCode", "TransactionCurrencyId", "TransactionCurrencyCode", "ExchangeDate", "ExchangeRate")
            .Where(new
            {
                baseCurrencyCode = baseCurrencyCode,
                transactionCurrencyCode = transactionCurrency
            })
            .Where(q =>
    q.Where("exchangeDate", transactionDate).OrWhere("exchangeDate", "<", transactionDate))
            .WhereNotArchived().OrderByDesc("exchangeDate").Limit(1)
            .GetAsync<CurrencyExchangeRate>();
    }

    public async Task<FinancialYear> GetFinancialYear(int id)
    {
        return await QueryFactory
            .Query<FinancialYear>()
            .Select("id", "name", "code")
            .Where("id", id)
            .WhereNotArchived()
            .FirstOrDefaultAsync<FinancialYear>();
    }

    public async Task<IEnumerable<FinancialYear>> GetFinancialYears()
    {
        return await QueryFactory
            .Query<Country>()
            .Select("id", "name", "code")
            .WhereNotArchived()
            .GetAsync<FinancialYear>();
    }

    public async Task<IEnumerable<ItemClass>> GetItemClasses(int familyId)
    {
        return await QueryFactory
            .Query<ItemClass>()
            .Select("id", "name", "code")
            .Where("familid", familyId)
            .WhereNotArchived()
            .GetAsync<ItemClass>();
    }

    public async Task<IEnumerable<ItemCommodity>> GetItemCommodities(int itemClassId)
    {
        return await QueryFactory
            .Query<ItemCommodity>()
            .Select("id", "name", "code")
            .Where("itemclassid", itemClassId)
            .WhereNotArchived()
            .GetAsync<ItemCommodity>();
    }

    public async Task<IEnumerable<ItemFamily>> GetItemFamilies(int segmentId)
    {
        return await QueryFactory
            .Query<ItemFamily>()
            .Select("id", "name", "code")
            .Where("segmentId", segmentId)
            .WhereNotArchived()
            .GetAsync<ItemFamily>();
    }

    public async Task<IEnumerable<ItemSegment>> GetItemSegments()
    {
        return await QueryFactory
            .Query<ItemSegment>()
            .Select("id", "name", "code")
            .WhereNotArchived()
            .GetAsync<ItemSegment>();
    }

    public async Task<JournalBookType> GetJournalBookTypeByGroup(int groupNumber)
    {
        return await QueryFactory
            .Query<JournalBookType>()
            .Select("id", "name", "code")
            .Where("groupnumber", groupNumber)
            .WhereNotArchived()
            .FirstOrDefaultAsync<JournalBookType>();
    }

    public async Task<IEnumerable<JournalBookType>> GetJournalBookTypes()
    {
        return await QueryFactory
            .Query<JournalBookType>()
            .Select("id", "name", "code")
            .WhereNotArchived()
            .GetAsync<JournalBookType>();
    }

    public async Task<CurrencyExchangeRate> GetLatestExchangeRate(string currencyCode)
    {
        return await QueryFactory
            .Query<CurrencyExchangeRate>()
            .Where("transactioncurrencycode", "=", currencyCode)
            .WhereNotArchived()
            .OrderByDesc("exchangedate")
            .Limit(1)
            .FirstOrDefaultAsync<CurrencyExchangeRate>();
    }

    public async Task<IEnumerable<State>> GetStates(int countryId)
    {
        return await QueryFactory
            .Query<State>()
            .Select("id", "name", "code")
            .Where("countryid", countryId)
            .WhereNotArchived()
            .GetAsync<State>();
    }

    public async Task<IEnumerable<TimeZone>> GetTimeZones()
    {
        return await QueryFactory
            .Query<TimeZone>()
            .Select("id", "timezoneid", "displayname", "baseutcoffset")
            .WhereNotArchived()
            .GetAsync<TimeZone>();
    }

    public async Task<Company> GetBaseCompany()
    {
        return await QueryFactory
            .Query<Company>()
            .Where("basecompanyid", 0)
            .WhereNotArchived()
            .FirstOrDefaultAsync<Company>();
    }

    public async Task<Currency> GetByCurrency(string baseCurrency)
    {
        return await QueryFactory
            .Query<Currency>()
            .Where("code", baseCurrency)
            .WhereNotArchived()
            .FirstOrDefaultAsync<Currency>();
    }
}

