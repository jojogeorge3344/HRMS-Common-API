
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public class IntegrationJournalRepository : TenantRepository<TradingIntegrationHeader>, IIntegrationJournalRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public IntegrationJournalRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate, int status)
    {
        string sql = @"SELECT id,
                                   documentnumber,
                                   businesspartnercode,
                                   businesspartnername,
                                   currency,
                                   totalamount,
                                   transactiondate 
                            FROM   finance.tradingintegrationheader
                            WHERE  to_date(cast(createddate AS text), 'YYYY-MM-DD') BETWEEN @fromDate AND    @toDate AND approvestatus = @status";

        if (transorginId != 0 || transtypeId != 0)
        {
            sql += " and  transorginid = @transorginId  and  transtypeid =  @transtypeId";
        }
        sql += " order by createddate";
        return await DatabaseSession.QueryAsync<TradingIntegrationHeader>(sql, new { transorginId, transtypeId, fromDate, toDate, status });

    }

    public async Task<IEnumerable<IntegrationDetalDimensionViewModel>> GetAllIntegrationDetailsDimensionById(int integrationId)
    {
        string sql = @"SELECT ids.id,
                                       ids.integrationheaderid,
                                       ids.ledgeraccountid,
                                       ids.ledgeraccountcode,
                                       ids.narration,
                                       ids.ledgeraccountname,
                                       ids.debitamount,
                                       ids.debitamountinbasecurrency,
                                       ids.creditamount,
                                       ids.creditamountinbasecurrency
                                FROM   finance.integrationdetails ids
                                WHERE  ids.integrationheaderid = @integrationId";
        return await DatabaseSession.QueryAsync<IntegrationDetalDimensionViewModel>(sql, new { integrationId });
        //SqlKata.Query query = SqlQueryBuilder.Query<IntegrationDetails>()
        //    .Select<IntegrationDetails>()
        //    .Where("integrationheaderid", integrationId);
        //return await DatabaseSession.QueryAsync<IntegrationDetails>(query, default);
    }
}
