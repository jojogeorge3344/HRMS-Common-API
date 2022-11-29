
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;

public class IntegrationDetailDimensionRepositroy : GenericRepository<IntegrationDetailDimension>, IIntegrationDetailDimensionRepository
    {
    private readonly IHttpContextAccessor httpContextAccessor;
    public IntegrationDetailDimensionRepositroy(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {
        this.httpContextAccessor = httpContextAccessor;
    }


    public async  Task<IEnumerable<IntegrationDetailDimension>> GetDetailDimensionByHeaderId(int HeaderId)
        {
            string sql = @"SELECT idd.branchname,
                                   idd.dimensiontypeid,
                                   idd.dimensiontypename,
                                   idd.integrationdetailid,
                                   idd.dimensiondetailid,
                                   idd.dimensioncode,
                                   idd.dimensiondetailname,
                                   idd.branchid,
                                   idd.financialyearid,
                                   idd.transactiondate
                            FROM   finance.integrationdetaildimension idd
                            WHERE  idd.headerid =@HeaderId";
            return await Connection.QueryAsync<IntegrationDetailDimension>(sql, new { HeaderId });
        }

        public async  Task<IEnumerable<IntegrationDetailDimension>> GetDimensionDetailsbyId(int integrationId)
        {
            string sql = @"SELECT idd.id,
                                   idd.integrationdetailid,
                                   idd.branchname,
                                   idd.branchid,
                                   idd.dimensiontypename,
                                   idd.dimensiontypeid,
                                   idd.dimensioncode,
                                   idd.dimensiondetailid,
                                   idd.dimensiondetailname,
                                   idd.debitamount,
                                   idd.creditamount
                            FROM   finance.integrationdetaildimension idd
                            WHERE  idd.headerid = @integrationId";
            return await Connection.QueryAsync<IntegrationDetailDimension>(sql, new { integrationId });
        }
    }

