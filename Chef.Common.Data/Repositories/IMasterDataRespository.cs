﻿namespace Chef.Common.Data.Repositories;

public interface IMasterDataRepository : IRepository
{
    Task<IEnumerable<CountryDto>> GetCountries();
    Task<IEnumerable<State>> GetStates(int countryId);

    Task<IEnumerable<Employee>> GetActiveEmployees();
    Task<Employee> GetEmployee(int id);

    Task<IEnumerable<JournalBookType>> GetJournalBookTypes();
    Task<JournalBookType> GetJournalBookTypeByGroup(int groupNumber);

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
}

