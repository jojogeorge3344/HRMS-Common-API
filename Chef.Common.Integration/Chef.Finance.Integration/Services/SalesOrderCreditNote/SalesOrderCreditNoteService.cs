using Chef.Finance.BP.Services;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Repositories;
using Chef.Finance.Customer.Services;
using Chef.Finance.GL.Repositories;
using Chef.Finance.GL.Repositoriesr.Repositories;
using Chef.Finance.GL.Services;
using Chef.Finance.Integration.Models;
using Chef.Finance.Repositories;
using Chef.Finance.Services;
namespace Chef.Finance.Integration;


public class SalesOrderCreditNoteService : AsyncService<SalesReturnCreditDto>, ISalesOrderCreditNoteService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly ICustomerCreditNoteService customerCreditNoteService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly IBusinessPartnerGroupService businessPartnerGroupService;
    private readonly ITaxAccountSetupService taxAccountSetupService;
    private readonly IGLControlAccountService glControlAccountService;
    private readonly IIntegrationControlAccountRepository integrationControlAccountRepository;
    private readonly ICustomerTransactionRepository customerTransactionRepository;
    private readonly IGeneralLedgerPostingRepository generalLedgerPostingRepository;
    private readonly IPostDocumentViewModelRepository postDocumentViewModelRepository;
    private readonly IGeneralLedgerPostingService generalLedgerPostingService;

    public SalesOrderCreditNoteService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        ICustomerCreditNoteService customerCreditNoteService,
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
        this.customerCreditNoteService = customerCreditNoteService;
        this.companyFinancialYearRepository = companyFinancialYearRepository;
        this.businessPartnerGroupService = businessPartnerGroupService;
        this.taxAccountSetupService = taxAccountSetupService;
        this.glControlAccountService = glControlAccountService;
        this.integrationControlAccountRepository = integrationControlAccountRepository;
        this.customerTransactionRepository = customerTransactionRepository;
        this.generalLedgerPostingRepository = generalLedgerPostingRepository;
        this.postDocumentViewModelRepository = postDocumentViewModelRepository;
    }
    public  async Task<string> PostAsync(SalesReturnCreditDto salesReturnCreditDto)
    {
        IntegrationJournalBookConfiguration journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(TransactionOrgin.SalesOrder.ToString(), TransactionType.Return.ToString());

        if (journalBookConfig == null)
            throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and type");

        CustomerCreditNote customerCreditNote = Mapper.Map<CustomerCreditNote>(salesReturnCreditDto);

        customerCreditNote.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
        customerCreditNote.ApproveStatus = ApproveStatus.Draft;
        customerCreditNote.JournalBookCode = journalBookConfig.JournalBookCode;
        customerCreditNote.JournalBookId = journalBookConfig.JournalBookId;
        customerCreditNote.JournalBookName = journalBookConfig.JournalBookName;
        customerCreditNote.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
        customerCreditNote.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;

        CustomerCreditNoteDetail customerCreditNoteDetail = Mapper.Map<CustomerCreditNoteDetail>(salesReturnCreditDto);
        customerCreditNote.CreditNoteDetails = customerCreditNoteDetail;
        customerCreditNote.CustomerTransactionDetails = new();

        if (salesReturnCreditDto.salesReturnCreditItemDtos != null)
        {
            int transactionDetailNumber = 0;

            var businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(customerCreditNote.BusinessPartnerId);
            if (businessPartnerControlAccount == null)
                throw new ResourceNotFoundException("Business partner control account not found for this business partner");

            var salesTaxAccount = await taxAccountSetupService.GetSalesTaxAccountAsync();
            if (salesTaxAccount == null)
                throw new ResourceNotFoundException("Sales tax account not found");

            var chartOfAccount = await glControlAccountService.GetSalesInvoiceDiscountGLControlAccountsAsync();
            if (chartOfAccount == null)
                throw new ResourceNotFoundException("Control account not configured for sales invoice discount");

            foreach (var item in salesReturnCreditDto.salesReturnCreditItemDtos)
            {
                if (item.NetAmount > 0)
                {
                    customerCreditNote.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = businessPartnerControlAccount.First().AccountId,
                        LedgerAccountCode = businessPartnerControlAccount.First().AccountCode,
                        LedgerAccountName = businessPartnerControlAccount.First().AccountDescription,
                        DebitAmount = item.NetAmount,
                        DebitAmountInBaseCurrency = item.NetAmount * customerCreditNote.ExchangeRate,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Customer,
                        BranchId = customerCreditNote.BranchId,
                        FinancialYearId = customerCreditNote.FinancialYearId
                    });
                }

                if (item.TotalTaxAmt > 0)
                {
                    customerCreditNote.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = salesTaxAccount.Id,
                        LedgerAccountCode = salesTaxAccount.Code,
                        LedgerAccountName = salesTaxAccount.Description,
                        CreditAmount = item.TotalTaxAmt,
                        CreditAmountInBaseCurrency = item.TotalTaxAmt * customerCreditNote.ExchangeRate,
                        TotalAmount = item.TotalTaxAmt,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Tax,
                        BranchId = customerCreditNote.BranchId,
                        FinancialYearId = customerCreditNote.FinancialYearId
                    });
                }

                if (item.DiscountAmt > 0)
                {
                    customerCreditNote.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = chartOfAccount.Id,
                        LedgerAccountCode = chartOfAccount.Code,
                        LedgerAccountName = chartOfAccount.Description,
                        DebitAmount = item.DiscountAmt,
                        DebitAmountInBaseCurrency = item.DiscountAmt * customerCreditNote.ExchangeRate,
                        TotalAmount = item.DiscountAmt,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        IsControlAccount = true,
                        ControlAccountType = ControlAccountType.Discount,
                        BranchId = customerCreditNote.BranchId,
                        FinancialYearId = customerCreditNote.FinancialYearId
                    });
                }

               
                if (item.TotalItemAmount > 0)
                {
                    ItemViewModel viewModel = new()
                    {
                        ItemCategoryId = item.ItemCategory,
                        ItemTypeId = item.ItemType,
                        ItemSegmentId = item.ItemSegmentId,
                        ItemFamilyId = item.ItemFamilyId,
                        ItemClassId = item.ItemClassId,
                        ItemCommodityId = item.ItemCommodityId
                    };
                    var ledgeraccount = await integrationControlAccountRepository.getLedgerAccountDetails(viewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.SalesRevenueAccountType));

                    customerCreditNote.CustomerTransactionDetails.Add(new()
                    {
                        LineNumber = ++transactionDetailNumber,
                        LedgerAccountId = ledgeraccount.chartofaccountid,
                        LedgerAccountCode = ledgeraccount.chartofaccountcode,
                        LedgerAccountName = ledgeraccount.chartofaccountname,
                        CreditAmount = item.TotalItemAmount,
                        CreditAmountInBaseCurrency = item.TotalItemAmount * customerCreditNote.ExchangeRate,
                        CostAllocationCode = "NA",
                        CostAllocationDescription = "No Cost Allocation",
                        BranchId = customerCreditNote.BranchId,
                        FinancialYearId = customerCreditNote.FinancialYearId
                    });
                }
            }
        }

        var customerCreditNoteResult = await customerCreditNoteService.InsertAsync(customerCreditNote);

        CustomerTransaction doc = await customerTransactionRepository.GetByCreditNoteIdAsync(customerCreditNoteResult.Id);
        if (doc != null)
        {
            var GLPosting = await generalLedgerPostingRepository.GetGeneralLedgerBeforePostingEntries(doc.DocumentType, doc.Id);
            var GLPostingGroup = generalLedgerPostingService.GroupGLPostingByLedgerAccountId(GLPosting);

            await postDocumentViewModelRepository.PostGLAsync(GLPostingGroup);
        }
        return customerCreditNoteResult.DocumentNumber;
    }
}