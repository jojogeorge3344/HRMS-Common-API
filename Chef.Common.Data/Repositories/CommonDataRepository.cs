using Chef.Common.Authentication.Models;
using Chef.Common.Core.Extensions;
using Chef.Common.Models;
using Chef.Common.Repositories;
using SqlKata;

namespace Chef.Common.Data.Repositories;

public class CommonDataRepository : TenantRepository<Model>, ICommonDataRepository
{
    public CommonDataRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory tenantConnectionFactory)
        : base(httpContextAccessor, tenantConnectionFactory)
    {
    }

    public async Task<IEnumerable<BranchViewModel>> GetBranches()
    {
        return await QueryFactory
            .Query<Branch>()
            .Select("id as BranchId", "name as BranchName", "code as BranchCode", "companycode")      
            .WhereNotArchived()
            .GetAsync<BranchViewModel>();
    }

    public async Task<IEnumerable<UserBranchDto>> GetBranches(string userName)
    {
        return await QueryFactory
              .Query<UserBranch>().Join("common.branch", "branch.id", "userbranch.branchid")
              .Select(
                  "userbranch.username",
                  "userbranch.branchid",
                  "userbranch.IsDefault",
                  "branch.name as BranchName",
                  "branch.code as BranchCode")
              .Where(new
              {
                  username = userName,

              })
              .WhereFalse("branch.isArchived")
             .WhereFalse("userbranch.isArchived")
             .GetAsync<UserBranchDto>();
    }


    public async Task<Company> GetMyCompany()
    {
        return await QueryFactory
                 .Query<Company>()
                 .WhereNotArchived()
                .FirstOrDefaultAsync<Company>();
    }


    public async Task<IEnumerable<ReasonCodeMaster>> GetAllReasonCode()
    {
        return await QueryFactory
            .Query<ReasonCodeMaster>()
            .Select("reasoncodemaster.reasoncode", "reasoncodemaster.remarks")
            .WhereNotArchived()
            .GetAsync<ReasonCodeMaster>();
    }
    public async Task<CompanyDetails> GetCompanyDetailsForSalesInvoicePrint()
    {
        string sql = @"SELECT 'P.O. Box:'
                                   || COALESCE(zipcode, '')
                                   ||chr(13)||chr(10)||'Email:'
                                   || COALESCE(email, '')
                                   || ' Tel. :'
                                   || COALESCE(phone, '')
                                   || ' Fax :'
                                   || COALESCE(fax, '')
                                    AS CompanyDetail,
                                    name as CompanyName,
                                    taxregistrationnumber as TRNNo,
                                    encode(logo::bytea, 'escape') as logo
                            FROM   common.company";

        return await DatabaseSession.QueryFirstAsync<CompanyDetails>(sql);
    }

    public async Task<int> UpdateCompanyLogo(Company company)
    {
        return await QueryFactory
                 .Query<Company>()
                 .Where("id", company.Id)
                 .UpdateAsync(new
                 {
                     logo = company.Logo
                 });
    }


    public async Task<Company> GetCompanyDetailsForVoucherPrint()
    {
        string sql = @"SELECT zipcode,
                               email,
                               phone,
                               name as CompanyName,
                               fax,
                               Encode(logo :: bytea, 'escape') AS LogoEncoded,
                               taxregistrationnumber
                        FROM   common.company ";
        return await DatabaseSession.QueryFirstAsync<Company>(sql);

    }


    public async Task<int> UpdateCompany(Company company)
    {
        return await QueryFactory
                 .Query<Company>()
                 .Where("id", company.Id)
                 .UpdateAsync(company);
    }
}