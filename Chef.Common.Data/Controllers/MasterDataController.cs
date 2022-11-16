using Chef.Common.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chef.Common.Data.Controller;

[ApiController]
[Route("api/console/[controller]/[action]")]
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

    public async Task<ActionResult<IEnumerable<CurrencyExchangeRate>>> GetExchangeRates(
        string baseCurrencyCode,
        string transactionCurrency,
        DateTime transactionDate)
    {
        throw new NotImplementedException();
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
        return Ok(masterDataService.GetItemFamilies(segmentId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemSegment>>> GetItemSegments()
    {
        return Ok(await masterDataService.GetItemSegments());
    }

    [HttpGet]
    [Route("{groupNumber:int}")]
    public async Task<ActionResult<JournalBookType>> GetJournalBookTypeByGroup(int groupNumber)
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
}

