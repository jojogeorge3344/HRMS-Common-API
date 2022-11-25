using Chef.Common.Services;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Services;
using Chef.Finance.Integration.Models;
using Chef.Finance.Repositories;
using Chef.Finance.Services;

namespace Chef.Finance.Integration.Services;

public class SalesOrderInvoiceService : BaseService, ISalesOrderInvoiceService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly ISalesInvoiceService salesInvoiceService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly IBusinessPartnerGroupService businessPartnerGroupService;
    private readonly ITaxAccountSetupService taxAccountSetupService;
    private readonly IGLControlAccountService glControlAccountService;
    public SalesOrderInvoiceService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        ISalesInvoiceService salesInvoiceService,
        ICompanyFinancialYearRepository companyFinancialYearRepository,
        IBusinessPartnerGroupService businessPartnerGroupService,
        ITaxAccountSetupService taxAccountSetupService,
        IGLControlAccountService glControlAccountService
        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.salesInvoiceService = salesInvoiceService;
        this.companyFinancialYearRepository = companyFinancialYearRepository;
        this.businessPartnerGroupService = businessPartnerGroupService;
        this.taxAccountSetupService = taxAccountSetupService;
        this.glControlAccountService = glControlAccountService;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SalesInvoiceDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SalesInvoiceDto> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public new async Task<string> InsertAsync(SalesInvoiceDto salesInvoiceDto)
    {
        IntegrationJournalBookConfiguration journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails("SalesOrder", "Invoice");

        if (journalBookConfig == null)
            throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and  type");

        SalesInvoice salesInvoice = Mapper.Map<SalesInvoice>(salesInvoiceDto);

        salesInvoice.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
        salesInvoice.ApproveStatus = ApproveStatus.Draft;
        salesInvoice.JournalBookCode = journalBookConfig.JournalBookCode;

        salesInvoice.OtherDetail = new()
        {
            Narration = salesInvoiceDto.Narration,
            BranchId = salesInvoice.BranchId,
            FinancialYearId = salesInvoice.FinancialYearId
        };

        if (salesInvoice.PaymentTerm != null)
        {
            salesInvoice.PaymentTerm.BranchId = salesInvoice.BranchId;
            salesInvoice.PaymentTerm.FinancialYearId = salesInvoice.FinancialYearId;
        }
        salesInvoice.CustomerTransactionDetails = new();

        if (salesInvoice.LineItems != null)
        {
            int itemLineNumber = 0;
            int transactionDetailNumber = 0;

            foreach (var item in salesInvoice.LineItems)
            {
                item.LineNumber = ++itemLineNumber;
                item.BranchId = salesInvoice.BranchId;
                item.FinancialYearId = salesInvoice.FinancialYearId;

                if (item.Amount > 0)
                {
                    var businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(salesInvoice.BusinessPartnerId);

                    if (businessPartnerControlAccount == null)
                        throw new ResourceNotFoundException("Business partner control account not found for this business partner");

                    salesInvoice.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = businessPartnerControlAccount.First().BusinessPartnerId,
                        LedgerAccountCode = businessPartnerControlAccount.First().AccountCode,
                        LedgerAccountName = businessPartnerControlAccount.First().AccountDescription,
                        DebitAmount = item.Amount,
                        DebitAmountInBaseCurrency = item.Amount * salesInvoice.ExchangeRate,
                        TotalAmount = item.TotalAmount,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Customer,
                        BranchId = salesInvoice.BranchId,
                        FinancialYearId = salesInvoice.FinancialYearId
                    });
                }

                if (item.TaxAmount > 0)
                {
                    var salesTaxAccount = await taxAccountSetupService.GetSalesTaxAccountAsync();

                    if (salesTaxAccount == null)
                        throw new ResourceNotFoundException("Sales tax account not found");

                    salesInvoice.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = salesTaxAccount.Id,
                        LedgerAccountCode = salesTaxAccount.Code,
                        LedgerAccountName = salesTaxAccount.Description,
                        CreditAmount = item.TaxAmount,
                        CreditAmountInBaseCurrency = item.TaxAmount * salesInvoice.ExchangeRate,
                        TotalAmount = item.TaxAmount,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Tax,
                        BranchId = salesInvoice.BranchId,
                        FinancialYearId = salesInvoice.FinancialYearId
                    });
                }

                if (item.DiscountAmount > 0)
                {
                    var chartOfAccount = await glControlAccountService.GetSalesInvoiceDiscountGLControlAccountsAsync();

                    if (chartOfAccount == null)
                        throw new ResourceNotFoundException("Control account not configured for sales invoice discount");

                    salesInvoice.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = chartOfAccount.Id,
                        LedgerAccountCode = chartOfAccount.Code,
                        LedgerAccountName = chartOfAccount.Description,
                        DebitAmount = item.DiscountAmount,
                        DebitAmountInBaseCurrency = item.DiscountAmount * salesInvoice.ExchangeRate,
                        TotalAmount = item.DiscountAmount,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Discount,
                        BranchId = salesInvoice.BranchId,
                        FinancialYearId = salesInvoice.FinancialYearId
                    });
                }
            }
        }
        int salesInvoiceId = await salesInvoiceService.InsertAsync(salesInvoice);
        var response = await salesInvoiceService.GetAsync(salesInvoiceId);
        return response.DocumentNumber;
    }

    public Task<int> UpdateAsync(SalesInvoiceDto obj)
    {
        throw new NotImplementedException();
    }

    Task<int> IAsyncService<SalesInvoiceDto>.InsertAsync(SalesInvoiceDto obj)
    {
        throw new NotImplementedException();
    }
}
