using Chef.Common.Authentication.Models;
using Chef.Common.Data.Repositories;
using Chef.Common.Models;

namespace Chef.Common.Data.Services;

public class CommonDataService : ICommonDataService
{
    private readonly ICommonDataRepository commonDataRepository;

    public CommonDataService(ICommonDataRepository commonDataRepository)
    {
        this.commonDataRepository = commonDataRepository;
    }

    public Task<IEnumerable<BranchViewModel>> GetBranches()
    {
        return commonDataRepository.GetBranches();
    }

    public async Task<IEnumerable<UserBranchDto>> GetMyBranches()
    {
        return await commonDataRepository.GetBranches(HttpHelper.Username);
    }
    public async Task<Company> GetMyCompany()
    {
        var company = await commonDataRepository.GetMyCompany();

        if (company != null && company.Logo != null)
        {
            company.LogoEncoded = System.Text.Encoding.UTF8.GetString(company.Logo);
            company.Logo = null;
        }

        return company;
    }

    public Task<IEnumerable<ReasonCodeMaster>> GetAllReasonCode()
    {
        return commonDataRepository.GetAllReasonCode();
    }

    public Task<CompanyDetails> GetCompanyDetailsForSalesInvoicePrint()
    {
        return commonDataRepository.GetCompanyDetailsForSalesInvoicePrint();
    }
    public async Task<int> UpdateCompanyLogo(Company company)
    {
        if (company.LogoEncoded != null)
            company.Logo = System.Text.Encoding.UTF8.GetBytes(company.LogoEncoded);

        return await commonDataRepository.UpdateCompanyLogo(company);
    }
}

