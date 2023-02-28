
using Chef.Common.Types;
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
        int headerBrandId = this.headerBranchId;
        string sql = @"SELECT th.id,
                               th.documentnumber,
                               th.businesspartnercode,
                               th.businesspartnername,
                               th.currency,
                               th.totalamount,
                               th.transactiondate,
                               th.createddate,
                               Max(it.narration) AS narration
                        FROM   finance.tradingintegrationheader th
                               RIGHT JOIN finance.integrationdetails it
                                       ON th.id = it.integrationheaderid
                        WHERE  To_date(Cast(th.transactiondate AS TEXT), 'YYYY-MM-DD') BETWEEN
                                      @fromDate AND @toDate AND th.branchid=@headerBrandId";
        if (status == 1)
        {
            sql += " AND th.approvestatus = 1";
        }
        else
        {
            sql += " AND th.approvestatus = 2";
        }
        if (transorginId != 0 || transtypeId != 0)
        {
            sql += " and   th.transorginid = @transorginId  and   th.transtypeid =  @transtypeId";
        }
        sql += " GROUP  BY th.id order by  th.transactiondate";
        return await DatabaseSession.QueryAsync<TradingIntegrationHeader>(sql, new { transorginId, transtypeId, fromDate, toDate, status, headerBrandId });

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

    public async Task<int> GetintegrationheaderId(string documentNumber)
    {
        string sql = @"SELECT id
                            FROM   finance.tradingintegrationheader
                            WHERE  documentnumber = @documentNumber
                                   AND isarchived = false";
        return await Connection.QueryFirstOrDefaultAsync<int>(sql, new { documentNumber });

    }
    public async Task<int> Deleteintegrationheader(int tradingintegrationheaderId)
    {
        int approveStatus = Convert.ToInt32(ApproveStatus.Deleted);
        string sql = @"UPDATE finance.tradingintegrationheader
                        SET    isarchived = true,
                               approvestatus = @approveStatus
                        WHERE  id = @tradingintegrationheaderId;

                        UPDATE finance.integrationdetails
                        SET    isarchived = true
                        WHERE  integrationheaderid = @tradingintegrationheaderId;

                        UPDATE finance.integrationdetaildimension
                        SET    isarchived = true
                        WHERE  headerid = @tradingintegrationheaderId ";
        return await Connection.ExecuteAsync(sql,new { tradingintegrationheaderId, approveStatus });

    }
}
