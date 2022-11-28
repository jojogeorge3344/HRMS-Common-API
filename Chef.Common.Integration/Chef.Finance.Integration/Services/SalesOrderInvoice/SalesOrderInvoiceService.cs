using Chef.Common.Services;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Repositories;
using Chef.Finance.Customer.Services;
using Chef.Finance.GL.Repositories;
using Chef.Finance.GL.Services;
using Chef.Finance.Integration.Models;
using Chef.Finance.Repositories;
using Chef.Finance.Services;

namespace Chef.Finance.Integration;

public class SalesOrderInvoiceService : BaseService, ISalesOrderInvoiceService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly ISalesInvoiceService salesInvoiceService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly IBusinessPartnerGroupService businessPartnerGroupService;
    private readonly ITaxAccountSetupService taxAccountSetupService;
    private readonly IGLControlAccountService glControlAccountService;
    private readonly IIntegrationControlAccountRepository integrationControlAccountRepository;
    private readonly ICustomerTransactionRepository customerTransactionRepository;
    private readonly IGeneralLedgerPostingRepository generalLedgerPostingRepository;
    private readonly IPostDocumentViewModelRepository postDocumentViewModelRepository;
    private readonly IGeneralLedgerPostingService generalLedgerPostingService;

    public SalesOrderInvoiceService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        ISalesInvoiceService salesInvoiceService,
        ICompanyFinancialYearRepository companyFinancialYearRepository,
        IBusinessPartnerGroupService businessPartnerGroupService,
        ITaxAccountSetupService taxAccountSetupService,
        IGLControlAccountService glControlAccountService,
        IIntegrationControlAccountRepository integrationControlAccountRepository,
        ICustomerTransactionRepository customerTransactionRepository,
        IGeneralLedgerPostingRepository generalLedgerPostingRepository,
        IPostDocumentViewModelRepository postDocumentViewModelRepository,
        IGeneralLedgerPostingService generalLedgerPostingService
        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.salesInvoiceService = salesInvoiceService;
        this.companyFinancialYearRepository = companyFinancialYearRepository;
        this.businessPartnerGroupService = businessPartnerGroupService;
        this.taxAccountSetupService = taxAccountSetupService;
        this.glControlAccountService = glControlAccountService;
        this.integrationControlAccountRepository = integrationControlAccountRepository;
        this.customerTransactionRepository = customerTransactionRepository;
        this.generalLedgerPostingRepository = generalLedgerPostingRepository;
        this.postDocumentViewModelRepository = postDocumentViewModelRepository;
        this.generalLedgerPostingService = generalLedgerPostingService;
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

    public async Task<string> InsertAsync(SalesInvoiceDto salesInvoiceDto)
    {
        IntegrationJournalBookConfiguration journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(TransactionOrgin.SalesOrder.ToString(), TransactionType.Invoice.ToString());

        if (journalBookConfig == null)
            throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and type");

        SalesInvoice salesInvoice = Mapper.Map<SalesInvoice>(salesInvoiceDto);

        salesInvoice.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
        salesInvoice.ApproveStatus = ApproveStatus.Draft;
        salesInvoice.JournalBookCode = journalBookConfig.JournalBookCode;
        salesInvoice.JournalBookId = journalBookConfig.JournalBookId;
        salesInvoice.JournalBookName = journalBookConfig.JournalBookName;
        salesInvoice.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
        salesInvoice.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;

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

            var businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(salesInvoice.BusinessPartnerId);
            if (businessPartnerControlAccount == null)
                throw new ResourceNotFoundException("Business partner control account not found for this business partner");

            var salesTaxAccount = await taxAccountSetupService.GetSalesTaxAccountAsync();
            if (salesTaxAccount == null)
                throw new ResourceNotFoundException("Sales tax account not found");

            var chartOfAccount = await glControlAccountService.GetSalesInvoiceDiscountGLControlAccountsAsync();
            if (chartOfAccount == null)
                throw new ResourceNotFoundException("Control account not configured for sales invoice discount");

            foreach (var item in salesInvoice.LineItems)
            {
                item.LineNumber = ++itemLineNumber;
                item.BranchId = salesInvoice.BranchId;
                item.FinancialYearId = salesInvoice.FinancialYearId;

                if (item.TotalAmount > 0)
                {
                    salesInvoice.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = businessPartnerControlAccount.First().AccountId,
                        LedgerAccountCode = businessPartnerControlAccount.First().AccountCode,
                        LedgerAccountName = businessPartnerControlAccount.First().AccountDescription,
                        DebitAmount = item.TotalAmount,
                        DebitAmountInBaseCurrency = item.TotalAmount * salesInvoice.ExchangeRate,
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

                var itemDto = salesInvoiceDto.SalesInvoiceItemDto[itemLineNumber - 1];
                if (item.Amount > 0)
                {
                    ItemViewModel viewModel = new()
                    {
                        ItemCategoryId = itemDto.ItemCategory,
                        ItemTypeId = itemDto.ItemType,
                        ItemSegmentId = itemDto.ItemSegmentId,
                        ItemFamilyId = itemDto.ItemFamilyId,
                        ItemClassId = itemDto.ItemClassId,
                        ItemCommodityId = itemDto.ItemCommodityId
                    };
                    var ledgeraccount = await integrationControlAccountRepository.getLedgerAccountDetails(viewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.SalesRevenueAccountType));

                    salesInvoice.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = ledgeraccount.chartofaccountid,
                        LedgerAccountCode = ledgeraccount.chartofaccountcode,
                        LedgerAccountName = ledgeraccount.chartofaccountname,
                        CreditAmount = item.Amount,
                        CreditAmountInBaseCurrency = item.Amount * salesInvoice.ExchangeRate,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        BranchId = salesInvoice.BranchId,
                        FinancialYearId = salesInvoice.FinancialYearId
                    });
                }
            }
        }

        var salesInvoiceResponse = await salesInvoiceService.InsertAsync(salesInvoice);

        CustomerTransaction doc = await customerTransactionRepository.GetByInvoiceIdAsync(salesInvoiceResponse.Id);
        if (doc != null)
        {
            var GLPosting = await generalLedgerPostingRepository.GetGeneralLedgerBeforePostingEntries(doc.DocumentType, doc.Id);
            var GLPostingGroup = generalLedgerPostingService.GroupGLPostingByLedgerAccountId(GLPosting);

            await postDocumentViewModelRepository.PostGLAsync(GLPostingGroup);
        }
        return salesInvoiceResponse.DocumentNumber;
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
