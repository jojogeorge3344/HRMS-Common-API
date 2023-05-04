using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public class CompanyDocumentAttachmentRepository:TenantRepository<CompanyDocumentAttachment>, ICompanyDocumentAttachmentRepository
{
    public CompanyDocumentAttachmentRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {

    }
}
