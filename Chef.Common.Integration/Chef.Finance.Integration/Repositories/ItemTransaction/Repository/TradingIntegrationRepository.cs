
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public class TradingIntegrationRepository : TenantRepository<TradingIntegrationHeader>, ITradingIntegrationRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository;

    public TradingIntegrationRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session, IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository) : base(httpContextAccessor, session)
    {
        this.journalBookNumberingSchemeRepository = journalBookNumberingSchemeRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<TradingIntegrationHeaderDetailsViewModel>> GetIntegrationHeaderDetails(int integerationHeaderId)
    {

        //string sql = @"SELECT 
        //                              tih.id AS tradingintegrationheaderid, 
        //                              tih.businesspartnerid, 
        //                              tih.documenttype, 
        //                              tih.documentnumber, 
        //                              tih.journalbookcode, 
        //                              tih.journalbookid, 
        //                              tih.currency, 
        //                              tih.exchangerate, 
        //                              tih.remark, 
        //                              tih.businesspartnercode, 
        //                              ids.id AS integrationdetailid, 
        //                              ids.transactiondate, 
        //                              Sum(ids.debitamountinbasecurrency), 
        //                              Sum(ids.creditamountinbasecurrency), 
        //                              ids.branchid, 
        //                              ids.ledgeraccountid, 
        //                              ids.ledgeraccountcode, 
        //                              ids.ledgeraccountname, 
        //                              Sum(ids.creditamount), 
        //                              Sum(ids.debitamount), 
        //                              ids.financialyearid 
        //                            FROM 
        //                              finance.tradingintegrationheader tih 
        //                              INNER JOIN finance.integrationdetails ids ON tih.id = ids.integrationheaderid 
        //                            WHERE 
        //                              tih.id = @integerationHeaderId 
        //                            GROUP BY 
        //                              tih.id, 
        //                              tih.businesspartnerid, 
        //                              tih.documenttype, 
        //                              tih.documentnumber, 
        //                              tih.journalbookcode, 
        //                              tih.journalbookid, 
        //                              tih.currency, 
        //                              tih.exchangerate, 
        //                              tih.remark, 
        //                              tih.businesspartnercode, 
        //                              ids.id, 
        //                              ids.transactiondate, 
        //                              ids.branchid, 
        //                              ids.ledgeraccountid, 
        //                              ids.ledgeraccountcode, 
        //                              ids.ledgeraccountname, 
        //                              ids.financialyearid
        //                            ";
        string sql = @"SELECT tih.id AS tradingintegrationheaderid,
                                   tih.*,
                                   ids.id AS integrationdetailid,
                                   ids.*
                            FROM   finance.tradingintegrationheader tih
                                   INNER JOIN finance.integrationdetails ids
                                           ON tih.id = ids.integrationheaderid
                            WHERE  tih.id = @integerationHeaderId";
        return await Connection.QueryAsync<TradingIntegrationHeaderDetailsViewModel>(sql, new { integerationHeaderId });

    }
    //public async Task<TradingIntegrationHeader> InsertAsync(ItemTransactionFinance itemTransactionFinances)
    //{
    //    if (itemTransactionFinances != null)
    //    { 
    //        string sql = new QueryBuilder<TradingIntegrationHeader>().GenerateInsertQuery();
    //        intHeader.Id = Convert.ToInt32(Connection.ExecuteScalar(sql, intHeader));
    //        return intHeader;
    //    }
    //    return null;
    //}


    public async Task<IEnumerable<ItemTransactionFinanceDetailsDto>> GetItemtransactionFinanceDetails(int headerId)
    {
        string sql = String.Format(@"SELECT ids.itemtransactionfinanceid,
                                   ti.id              AS referencedocumentid,
                                   ti.businesspartnerid,
                                   ti.documenttype,
                                   ti.documentnumber,
                                   ti.transactiondate AS documentdate,
                                   ti.transactiondate,
                                   ti.journalbookid,
                                   ti.journalbookcode,
                                   ti.currency        AS transactioncurrencycode,
                                   ti.exchangerate,
                                   ti.remark          AS narration,
                                   ti.branchid,
                                   ti.approvestatus,
                                   ti.businesspartnercode,
                                   ids.debitamountinbasecurrency,
                                   ids.creditamountinbasecurrency,
                                   ids.ledgeraccountid,
                                   ids.ledgeraccountcode,
                                   ids.ledgeraccountname,
                                   ids.debitamount,
                                   ids.creditamount,                                    
                                   ids.id             AS referencedocumentdetailid,
                                   ids.financialyearid
                            FROM   finance.tradingintegrationheader ti
                                   INNER JOIN finance.integrationdetails ids
                                           ON ids.integrationheaderid = ti.id
                            WHERE  ti.id = {0};", headerId);
        return await Connection.QueryAsync<ItemTransactionFinanceDetailsDto>(sql);
    }
    public async Task<int> UpdateStatus(int HeaderId)
    {
        string sql = @"UPDATE finance.tradingintegrationheader set approvestatus=2,approvestatusname='Approved' where id=@HeaderId";
        await Connection.ExecuteAsync(sql, new { HeaderId });
        return 1;

    }
}
