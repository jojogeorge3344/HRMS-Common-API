﻿namespace Chef.Common.Data.Services;

public class MasterDataService : IMasterDataService
{
    private readonly IMasterDataRepository masterDataRespository;

    public MasterDataService(IMasterDataRepository masterDataRespository)
    {
        this.masterDataRespository = masterDataRespository;
    }

    public Task<IEnumerable<BusinessPartner>> GetActiveBusinessPartners()
    {
        return masterDataRespository.GetActiveBusinessPartners();
    }

    public Task<IEnumerable<Employee>> GetActiveEmployees()
    {
        return masterDataRespository.GetActiveEmployees();
    }

    public  Task<Company> GetBaseCompany()
    {
        return masterDataRespository.GetBaseCompany();
    }

    public Task<BusinessPartner> GetBusinessPartner(int id)
    {
        return masterDataRespository.GetBusinessPartner(id);
    }

    public Task<IEnumerable<CountryDto>> GetCountries()
    {
        return masterDataRespository.GetCountries();
    }

    public Task<IEnumerable<Currency>> GetCurrencies()
    {
        return masterDataRespository.GetCurrencies();
    }

    public Task<Currency> GetCurrency(int id)
    {
        return masterDataRespository.GetCurrency(id);
    }

    public  Task<Currency> GetCurrencyByCode(string code)
    {
        return  masterDataRespository.GetCurrencyByCode(code);
    }

    public Task<Employee> GetEmployee(int id)
    {
        return masterDataRespository.GetEmployee(id);
    }

    public Task<IEnumerable<CurrencyExchangeRate>> GetExchangeRates(string baseCurrencyCode, string transactionCurrency, DateTime transactionDate)
    {
        return masterDataRespository.GetExchangeRates(baseCurrencyCode, transactionCurrency, transactionDate);
    }

    public Task<FinancialYear> GetFinancialYear(int id)
    {
        return masterDataRespository.GetFinancialYear(id);
    }

    public Task<IEnumerable<FinancialYear>> GetFinancialYears()
    {
        return masterDataRespository.GetFinancialYears();
    }

    public Task<IEnumerable<ItemClass>> GetItemClasses(int familyId)
    {
        return masterDataRespository.GetItemClasses(familyId);
    }

    public Task<IEnumerable<ItemCommodity>> GetItemCommodities(int itemClassId)
    {
        return masterDataRespository.GetItemCommodities(itemClassId);
    }

    public Task<IEnumerable<ItemFamily>> GetItemFamilies(int segmentId)
    {
        return masterDataRespository.GetItemFamilies(segmentId);
    }

    public Task<IEnumerable<ItemSegment>> GetItemSegments()
    {
        return masterDataRespository.GetItemSegments();
    }

    public Task<JournalBookType> GetJournalBookTypeByGroup(int groupNumber)
    {
        return masterDataRespository.GetJournalBookTypeByGroup(groupNumber);
    }

    public Task<IEnumerable<JournalBookType>> GetJournalBookTypes()
    {
        return masterDataRespository.GetJournalBookTypes();
    }

    public Task<CurrencyExchangeRate> GetLatestExchangeRate(string currencyCode)
    {
        return masterDataRespository.GetLatestExchangeRate(currencyCode);
    }

    public Task<IEnumerable<State>> GetStates(int countryId)
    {
        return masterDataRespository.GetStates(countryId);
    }

    public Task<IEnumerable<TimeZone>> GetTimeZones()
    {
        return masterDataRespository.GetTimeZones();
    }
	public Task<IEnumerable<Bank>> GetAllBank()
	{
		return masterDataRespository.GetAllBank();

	}
	public Task<IEnumerable<BankBranch>> GetBranchByBank(int id)
	{
		return masterDataRespository.GetBranchByBank(id);

	}
}

