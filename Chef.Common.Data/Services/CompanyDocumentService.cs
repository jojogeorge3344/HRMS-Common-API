using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public class CompanyDocumentService : AsyncService<ComapnyDocuments>, ICompanyDocumentService
{
    private readonly ICompanyDocumentRepostory companyDocumentRepostory;
    private readonly ITenantSimpleUnitOfWork tenantSimpleUnitOfWork;
    private readonly IHttpContextAccessor httpContext;
    private readonly ICompanyDocumentAttachmentRepository companyDocumentAttachmentRepository;

    public CompanyDocumentService(ICompanyDocumentRepostory companyDocumentRepostory, ITenantSimpleUnitOfWork tenantSimpleUnitOfWork, IHttpContextAccessor httpContext, ICompanyDocumentAttachmentRepository companyDocumentAttachmentRepository)
    {
        this.companyDocumentRepostory = companyDocumentRepostory;
        this.tenantSimpleUnitOfWork = tenantSimpleUnitOfWork;
        this.httpContext = httpContext;
        this.companyDocumentAttachmentRepository = companyDocumentAttachmentRepository;
    }
    public async Task<int> Insert(ComapnyDocuments comapnyDocuments)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            comapnyDocuments.Id = await companyDocumentRepostory.InsertAsync(comapnyDocuments);
            if(comapnyDocuments.IsAttachment == true)
            {
                int count = httpContext.HttpContext.Request.Form.Files.Count();
                for (int i = 0; i < count; i++)
                {
                    IFormFile file = httpContext.HttpContext.Request.Form.Files[i];
                    using MemoryStream ms = new();
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    CompanyDocumentAttachment companyDocumentAttachment = new()
                    {
                        FileName = file.FileName,
                        ComapnyDocumentId = comapnyDocuments.Id,
                        AttachmentByte = fileBytes,
                    };
                    await companyDocumentAttachmentRepository.InsertAsync(companyDocumentAttachment);
                }
            }
            tenantSimpleUnitOfWork.Commit();
            return 1;

        }
        catch (Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw ex;
        }
    }

    public async Task<int> Update(ComapnyDocuments comapnyDocuments)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            if(comapnyDocuments.Id > 0)
            {
                await companyDocumentRepostory.UpdateAsync(comapnyDocuments);
            }
            else
            {
                comapnyDocuments.Id =  await companyDocumentRepostory.InsertAsync(comapnyDocuments);
            }
            int count = httpContext.HttpContext.Request.Form.Files.Count();
            if(count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    IFormFile file = httpContext.HttpContext.Request.Form.Files[i];
                    using MemoryStream ms = new();
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    CompanyDocumentAttachment companyDocumentAttachment = new()
                    {
                        FileName = file.FileName,
                        ComapnyDocumentId = comapnyDocuments.Id,
                        AttachmentByte = fileBytes,
                    };
                    await companyDocumentAttachmentRepository.InsertAsync(companyDocumentAttachment);
                }
            }
            tenantSimpleUnitOfWork.Commit();
            return 1;
        }
        catch(Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw;
        }
    }
}
