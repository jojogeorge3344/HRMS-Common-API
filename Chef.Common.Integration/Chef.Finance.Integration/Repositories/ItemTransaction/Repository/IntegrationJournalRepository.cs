
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public class IntegrationJournalRepository : TenantRepository<TradingIntegrationHeader>, IIntegrationJournalRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public IntegrationJournalRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId,int transtypeId, DateTime fromDate,DateTime toDate)
    {
        string sql = @"SELECT id,
                                   documentnumber,
                                   businesspartnercode,
                                   businesspartnername,
                                   currency,
                                   totalamount 
                            FROM   finance.tradingintegrationheader
                            WHERE  transorginid = @transorginId
                            and    transtypeid =  @transtypeId
                            and to_date(cast(createddate AS text), 'YYYY-MM-DD') BETWEEN @fromDate AND    @toDate";
        return await DatabaseSession.QueryAsync<TradingIntegrationHeader>(sql, new { transorginId, transtypeId , fromDate, toDate });
         
    }

    public async Task<IEnumerable<IntegrationDetails>> GetAllIntegrationDetailsById(int integrationId)
    {
        SqlKata.Query query = SqlQueryBuilder.Query<IntegrationDetails>()
            .Select<IntegrationDetails>()
            .Where("integrationheaderid", integrationId);
        return await DatabaseSession.QueryAsync<IntegrationDetails>(query, default);
    }
}
