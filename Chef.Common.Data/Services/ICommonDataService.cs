﻿using Chef.Common.Authentication.Models;

namespace Chef.Common.Data.Services;

public interface ICommonDataService : IBaseService
{
    Task<IEnumerable<BranchViewModel>> GetBranches();
    Task<IEnumerable<UserBranchDto>> GetMyBranches();
    Task<IEnumerable<ReasonCodeMaster>> GetAllReasonCode();
    Task<Company> GetMyCompany();
    Task<CompanyDetails> GetCompanyDetailsForSalesInvoicePrint();
    Task<int> UpdateCompanyLogo(Company company);
    Task<int> UpdateCompany(Company company);

    Task<Company> GetCompanyDetailsForVoucherPrint();

    string ConvertToWords(string numb, string currency);
    decimal GetChartLimitAmount(decimal maxAmount);
    Task<IEnumerable<Uom>> GetAllUom();
}