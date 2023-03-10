using Chef.Common.Models;
using Chef.Finance.BP.Services;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Repositories;
using Chef.Finance.Customer.Services;
using Chef.Finance.GL.Repositories;
using Chef.Finance.GL.Repositoriesr.Repositories;
using Chef.Finance.GL.Services;
using Chef.Finance.Integration.Models;
using Chef.Finance.Models;
using Chef.Finance.Repositories;
using Chef.Finance.Services;
namespace Chef.Finance.Integration;


public class SalesOrderCreditNoteService : AsyncService<SalesReturnCreditDto>, ISalesOrderCreditNoteService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly ICustomerCreditNoteRepository customerCreditNoteRepository;
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
    private readonly ICustomerAllocationDetailRepository customerAllocationDetailRepository;
    private readonly ICustomerAllocationTransactionService customerAllocationTransactionService;
    private readonly IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository;
    private readonly IJournalBookRepository journalBookRepository;
    private readonly ITenantSimpleUnitOfWork tenantSimpleUnitOfWork;

    public SalesOrderCreditNoteService(
        ICustomerCreditNoteRepository customerCreditNoteRepository,
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
        IGeneralLedgerPostingService generalLedgerPostingService,
        ICustomerAllocationDetailRepository customerAllocationDetailRepository,
        ICustomerAllocationTransactionService customerAllocationTransactionService,
        IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository,
        IJournalBookRepository journalBookRepository,
        ITenantSimpleUnitOfWork tenantSimpleUnitOfWork
        )
    {
        this.customerCreditNoteRepository = customerCreditNoteRepository;
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
        this.generalLedgerPostingService = generalLedgerPostingService;
        this.customerAllocationDetailRepository = customerAllocationDetailRepository;
        this.customerAllocationTransactionService = customerAllocationTransactionService;
        this.journalBookNumberingSchemeRepository = journalBookNumberingSchemeRepository;
        this.journalBookRepository = journalBookRepository;
        this.tenantSimpleUnitOfWork = tenantSimpleUnitOfWork;


    }
    private CustomerAllocationDetail detail = new CustomerAllocationDetail();

    private IntegrationJournalBookConfiguration journalBookConfig = new IntegrationJournalBookConfiguration();
    public  async Task<SalesReturnCreditResponse> PostAsync(SalesReturnCreditDto salesReturnCreditDto)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            if (salesReturnCreditDto.isVanSales == true)
            {
                string code = salesReturnCreditDto.CreditNoteNumber.Substring(0, 5);

                int financialYearId = await companyFinancialYearRepository.GetFinancialYearIdByDate(salesReturnCreditDto.SalesCreditDate);

                int journalBookNumberingScheme = await journalBookNumberingSchemeRepository.GetJournalNumberingSchemeCount(financialYearId, salesReturnCreditDto.BranchId, code);

                if (journalBookNumberingScheme == 0)
                    throw new ResourceNotFoundException($"DocumentSeries not configured for this VanSalesCode:{salesReturnCreditDto.CreditNoteNumber}");

                journalBookConfig = await journalBookRepository.getJournalBookdetailsByVanSalesCode(code);
                if (journalBookConfig == null)
                    throw new ResourceNotFoundException($"Journalbook not configured for this VanSalesCode:{salesReturnCreditDto.CreditNoteNumber}");


                int updateJournalBookNumberingScheme = await journalBookNumberingSchemeRepository.UpdateJournalBookNumberingScheme(code, salesReturnCreditDto.BranchId, financialYearId, salesReturnCreditDto.CreditNoteNumber);


            }
            else
            {
                journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(Convert.ToInt32(TransactionOrgin.SalesOrder), Convert.ToInt32(TransactionType.SalesOrderReturn));

                if (journalBookConfig == null)
                    throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and type");
            }


            CustomerCreditNote customerCreditNote = Mapper.Map<CustomerCreditNote>(salesReturnCreditDto);

            if (salesReturnCreditDto.isVanSales == true)
            {
                customerCreditNote.Narration = TransactionType.SalesOrderReturn + "-" + salesReturnCreditDto.CreditNoteNumber;
            }
            else
            {
                customerCreditNote.Narration = TransactionType.SalesOrderReturn + "-" + salesReturnCreditDto.CreditNoteNumber;
            }

            customerCreditNote.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
            customerCreditNote.ApproveStatus = ApproveStatus.Draft;
            customerCreditNote.JournalBookCode = journalBookConfig.JournalBookCode;
            customerCreditNote.JournalBookId = journalBookConfig.JournalBookId;
            customerCreditNote.JournalBookName = journalBookConfig.JournalBookName;
            //customerCreditNote.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
            //customerCreditNote.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;

            CustomerCreditNoteDetail customerCreditNoteDetail = Mapper.Map<CustomerCreditNoteDetail>(salesReturnCreditDto);
            customerCreditNoteDetail.FinancialYearId = customerCreditNote.FinancialYearId;
            customerCreditNote.CreditNoteDetails = customerCreditNoteDetail;
            customerCreditNote.CustomerTransactionDetails = new();

            if (salesReturnCreditDto.salesReturnCreditItemDtos != null)
            {
                int transactionDetailNumber = 0;

                var businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(customerCreditNote.BusinessPartnerId);
                if (businessPartnerControlAccount.Count() == 0)
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
                            CreditAmount = item.NetAmount,
                            CreditAmountInBaseCurrency = item.NetAmount * customerCreditNote.ExchangeRate,
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
                            DebitAmount = item.TotalTaxAmt,
                            DebitAmountInBaseCurrency = item.TotalTaxAmt * customerCreditNote.ExchangeRate,
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
                            CreditAmount = item.DiscountAmt,
                            CreditAmountInBaseCurrency = item.DiscountAmt * customerCreditNote.ExchangeRate,
                            TotalAmount = item.DiscountAmt,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            IsControlAccount = true,
                            ControlAccountType = ControlAccountType.Discount,
                            BranchId = customerCreditNote.BranchId,
                            FinancialYearId = customerCreditNote.FinancialYearId
                        });
                    }

                    int transactionOrgin = (int)salesReturnCreditDto.TransactionOriginName;
                    int transactionType = (int)salesReturnCreditDto.TransOriginType;
                    if (item.TotalItemAmount > 0)
                    {
                        ItemViewModel viewModel = new()
                        {
                            ItemCategoryId = item.ItemCategory,
                            TransOrginId = transactionOrgin,
                            TransTypeId = transactionType,
                            ItemTypeId = item.ItemType,
                            ItemSegmentId = item.ItemSegmentId,
                            ItemFamilyId = item.ItemFamilyId,
                            ItemClassId = item.ItemClassId,
                            ItemCommodityId = item.ItemCommodityId,
                            GroupId = salesReturnCreditDto.PoGroupId
                        };
                        var ledgeraccount = await integrationControlAccountRepository.getLedgerAccountDetails(viewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.SalesRevenueAccountType));
                        if (ledgeraccount == null)
                            throw new ResourceNotFoundException($"Ledger Account not configured for this item:{salesReturnCreditDto.salesReturnCreditItemDtos.First().ItemName}-{salesReturnCreditDto.salesReturnCreditItemDtos.First().ItemCode}");

                        customerCreditNote.CustomerTransactionDetails.Add(new()
                        {
                            LineNumber = ++transactionDetailNumber,
                            LedgerAccountId = ledgeraccount.chartofaccountid,
                            LedgerAccountCode = ledgeraccount.chartofaccountcode,
                            LedgerAccountName = ledgeraccount.chartofaccountname,
                            DebitAmount = item.TotalItemAmount,
                            DebitAmountInBaseCurrency = item.TotalItemAmount * customerCreditNote.ExchangeRate,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            BranchId = customerCreditNote.BranchId,
                            FinancialYearId = customerCreditNote.FinancialYearId,
                            Narration = customerCreditNote.Narration
                        });
                    }
                }
            }

            if (salesReturnCreditDto.isVanSales == true)
            {
                customerCreditNote.DocumentNumber = salesReturnCreditDto.CreditNoteNumber;
            }

            var customerCreditNoteResult = await customerCreditNoteService.InsertCustomerCreditNote(customerCreditNote);



            CustomerAllocation customerAllocations = new CustomerAllocation();
            List<CustomerAllocationDetail> customerAllocationDetailList = new List<CustomerAllocationDetail>();
            List<CustomerAllocationOpenDocument> customerAllocationOpenDocumentsList = new List<CustomerAllocationOpenDocument>();

            customerAllocations.BranchId = salesReturnCreditDto.BranchId;
            customerAllocations.FinancialYearId = customerCreditNoteResult.FinancialYearId;
            customerAllocations.AllocationType = CustomerAllocationType.CreditNoteAllocation;
            customerAllocations.DocumentDate = DateTime.UtcNow;
            customerAllocations.TransactionDate = salesReturnCreditDto.SalesCreditDate;
            customerAllocations.BusinessPartnerId = customerCreditNoteResult.BusinessPartnerId;
            customerAllocations.BusinessPartnerCode = customerCreditNoteResult.BusinessPartnerCode;
            customerAllocations.BusinessPartnerName = customerCreditNoteResult.BusinessPartnerName;
            customerAllocations.CurrencyCode = customerCreditNoteResult.TransactionCurrencyCode;
            customerAllocations.TotalAmount = customerCreditNoteResult.TotalAmount;
            customerAllocations.TotalAmountInBaseCurrency = salesReturnCreditDto.totalAmountInBaseCurrency;
            customerAllocations.SourceDocumentNumber = customerCreditNoteResult.DocumentNumber;
            customerAllocations.ApproveStatus = ApproveStatus.Approved;
            customerAllocations.CustomerCreditNoteId = customerCreditNoteResult.Id;


            List<SalesReturnCreditItemDto> salesReturnCreditItemDtosList = new List<SalesReturnCreditItemDto>();
            salesReturnCreditItemDtosList = salesReturnCreditDto.salesReturnCreditItemDtos.GroupBy(x => x.SalesInvoiceId).Where(z => z.Key > 0).Select(y =>
            new SalesReturnCreditItemDto()
            {
                NetAmountInBaseCurrency = y.Sum(x => x.NetAmountInBaseCurrency),
                NetAmount = y.Sum(x => x.NetAmount),
                SalesInvoiceNo = y.First().SalesInvoiceNo,
            }).ToList();

            foreach (var details in salesReturnCreditItemDtosList)
            {
                detail = await customerAllocationDetailRepository.GetAllCustomerInvoiceByDocumentNumber(details.SalesInvoiceNo);
                if (detail != null)
                {

                    detail.CustomerAllocationId = customerCreditNoteResult.Id;
                    detail.IsFullAllocation = false;
                    detail.Amount = salesReturnCreditDto.NetAmount;
                    detail.AmountAllocated = details.NetAmount;
                    detail.AmountAllocatedInBaseCurrency = details.NetAmountInBaseCurrency;
                    detail.AllocationType = 0;
                    detail.IsFullSettlement = false;
                    detail.FinancialYearId = customerCreditNoteResult.FinancialYearId;
                    detail.TransactionDate = customerCreditNoteResult.TransactionDate;
                    customerAllocationDetailList.Add(detail);
                }
            }

            customerAllocations.CustomerAllocationDetails = customerAllocationDetailList;

            CustomerAllocationOpenDocument customerAllocationOpenDocument = new CustomerAllocationOpenDocument();
            customerAllocationOpenDocument.CustomerAllocationId = 0;
            customerAllocationOpenDocument.DocumentNumber = customerCreditNoteResult.DocumentNumber;
            customerAllocationOpenDocument.DocumentDate = DateTime.UtcNow;
            customerAllocationOpenDocument.DocumentId = customerCreditNoteResult.Id;
            customerAllocationOpenDocument.SourceId = customerCreditNoteResult.Id;
            customerAllocationOpenDocument.SourceNumber = customerCreditNoteResult.DocumentNumber;
            customerAllocationOpenDocument.SourceDate = DateTime.UtcNow;
            customerAllocationOpenDocument.CurrencyCode = customerAllocations.CurrencyCode;
            customerAllocationOpenDocument.ExchangeRate = customerCreditNote.ExchangeRate;
            customerAllocationOpenDocument.Amount = customerAllocations.TotalAmount;
            customerAllocationOpenDocument.AmountToAllocated = customerAllocations.CustomerAllocationDetails.Sum(x => x.AmountAllocated);
            customerAllocationOpenDocument.AmountToAllocatedInBaseCurrency = customerAllocations.CustomerAllocationDetails.Sum(x => x.AmountAllocatedInBaseCurrency);
            customerAllocationOpenDocument.BranchId = customerAllocations.BranchId;
            customerAllocationOpenDocument.FinancialYearId = customerAllocations.FinancialYearId;

            customerAllocationOpenDocumentsList.Add(customerAllocationOpenDocument);

            customerAllocations.CustomerAllocationOpenDocuments = customerAllocationOpenDocumentsList;

            if (customerAllocations.CustomerAllocationDetails.Count() > 0)
                await customerAllocationTransactionService.InsertCustomerAllocation(customerAllocations);

            CustomerTransaction doc = await customerTransactionRepository.GetByCreditNoteIdAsync(customerCreditNoteResult.Id);
            if (doc != null)
            {
                var GLPosting = await generalLedgerPostingRepository.GetGeneralLedgerBeforePostingEntries(doc.DocumentType, doc.Id);
                var GLPostingGroup = generalLedgerPostingService.GroupGLPostingByLedgerAccountId(GLPosting);

                await postDocumentViewModelRepository.PostGLAsync(GLPostingGroup);
                await customerTransactionRepository.UpdateStatus(doc.Id, ApproveStatus.Approved);
                await customerCreditNoteRepository.UpdateStatus(customerCreditNoteResult.Id, ApproveStatus.Approved);
            }
            tenantSimpleUnitOfWork.Commit();
            if (salesReturnCreditDto.isVanSales == true)
            {
                return new()
                {
                    DocumentNumber = customerCreditNoteResult.DocumentNumber
                };
            }
            return new()
            {
                DocumentNumber = customerCreditNoteResult.DocumentNumber
            };
            
        }
        catch(Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw;
        }
    }
}