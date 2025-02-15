﻿using Chef.Common.Core.Extensions;
using Chef.Common.Models;
using Chef.Common.Repositories;
using SqlKata;
using System.ComponentModel.Design;

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
            .Select("id", "displayname", "employeecode","firstname","lastname","middlename")
            .Where("isactive", true)
            .WhereNotArchived()
            .GetAsync<Employee>();
    }

    public async Task<IEnumerable<Employee>> GetCurrentCompanyActiveEmployees(int? companyId)
    {
        return await QueryFactory
            .Query<Employee>()
            .Select("id", "displayname", "employeecode", "firstname", "lastname", "middlename")
            .Where("isactive", true)
            .Where("companyid", companyId)
            .WhereNotArchived()
            .GetAsync<Employee>();
    }

    public async Task<Company> GetBaseCompany()
    {
        return await QueryFactory
            .Query<Company>()
            .Select("id", "name", "code", "cityid", "cityname", "stateid", "statename", "countryid", "countryname", "zipcode", "currencyid", "currencycode", "phone", "email", "basecompanyid", "basecompanycode")
            .Where("basecompanyid", 0)
            .WhereNotArchived()
            .FirstOrDefaultAsync<Company>();
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
            .OrderBy("name")
            .GetAsync<CountryDto>();
    }

    public async Task<IEnumerable<Currency>> GetCurrencies()
    {
        return await QueryFactory
            .Query<Currency>()
            .Select("id", "name", "code", "exchangevariationup", "exchangevariationdown")
            .WhereNotArchived()
            .GetAsync<Currency>();
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesHavingExRate()
    {
        var baseCompanyCurrency = QueryFactory
                                    .Query("common.currency as cr")
                                    .Select("cr.{ id, name, code, exchangevariationup, exchangevariationdown }")
                                    .Join("common.company as cmp", "cmp.currencycode", "cr.code")
                                    .Where("cmp.basecompanyid", 0)
                                    .WhereFalse("cmp.isarchived")
                                    .WhereFalse("cr.isarchived")
                                    .WhereTrue("cr.isactive");
        var currenciesContainsExRate = QueryFactory
                                    .Query<Currency>()
                                    .Select("common.currency.{ id, name, code, exchangevariationup, exchangevariationdown }")
                                    .Join("common.currencyexchangerate", "common.currencyexchangerate.transactioncurrencycode", "common.currency.code")
                                    .WhereFalse("common.currency.isarchived")
                                    .WhereFalse("common.currencyexchangerate.isarchived")
                                    .WhereTrue("common.currency.isactive")
                                    .OrderBy("code");

        return await baseCompanyCurrency.Union(currenciesContainsExRate).GetAsync<Currency>();
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

    public async Task<Currency> GetCurrencyByCode(string code)
    {
        return await QueryFactory
           .Query<Currency>()
           .Select("id", "name", "code", "exchangevariationup", "exchangevariationdown")
           .Where("code", code)
           .WhereNotArchived()
           .FirstOrDefaultAsync<Currency>();
    }

    public async Task<Employee> GetEmployee(int id)
    {
        return await QueryFactory
            .Query<Employee>()
            .Select("id", "displayname", "employeecode", "firstname", "lastname", "middlename")
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
            .Query<FinancialYear>()            
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

    public async Task<IEnumerable<JournalBookType>> GetJournalBookTypeByGroup(int groupNumber)
    {
        return await QueryFactory
            .Query<JournalBookType>()
            .Select("id", "name", "code")
            .Where("groupnum", groupNumber)
            .WhereNotArchived()
            .GetAsync<JournalBookType>();
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
            .Select("id", "name")
            .Where("countryid", countryId)
            .WhereNotArchived()
            .OrderBy("name")
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

	public async Task<IEnumerable<Bank>> GetAllBank()
	{
		return await QueryFactory
			.Query<Bank>()
			.Select("id", "code", "name")
			.WhereNotArchived()
			.GetAsync<Bank>();
	}

	public async Task<IEnumerable<BankBranch>> GetBranchByBank(int id)
	{
		return await QueryFactory
				.Query<BankBranch>()
				.Select("id", "name", "code")
				.Where("bankid", id)
				.WhereNotArchived()
				.GetAsync<BankBranch>();
	}
    public async Task<IEnumerable<TaxClass>> GetAllTaxSetupAsync()
    {
        return await QueryFactory
                .Query<TaxClass>()
                .WhereNotArchived()
                .GetAsync<TaxClass>();
    }
    public async Task<IEnumerable<BusinessPartner>> GetAllActiveBP(SqlSearch search , CancellationToken cancellationToken = default)
    {
        //TODO - revisit Order by
        Query query = QueryFactory.Query<BusinessPartner>().OrderByDesc("createddate");
        Query query2 = query.Where(q => q.ApplySqlSearch(search));
        search.Rules.Clear();
        search.Condition = SqlConditionOperator.AND;
        search.Rules.Add(new SqlSearchRule());
        search.Rules[0].Field = "isactive";
        search.Rules[0].Operator = SqlSearchOperator.Equal;
        search.Rules[0].Value = true;
        query2.ApplySqlSearch(search);

        return await query2.GetAsync<BusinessPartner>();
    }
    public async Task<BankBranch> getBankBranchById(int id)
    {
		
		return await QueryFactory
					.Query<BankBranch>()
					//.Select("id", "name", "code")
					.Where("id", id)
					.WhereNotArchived()
					.FirstOrDefaultAsync<BankBranch>();
	}
    public async Task<IEnumerable<BankBranch>> getAllBranches()
    {
        return await QueryFactory
                            .Query<BankBranch>()
                            .WhereNotArchived()
                            .GetAsync<BankBranch>();
    }
    public async Task<Currency>GetByCurrency(string transactionCurrency)
    {
		return await QueryFactory
					.Query<Currency>()
                    .Where("code", transactionCurrency)
					.WhereNotArchived()
					.FirstOrDefaultAsync<Currency>();
	}
    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
		return await QueryFactory
				.Query<Company>()
				.WhereNotArchived()
				.GetAsync<Company>();
	}
    public async Task<IEnumerable<BankBranch>> GetAllBankBranchesByBank(int bankId)
    {
		return await QueryFactory
						.Query<BankBranch>()
						.WhereNotArchived()
				        .Where("bankid", bankId)
						.GetAsync<BankBranch>();
	}

    public async Task<IEnumerable<FinancialYearPeriod>> GetFinancialYearPeriod(int finacialyearid)
    {
        return await QueryFactory
            .Query<FinancialYearPeriod>()
            .Where("financialyearId", finacialyearid)
            .WhereNotArchived()
            .GetAsync<FinancialYearPeriod>();
    }

    public async Task<BusinessPartner> GetCustomerDetails(int CustomerID)
    {
        return await QueryFactory
            .Query<BusinessPartner>()
            .Where("id", CustomerID)
            .WhereNotArchived()
            .FirstOrDefaultAsync<BusinessPartner>();
    }

    public async Task<IEnumerable<City>> GetCityByStateId(int stateId)
    {
        return await QueryFactory
            .Query<City>()
            .Select("id", "name", "stateid")
            .Where("stateid", stateId)
            .WhereNotArchived()
            .GetAsync<City>();
    }

    public async Task<IEnumerable<TaxJurisdiction>> GetAllTaxJurisdiction()
    {
        return await QueryFactory
                .Query<TaxJurisdiction>()
                .WhereNotArchived()
                .GetAsync<TaxJurisdiction>();
    }

    public async Task<IEnumerable<ItemSegment>> GetAllItemSegment(SqlSearch sqlSearch = null)
    {

        return await QueryFactory
            .Query<ItemSegment>()
            .ApplySqlSearch(sqlSearch)
            .WhereNotArchived()
            .GetAsync<ItemSegment>();

    }

    public async Task<IEnumerable<ItemFamily>> GetAllItemFamily(SqlSearch sqlSearch = null)
    {
        return await QueryFactory
           .Query<ItemFamily>()
           .ApplySqlSearch(sqlSearch)
           .WhereNotArchived()
           .GetAsync<ItemFamily>();
    }

    public async Task<IEnumerable<ItemCommodity>> GetAllItemCommodity(SqlSearch sqlSearch = null)
    {
        return await QueryFactory
          .Query<ItemCommodity>()
          .ApplySqlSearch(sqlSearch)
          .WhereNotArchived()
          .GetAsync<ItemCommodity>();
    }

    public async Task<IEnumerable<ItemClass>> GetAllItemClass(SqlSearch sqlSearch = null)
    {
        return await QueryFactory
         .Query<ItemClass>()
         .ApplySqlSearch(sqlSearch)
         .WhereNotArchived()
         .GetAsync<ItemClass>();
    }

    public async Task<IEnumerable<Item>> GetAllItem(SqlSearch sqlSearch = null)
    {
        return await QueryFactory
         .Query<Item>()
         .ApplySqlSearch(sqlSearch)
         .WhereNotArchived()
         .GetAsync<Item>();
    }
    public async Task<Country> GetCountryById(int countryId)
    {
        return await QueryFactory
            .Query<Country>()
            .Select("id", "name")
            .Where("id", countryId)
            .WhereNotArchived()
            .OrderBy("name")
            .FirstOrDefaultAsync<Country>();
    }

    public async Task<IEnumerable<Employee>> GetEmployeeDetailsByCompanyId(int companyId)
    {
        return await QueryFactory
           .Query<Employee>()
           .Where("companyid", companyId)
           .WhereNotArchived()
           .GetAsync<Employee>();
    }

    public async Task<IEnumerable<TypesOfDocument>> GetDocumentTypes()
    {
        return await QueryFactory
            .Query<TypesOfDocument>()
            .WhereNotArchived()
            .GetAsync<TypesOfDocument>();
    }

    public async Task<State> GetStateByStateId(int stateId)
    {
        return await QueryFactory
            .Query<State>()
            .Select("id", "name")
            .Where("id", stateId)
            .WhereNotArchived()
            .FirstOrDefaultAsync<State>();
    }
}

