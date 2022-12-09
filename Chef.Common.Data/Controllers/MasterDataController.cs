using Chef.Common.Data.Services;
using Chef.Common.Models;
using Chef.Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chef.Common.Data.Controller;

[ApiController]
[Route("api/common/[controller]/[action]")]
public class MasterDataController : ControllerBase
{
    private readonly IMasterDataService  masterDataService;

    public MasterDataController(IMasterDataService masterDataService)
    {
        this.masterDataService = masterDataService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessPartner>>> GetActiveBusinessPartners()
    {
        return Ok(await masterDataService.GetActiveBusinessPartners());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetActiveEmployees()
    {
        return Ok(await masterDataService.GetActiveEmployees());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<BusinessPartner>> GetBusinessPartner(int id)
    {
        return Ok(await(masterDataService.GetBusinessPartner(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
    {
        return Ok(await masterDataService.GetCountries());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
    {
        return Ok(await(masterDataService.GetCurrencies()));
    }

    [HttpGet]
    [Route("{code}")]
    public async Task<ActionResult<Currency>> GetCurrencyByCode(string code)
    {
        return Ok(await (masterDataService.GetCurrencyByCode(code)));
    }

    [HttpGet]
    public async Task<ActionResult<Company>> GetBaseCompany()
    {
        return Ok(await (masterDataService.GetBaseCompany()));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Currency>> GetCurrency(int id)
    {
        return Ok(await masterDataService.GetCurrency(id));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        return Ok(await masterDataService.GetEmployee(id));
    }

    [HttpGet]
    [Route("{baseCurrencyCode}/{transactionCurrency}/{transactionDate}")]
    public async Task<ActionResult<IEnumerable<CurrencyExchangeRate>>> GetExchangeRates(
        string baseCurrencyCode,
        string transactionCurrency,
        DateTime transactionDate)
    {
        return Ok(await masterDataService.GetExchangeRates(baseCurrencyCode, transactionCurrency, transactionDate));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<FinancialYear>> GetFinancialYear(int id)
    {
        return Ok(await masterDataService.GetFinancialYear(id));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialYear>>> GetFinancialYears()
    {
        return Ok(await masterDataService.GetFinancialYears());
    }

    [HttpGet]
    [Route("{familyId:int}")]
    public async Task<ActionResult<IEnumerable<ItemClass>>> GetItemClasses(int familyId)
    {
        return Ok(await masterDataService.GetItemClasses(familyId));
    }

    [HttpGet]
    [Route("{itemClassId:int}")]
    public async Task<ActionResult<IEnumerable<ItemCommodity>>> GetItemCommodities(int itemClassId)
    {
        return Ok(await masterDataService.GetItemCommodities(itemClassId));
    }

    [HttpGet]
    [Route("{segmentId:int}")]
    public async Task<ActionResult<IEnumerable<ItemFamily>>> GetItemFamilies(int segmentId)
    {
        return Ok(await masterDataService.GetItemFamilies(segmentId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemSegment>>> GetItemSegments()
    {
        return Ok(await masterDataService.GetItemSegments());
    }

    [HttpGet]
    [Route("{groupNumber:int}")]
    public async Task<ActionResult<IEnumerable<JournalBookType>>> GetJournalBookTypeByGroup(int groupNumber)
    {
        return Ok(await masterDataService.GetJournalBookTypeByGroup(groupNumber));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JournalBookType>>> GetJournalBookTypes()
    {
        return Ok(await masterDataService.GetJournalBookTypes());
    }

    [HttpGet]
    [Route("{countryId:int}")]
    public async Task<ActionResult<IEnumerable<State>>> GetStates(int countryId)
    {
        return Ok(await masterDataService.GetStates(countryId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeZone>>> GetTimeZones()
    {
        return Ok(await masterDataService.GetTimeZones());
    }
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Bank>>> GetAllBank()
	{
		var banks = await masterDataService.GetAllBank(); //.GetAll<Bank>("Bank/getAll"); 

		if (banks == null)
		{
			return NotFound("The bank does not exist.");
		}
		return Ok(banks);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<IEnumerable<BankBranch>>> GetBranchByBank(int id)
	{
		var branches = await masterDataService.GetBranchByBank(id);    //.GetById<BankBranch>(id, "bankBranch/get/");

		if (branches == null)
		{
			return NotFound("The branch does not exist.");
		}
		return Ok(branches);
	}


	[HttpGet]
	public async Task<ActionResult<IEnumerable<TaxClass>>> GetAllTaxSetupAsync()
	{
        var tax = await masterDataService.GetAllTaxSetupAsync();
		if (tax == null)
		{
			return NotFound("The branch does not exist.");
		}
		return Ok(tax); //GetAll<Tax>("TaxSetup/getAll"));
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<BusinessPartner>>> getAllActiveBP()
	{
        //TODO:move this changes to service class
	string top = Request.Query["$top"];
		string fil = Request.Query["$filter"];
		string skip = Request.Query["$skip"];

        var businessPartners = await masterDataService.GetAllActiveBP(top, fil, skip);   //getByLimit<BusinessPartner>(sqlSearch, "businessPartner/getAllActiveBP");

        if (businessPartners == null)
		{
			return NotFound("The business partner does not exist.");
		}
		return Ok(businessPartners);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<BankBranch>> getBankBranchById(int id)
	{
        var bankBranch = await masterDataService.getBankBranchById(id);     //GetBankBranchById<BankBranch>(id, "bankBranch/get/");

		if (bankBranch == null)
		{
			return NotFound("The Branch does not exist.");
		}

		return Ok(bankBranch);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<BankBranch>>> getAllBranches()
	{
        var bankBranch = await masterDataService.getAllBranches();    //GetAll<BankBranch>("bankBranch/getAll");

		if (bankBranch == null)
		{
			return NotFound("The Branch does not exist.");
		}

		return Ok(bankBranch);
	}

	[HttpGet("{transactionCurrency}")]
	public async Task<ActionResult<Currency>> GetByCurrency(string transactionCurrency)
	{
        return Ok(await masterDataService.GetByCurrency(transactionCurrency));  //GetByCurrency<Currency>("Currency/GetByCurrency/transactionCurrency/", transactionCurrency));
	}


	[HttpGet]
	public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
	{
        var companys = await masterDataService.GetAllCompanies();   //GetAll<Company>("Company/GetAll");

		if (companys == null)
		{
			return NotFound("The company does not exist.");
		}
		return Ok(companys);
	}



	[HttpGet("{bankId:int}")]
	public async Task<ActionResult<IEnumerable<BankBranch>>> GetAllBankBranchesByBank(int bankId)
	{
        var bankBranches = await masterDataService.GetAllBankBranchesByBank(bankId);   //GetById<BankBranch>(bankId, "bankBranch/getAllByBank");

		if (bankBranches == null)
		{
			return NotFound("The bank branch does not exist.");
		}
		return Ok(bankBranches);
	}

    [HttpGet("{finacialyearid}")]
    public async Task<ActionResult<IEnumerable<FinancialYearPeriod>>> GetFinancialYearPeriod(int finacialyearid)
    {
        return Ok(await masterDataService.GetFinancialYearPeriod(finacialyearid));
    }
}

