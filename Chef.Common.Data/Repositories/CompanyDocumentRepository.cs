using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public class CompanyDocumentRepository:TenantRepository<CompanyDocuments>, ICompanyDocumentRepostory
{
    public CompanyDocumentRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {

    }

    public async Task<IEnumerable<CompanyDocuments>> GetCompanyDocuments(int companyId)
    {
       return await QueryFactory
            .Query<CompanyDocuments>()
            .Select(" id", "documentname", "documenttypeid", "documenttypename", "companyid", "expiredate", "issuedate", "isattachment", "phoneno", "emailid", "email", "sms")
            .Where("companyid", companyId)
            .WhereNotArchived()
            .GetAsync<CompanyDocuments>();
    }
}
