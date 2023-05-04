using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public class CompanyDocumentService : AsyncService<CompanyDocuments>, ICompanyDocumentService
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

    public async Task<int> Delete(int id)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            int deleteId = await companyDocumentRepostory.DeleteAsync(id);
            if (deleteId > 0)
            {
                await companyDocumentAttachmentRepository.deleteAttachment(id);
            }
            tenantSimpleUnitOfWork.Commit();
            return deleteId;
        }
        catch (Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw ex;
        }
    }

    public async Task<IEnumerable<CompanyDocuments>> GetCompanyDocumentDetails(int companyId)
    {
        IEnumerable<CompanyDocuments> comapnyDocuments = await companyDocumentRepostory.GetCompanyDocuments(companyId);
        IEnumerable<CompanyDocumentAttachment> documentAttachments = (await companyDocumentAttachmentRepository.GetAttachmentDetails(companyId)).ToList();
        foreach (CompanyDocuments documents in comapnyDocuments)
        {
            IEnumerable<CompanyDocumentAttachment> attachments = documentAttachments.Where(x => x.CompanyDocumentId == documents.Id).ToList();
            documents.companyDocumentAttachments = attachments.ToList();
        }
        return comapnyDocuments;
    }

    public async Task<int> Insert(CompanyDocuments companyDocuments)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            companyDocuments.Id = await companyDocumentRepostory.InsertAsync(companyDocuments);
            if(companyDocuments.IsAttachment == true)
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
                        CompanyDocumentId = companyDocuments.Id,
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

    public async Task<int> Update(CompanyDocuments companyDocuments)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            if(companyDocuments.Id > 0)
            {
                await companyDocumentRepostory.UpdateAsync(companyDocuments);
            }
            else
            {
                companyDocuments.Id =  await companyDocumentRepostory.InsertAsync(companyDocuments);
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
                        CompanyDocumentId = companyDocuments.Id,
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
