using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public class CompanyDocumentAttachmentService:AsyncService<CompanyDocumentAttachment>, ICompanyDocumentAttachmentService
{
    private readonly ICompanyDocumentAttachmentRepository companyDocumentAttachmentRepository;
    public CompanyDocumentAttachmentService(ICompanyDocumentRepostory companyDocumentRepostory, ITenantSimpleUnitOfWork tenantSimpleUnitOfWork, IHttpContextAccessor httpContext, ICompanyDocumentAttachmentRepository companyDocumentAttachmentRepository)
    {
        this.companyDocumentAttachmentRepository = companyDocumentAttachmentRepository;
    }
}
