using Chef.Common.Models;

namespace Chef.Common.Data.Services;

public class MasterDataService : IMasterDataService
{
    private readonly IMasterDataRepository masterDataRespository;
    private readonly ICommonDataRepository commonDataRepository;

    public MasterDataService(IMasterDataRepository masterDataRespository, ICommonDataRepository commonDataRepository)
    {
        this.masterDataRespository = masterDataRespository;
        this.commonDataRepository = commonDataRepository;
    }

    public Task<IEnumerable<BusinessPartner>> GetActiveBusinessPartners()
    {
        return masterDataRespository.GetActiveBusinessPartners();
    }

    public Task<IEnumerable<Employee>> GetActiveEmployees()
    {
        return masterDataRespository.GetActiveEmployees();
    }

    public async Task<IEnumerable<Employee>> GetCurrentCompanyActiveEmployees()
    {
        var company = await commonDataRepository.GetMyCompany();
        return await masterDataRespository.GetCurrentCompanyActiveEmployees(company.CompanyId);
    }

    public Task<Company> GetBaseCompany()
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

    public async Task<IEnumerable<Currency>> GetCurrencies()
    {
        return await masterDataRespository.GetCurrenciesHavingExRate();
    }

    public Task<Currency> GetCurrency(int id)
    {
        return masterDataRespository.GetCurrency(id);
    }

    public Task<Currency> GetCurrencyByCode(string code)
    {
        return masterDataRespository.GetCurrencyByCode(code);
    }

    public Task<Employee> GetEmployee(int id)
    {
        return masterDataRespository.GetEmployee(id);
    }

    public async Task<IEnumerable<CurrencyExchangeRate>> GetExchangeRates(string baseCurrencyCode, string transactionCurrency, DateTime transactionDate)
    {
        if (baseCurrencyCode == transactionCurrency)
        {
            return await Task.FromResult(new List<CurrencyExchangeRate>()
            {
                new()
                {
                    BaseCurrencyCode = baseCurrencyCode,
                    TransactionCurrencyCode = transactionCurrency,
                    ExchangeDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    ExchangeRate = 1,
                    CreatedBy = HttpHelper.Username,
                    ModifiedBy = HttpHelper.Username
                }
            });
        }
        return await masterDataRespository.GetExchangeRates(baseCurrencyCode, transactionCurrency, transactionDate);
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

    public Task<IEnumerable<JournalBookType>> GetJournalBookTypeByGroup(int groupNumber)
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
    public Task<IEnumerable<TaxClass>> GetAllTaxSetupAsync()
    {
        return masterDataRespository.GetAllTaxSetupAsync();
    }
    public Task<IEnumerable<BusinessPartner>> GetAllActiveBP(string? top,string? fil, string? skip)
    {

        SqlSearch sqlSearch = new SqlSearch();
        sqlSearch.Rules.Add(new SqlSearchRule());
        sqlSearch.Rules[0].Field = "Name";
        sqlSearch.Rules[0].Operator = SqlSearchOperator.Contains;
        sqlSearch.Rules[0].Value = fil.Replace("code eq '", "").Replace("'", "");
        sqlSearch.Rules.Add(new SqlSearchRule());
        sqlSearch.Rules[1].Field = "Code";
        sqlSearch.Rules[1].Operator = SqlSearchOperator.Contains;
        sqlSearch.Rules[1].Value = fil.Replace("code eq '", "").Replace("'", "");
        sqlSearch.Condition = SqlConditionOperator.OR;

        sqlSearch.Limit = Convert.ToInt32(top);
        if (skip != null)
            sqlSearch.Offset = Convert.ToInt32(skip);
        //  string 
        return masterDataRespository.GetAllActiveBP(sqlSearch);
    }
    public Task<BankBranch> getBankBranchById(int id)
    {
        return masterDataRespository.getBankBranchById(id);
    }
    public Task<IEnumerable<BankBranch>> getAllBranches()
    {
        return masterDataRespository.getAllBranches();
    }
    public Task<Currency> GetByCurrency(string transactionCurrency)
    {
        return masterDataRespository.GetByCurrency(transactionCurrency);
    }
    public Task<IEnumerable<Company>> GetAllCompanies()
    {
        return masterDataRespository.GetAllCompanies();
    }
    public Task<IEnumerable<BankBranch>> GetAllBankBranchesByBank(int bankId)
    {
        return masterDataRespository.GetAllBankBranchesByBank(bankId);
    }

    public Task<IEnumerable<FinancialYearPeriod>> GetFinancialYearPeriod(int finacialyearid)
    {
        return masterDataRespository.GetFinancialYearPeriod(finacialyearid);
    }

    public Task<BusinessPartner> GetCustomerDetails(int CustomerID)
    {
        return masterDataRespository.GetCustomerDetails(CustomerID);
    }
}

