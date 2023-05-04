using Dapper;
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

    public async Task<int> deleteAttachment(int id)
    {
        return await QueryFactory
            .Query<CompanyDocumentAttachment>()
            .Where(" companydocumentid", id)
            .WhereNotArchived()
            .UpdateAsync(new
            {
                isarchived = true
            });
    }

    public async Task<IEnumerable<CompanyDocumentAttachment>> GetAttachmentDetails(int companyId)
    {
        string sql = @"SELECT     ct.id,
                                   ct.companydocumentid,
                                   ct.filename,
                                   ct.attachmentbyte
                        FROM       common.companydocumentattachment ct
                        INNER JOIN common.companydocuments cd
                        ON         cd.id = ct.comapnydocumentid
                        WHERE      ct.isarchived = false
                        AND        cd.isarchived = false
                        AND        cd.companyid = @companyId";
        return await Connection.QueryAsync<CompanyDocumentAttachment>(sql, new { companyId });  
    }
}
