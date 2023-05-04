using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public class CompanyDocumentRepository:TenantRepository<ComapnyDocuments>, ICompanyDocumentRepostory
{
    public CompanyDocumentRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {

    }
}
