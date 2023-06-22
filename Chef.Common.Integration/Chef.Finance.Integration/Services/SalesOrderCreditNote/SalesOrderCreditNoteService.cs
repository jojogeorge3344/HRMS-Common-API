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
    private readonly ICustomerTransactionService customerTransactionService;
    private readonly ICustomerTransactionDetailService customerTransactionDetailService;
    private readonly ICustomerAllocationTransactionRepository customerAllocationTransactionRepository;
    private readonly IPurchaseControlAccountService purchaseControlAccountService;
    private readonly IDimensionMasterRepository dimensionMasterRepository;
    private readonly IDimensionRepository dimensionRepository;
    private readonly IBranchService branchService;

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
        ITenantSimpleUnitOfWork tenantSimpleUnitOfWork,
        ICustomerTransactionService customerTransactionService,
        ICustomerTransactionDetailService customerTransactionDetailService,
        ICustomerAllocationTransactionRepository customerAllocationTransactionRepository,
        IPurchaseControlAccountService purchaseControlAccountService,
        IDimensionMasterRepository dimensionMasterRepository,
        IDimensionRepository dimensionRepository,
        IBranchService branchService
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
        this.customerTransactionService = customerTransactionService;
        this.customerTransactionDetailService = customerTransactionDetailService;
        this.customerAllocationTransactionRepository = customerAllocationTransactionRepository;
        this.purchaseControlAccountService = purchaseControlAccountService;
        this.dimensionMasterRepository = dimensionMasterRepository;
        this.dimensionRepository = dimensionRepository;
        this.branchService = branchService;


    }
    private CustomerAllocationDetail detail = new CustomerAllocationDetail();

    private IntegrationJournalBookConfiguration journalBookConfig = new IntegrationJournalBookConfiguration();


    public async Task<SalesReturnCreditResponse> ViewSalesCreditReturn(SalesReturnCreditDto salesReturnCreditDto)
    {
        try
        {
            SalesReturnCreditResponse salesReturnCreditResponse = await PostAsync(salesReturnCreditDto,false);
            return salesReturnCreditResponse;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<SalesReturnCreditResponse> Post(SalesReturnCreditDto salesReturnCreditDto)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            SalesReturnCreditResponse salesReturnCreditResponse = await PostAsync(salesReturnCreditDto);
            tenantSimpleUnitOfWork.Commit();
            return salesReturnCreditResponse;
        }
        catch (Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw;
        }
    }

    bool posting = true;


    private  async Task<SalesReturnCreditResponse> PostAsync(SalesReturnCreditDto salesReturnCreditDto,bool IsPosting = true)
    {
        CustomerCreditNote customerCreditNoteResult = new CustomerCreditNote();
        IEnumerable<BusinessPartnerControlAccountViewModel> businessPartnerControlAccount = new List<BusinessPartnerControlAccountViewModel>();
        LedgerAccountViewModel purchaseControlAccount = new LedgerAccountViewModel();
        int orgin = 0;
        int type = 0;

        int financialYearId = await companyFinancialYearRepository.GetFinancialYearIdByDate(salesReturnCreditDto.SalesCreditDate.Date);

        if (salesReturnCreditDto.isVanSales == true && salesReturnCreditDto.CreditNoteNumber != null)
            {
                    string code = salesReturnCreditDto.CreditNoteNumber.Substring(0, 5);

            //int financialYearId = await companyFinancialYearRepository.GetFinancialYearIdByDate(salesReturnCreditDto.SalesCreditDate);
            //As per discussion For Vansales no need Po Group journal book checking
            int journalBookNumberingScheme = await journalBookNumberingSchemeRepository.GetJournalNumberingSchemeCount(financialYearId, salesReturnCreditDto.BranchId, code);

                    if (journalBookNumberingScheme == 0)
                        throw new ResourceNotFoundException($"DocumentSeries not configured for this VanSalesCode:{salesReturnCreditDto.CreditNoteNumber}");

                    journalBookConfig = await journalBookRepository.getJournalBookdetailsByVanSalesCode(code);
                    if (journalBookConfig == null)
                        throw new ResourceNotFoundException($"Journalbook not configured for this VanSalesCode:{salesReturnCreditDto.CreditNoteNumber}");

                if (!salesReturnCreditDto.CreditNoteNumber.Contains(".") && !salesReturnCreditDto.CreditNoteNumber.Contains("_"))
                {
                    int updateJournalBookNumberingScheme = await journalBookNumberingSchemeRepository.UpdateJournalBookNumberingScheme(code, salesReturnCreditDto.BranchId, financialYearId, salesReturnCreditDto.CreditNoteNumber);
                }
            }
            if (salesReturnCreditDto.TransOriginType == TransactionType.RetailSalesOrderReturnCredit)
            {
               orgin = (int)TransactionOrgin.RetailSalesOrder;
               type = (int)TransactionType.RetailSalesOrderReturnCredit;
            }
            else if(salesReturnCreditDto.TransOriginType == TransactionType.RetailSalesOrderReturnCash)
            {
                orgin = (int)TransactionOrgin.RetailSalesOrder;
                type = (int)TransactionType.RetailSalesOrderReturnCash;
            }
            else if(salesReturnCreditDto.TransOriginType == TransactionType.SalesOrderReturn)
            {
                orgin = (int)TransactionOrgin.SalesOrder;
                type = (int)TransactionType.SalesOrderReturn;
            }
            else if (salesReturnCreditDto.TransOriginType == TransactionType.VanSalesOrderReturn && salesReturnCreditDto.CreditNoteNumber == null)
            {
                orgin = (int)TransactionOrgin.VanSalesOrder;
                type = (int)TransactionType.VanSalesOrderReturn;
                journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(orgin, type, salesReturnCreditDto.PoGroupId);
            }
           if (salesReturnCreditDto.isVanSales != true)
              journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(orgin, type, salesReturnCreditDto.PoGroupId);

            if (journalBookConfig == null)
                throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and type");

            CustomerCreditNote customerCreditNote = Mapper.Map<CustomerCreditNote>(salesReturnCreditDto);

            if (salesReturnCreditDto.isVanSales == true)
            {
                customerCreditNote.Narration = TransactionType.SalesOrderReturn + "-" + salesReturnCreditDto.CreditNoteNumber;
            }
            else if(salesReturnCreditDto.TransOriginType == TransactionType.RetailSalesOrderReturnCredit)
            {
                customerCreditNote.Narration = TransactionType.RetailSalesOrderReturnCredit + "-" + salesReturnCreditDto.CreditNoteNumber;
            }
            else if (salesReturnCreditDto.TransOriginType == TransactionType.RetailSalesOrderReturnCash)
            {
                customerCreditNote.Narration = TransactionType.RetailSalesOrderReturnCash + "-" + salesReturnCreditDto.CreditNoteNumber;
            }
            else
            {
                customerCreditNote.Narration = TransactionType.SalesOrderReturn + "-" + salesReturnCreditDto.CreditNoteNumber;
            }

            customerCreditNote.FinancialYearId = financialYearId;
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

                if (salesReturnCreditDto.IsCashSales != true) 
                {
                    businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(customerCreditNote.BusinessPartnerId);
                    if (businessPartnerControlAccount.Count() == 0)
                        throw new ResourceNotFoundException("Business partner control account not found for this business partner");
                }
                else
                {
                   purchaseControlAccount =  await purchaseControlAccountService.GetCashSuspenseAccount();
                   if (purchaseControlAccount == null)
                       throw new ResourceNotFoundException("Cash Suspense control account not Configured");
                }

                var salesTaxAccount = await taxAccountSetupService.GetTaxAccountDimension();
                if (salesTaxAccount == null)
                    throw new ResourceNotFoundException("Sales tax account not found");

                var chartOfAccount = await glControlAccountService.GetSalesDiscountAccountDimension();
                if (chartOfAccount == null)
                    throw new ResourceNotFoundException("Control account not configured for sales invoice discount");

                foreach (var item in salesReturnCreditDto.salesReturnCreditItemDtos)
                {
                    if (item.NetAmount > 0)
                    {
                        if (salesReturnCreditDto.IsCashSales != true)
                        {
                            List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                            BusinessPartnerControlAccountViewModel firstBusinessPartner = businessPartnerControlAccount.First();
                            if (firstBusinessPartner.isdimension1 || firstBusinessPartner.isdimension2 || firstBusinessPartner.isdimension3 || firstBusinessPartner.isdimension4 || firstBusinessPartner.isdimension5 || firstBusinessPartner.isdimension6)
                            {
                                for (int i = 1; i <= 6; i++)
                                {
                                    bool isDimension = (bool)businessPartnerControlAccount.GetType().GetProperty($"isdimension{i}").GetValue(businessPartnerControlAccount);
                                    if (isDimension)
                                    {
                                        string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                        dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesReturnCreditDto, item.NetAmount, customerCreditNote.ExchangeRate, customerCreditNote.BranchId, customerCreditNote.FinancialYearId));
                                    }
                                }
                            }
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
                                FinancialYearId = customerCreditNote.FinancialYearId,
                                ItemId = item.ItemId
                            });
                        }
                        else
                        {
                            List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                            if (purchaseControlAccount.isdimension1 || purchaseControlAccount.isdimension2 || purchaseControlAccount.isdimension3 || purchaseControlAccount.isdimension4 || purchaseControlAccount.isdimension5 || purchaseControlAccount.isdimension6)
                            {
                                for (int i = 1; i <= 6; i++)
                                {
                                    bool isDimension = (bool)purchaseControlAccount.GetType().GetProperty($"isdimension{i}").GetValue(purchaseControlAccount);
                                    if (isDimension)
                                    {
                                        string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                        dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesReturnCreditDto, item.NetAmount, customerCreditNote.ExchangeRate, customerCreditNote.BranchId, customerCreditNote.FinancialYearId));
                                    }
                                }
                            }
                            customerCreditNote.CustomerTransactionDetails.Add(new()
                            {
                                LineNumber = ++transactionDetailNumber,
                                LedgerAccountId = purchaseControlAccount.chartofaccountid,
                                LedgerAccountCode = purchaseControlAccount.chartofaccountcode,
                                LedgerAccountName = purchaseControlAccount.chartofaccountname,
                                CreditAmount = item.NetAmount,
                                CreditAmountInBaseCurrency = item.NetAmount * customerCreditNote.ExchangeRate,
                                CostAllocationCode = "NA",
                                CostAllocationDescription = "No Cost Allocation",
                                IsControlAccount = true,
                                ControlAccountType = ControlAccountType.Integration,
                                BranchId = customerCreditNote.BranchId,
                                FinancialYearId = customerCreditNote.FinancialYearId,
                                ItemId = item.ItemId
                            });
                        }
                    }

                    if (item.TotalTaxAmt > 0)
                    {
                        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                        if (salesTaxAccount.isdimension1 || salesTaxAccount.isdimension2 || salesTaxAccount.isdimension3 || salesTaxAccount.isdimension4 || salesTaxAccount.isdimension5 || salesTaxAccount.isdimension6)
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                bool isDimension = (bool)salesTaxAccount.GetType().GetProperty($"isdimension{i}").GetValue(salesTaxAccount);
                                if (isDimension)
                                {
                                    string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesReturnCreditDto, item.TotalTaxAmt, customerCreditNote.ExchangeRate, customerCreditNote.BranchId, customerCreditNote.FinancialYearId));
                                }
                            }
                        }
                        customerCreditNote.CustomerTransactionDetails.Add(new()
                        {
                            LineNumber = ++transactionDetailNumber,
                            LedgerAccountId = salesTaxAccount.chartofaccountid,
                            LedgerAccountCode = salesTaxAccount.chartofaccountcode,
                            LedgerAccountName = salesTaxAccount.chartofaccountname,
                            DebitAmount = item.TotalTaxAmt,
                            DebitAmountInBaseCurrency = item.TotalTaxAmt * customerCreditNote.ExchangeRate,
                            TotalAmount = item.TotalTaxAmt,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            IsControlAccount = true,
                            ControlAccountType = ControlAccountType.Tax,
                            BranchId = customerCreditNote.BranchId,
                            FinancialYearId = customerCreditNote.FinancialYearId,
                            ItemId = item.ItemId
                        });
                    }

                    if (item.DiscountAmt > 0)
                    {
                        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                        if (chartOfAccount.isdimension1 || chartOfAccount.isdimension2 || chartOfAccount.isdimension3 || chartOfAccount.isdimension4 || chartOfAccount.isdimension5 || chartOfAccount.isdimension6)
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                bool isDimension = (bool)chartOfAccount.GetType().GetProperty($"isdimension{i}").GetValue(chartOfAccount);
                                if (isDimension)
                                {
                                    string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesReturnCreditDto, item.DiscountAmt, customerCreditNote.ExchangeRate, customerCreditNote.BranchId, customerCreditNote.FinancialYearId));
                                }
                            }
                        }
                        customerCreditNote.CustomerTransactionDetails.Add(new()
                            {
                            LineNumber = ++transactionDetailNumber,
                            LedgerAccountId = chartOfAccount.chartofaccountid,
                            LedgerAccountCode = chartOfAccount.chartofaccountcode,
                            LedgerAccountName = chartOfAccount.chartofaccountname,
                            CreditAmount = item.DiscountAmt,
                            CreditAmountInBaseCurrency = item.DiscountAmt * customerCreditNote.ExchangeRate,
                            TotalAmount = item.DiscountAmt,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            IsControlAccount = true,
                            ControlAccountType = ControlAccountType.Discount,
                            BranchId = customerCreditNote.BranchId,
                            FinancialYearId = customerCreditNote.FinancialYearId,
                            ItemId = item.ItemId
                        });
                    }

                    int transactionOrgin = (int)salesReturnCreditDto.TransactionOriginName;
                    int transactionType = (int)salesReturnCreditDto.TransOriginType;
                    if (item.TotalItemAmount > 0)
                    {
                        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
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

                        if (ledgeraccount.isdimension1 || ledgeraccount.isdimension2 || ledgeraccount.isdimension3 || ledgeraccount.isdimension4 || ledgeraccount.isdimension5 || ledgeraccount.isdimension6)
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                bool isDimension = (bool)ledgeraccount.GetType().GetProperty($"isdimension{i}").GetValue(ledgeraccount);
                                if (isDimension)
                                {
                                    string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesReturnCreditDto, item.TotalItemAmount, customerCreditNote.ExchangeRate, customerCreditNote.BranchId, customerCreditNote.FinancialYearId));
                                }
                            }
                        }

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
                            Narration = customerCreditNote.Narration,
                            ItemId = item.ItemId,
                            DimensionAllocations = dimensions,
                            IsDimensionAllocation = dimensions.Count() > 0 ? true : false,
                        });
                    }
                }
            }

            if (salesReturnCreditDto.isVanSales == true)
            {
                customerCreditNote.DocumentNumber = salesReturnCreditDto.CreditNoteNumber;
            }

        if (IsPosting == true)
        {
            if (salesReturnCreditDto.TransactionOriginName == TransactionOrgin.RetailSalesOrder && salesReturnCreditDto.CreditNoteNumber != "")
            {
                CustomerCreditNoteViewModel viewModel = new CustomerCreditNoteViewModel();
                viewModel.CustomerCreditNote = customerCreditNote;
                int count = await customerCreditNoteService.IsDocumentExist(salesReturnCreditDto.CreditNoteNumber);
                if(count == 0)
                    throw new ResourceNotFoundException($"DocumentNo Does Not Exist:-{salesReturnCreditDto.CreditNoteNumber}");
                int customerId = await customerCreditNoteService.GetCustomerCreditNoteIdByDocumentNumber(salesReturnCreditDto.CreditNoteNumber);
                int customertransactionId = await customerTransactionService.GetCustomerTransactionId(customerId, (int)DocumentType.CustomerCreditNote);

                customerCreditNote.Id = customerId;
                customerCreditNote.CustomerTransactionId = customertransactionId;
                customerCreditNote.DocumentNumber = salesReturnCreditDto.CreditNoteNumber;
                customerCreditNote.CustomerTransactionDetails.ForEach(x => x.CustomerTransactionId = customertransactionId);
                int deletedId = await customerTransactionDetailService.DeleteDetailByCustomerTransactionId(customertransactionId);
                int updatedId = await customerCreditNoteService.UpdateCustomerCredit(viewModel);
                customerCreditNoteResult = customerCreditNote;
            }
            else
            {
                customerCreditNoteResult = await customerCreditNoteService.InsertCustomerCreditNote(customerCreditNote);
            }

            if (salesReturnCreditDto.CreditNoteNumber != "")
            {
                int deletedAlloction = await customerAllocationTransactionRepository.DeleteCustomerAllocationByCreditNoteIdAsync(customerCreditNote.Id);
            }

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

            //if (salesReturnCreditDto.TransOriginType == TransactionType.RetailSalesOrderReturnCredit && salesReturnCreditDto.CreditNoteNumber != "")
            //{
            //    if (customerAllocations.CustomerAllocationDetails.Count() > 0)
            //    {
            //        await customerAllocationTransactionService.InsertCustomerAllocation(customerAllocations);
            //    }
            //}
   
                if (customerAllocations.CustomerAllocationDetails.Count() > 0)
                    await customerAllocationTransactionService.InsertCustomerAllocation(customerAllocations);


            if (salesReturnCreditDto.IsProcess == true)
            {
                CustomerTransaction doc = await customerTransactionRepository.GetByCreditNoteIdAsync(customerCreditNoteResult.Id);
                if (doc != null)
                {
                    var GLPosting = await generalLedgerPostingRepository.GetGeneralLedgerBeforePostingEntries(doc.DocumentType, doc.Id);
                    var GLPostingGroup = generalLedgerPostingService.GroupGLPostingByLedgerAccountId(GLPosting);

                    await postDocumentViewModelRepository.PostGLAsync(GLPostingGroup);
                    await customerTransactionRepository.UpdateStatus(doc.Id, ApproveStatus.Approved);
                    await customerCreditNoteRepository.UpdateStatus(customerCreditNoteResult.Id, ApproveStatus.Approved);
                }
            }
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
        else
        {
            SalesReturnCreditResponse salesReturnCreditResponse = new SalesReturnCreditResponse();
            List<CustomerTransactionDetail> customerTransactionDetails = customerCreditNote.CustomerTransactionDetails.ToList();
            List<SalesReturnCreditViewDto> salesReturnCreditViewDtos = Mapper.Map<List<SalesReturnCreditViewDto>>(customerTransactionDetails);
            salesReturnCreditResponse.salesReturnCreditViewDtos = salesReturnCreditViewDtos;
            return salesReturnCreditResponse;

        }
    }
    private async Task<IEnumerable<CustomerTransactionDetailDimension>> LedgerDimensionInsert(string dimension, SalesReturnCreditDto salesReturnCreditDto, decimal amount, decimal exchangeRate,int branchId,int financialYearId)
    {
        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
        Dimension details = await dimensionRepository.GetByDimensionTypeName(dimension);
        if (details.DimensionTypeLabel == TradingDimensionTypeType.Project.ToString() && salesReturnCreditDto.ProjectCode != "")
        {
            dimensions = (await DimensionDetails(details.DimensionTypeLabel, salesReturnCreditDto.ProjectCode, amount, exchangeRate,branchId,financialYearId,salesReturnCreditDto.SalesCreditDate)).ToList();
        }
        else if (details.DimensionTypeLabel == TradingDimensionTypeType.costcenter.ToString() && salesReturnCreditDto.CostCenterCode != "")
        {
            dimensions = (await DimensionDetails(details.DimensionTypeLabel, salesReturnCreditDto.CostCenterCode, amount, exchangeRate, branchId, financialYearId, salesReturnCreditDto.SalesCreditDate)).ToList();
        }
        else if (details.DimensionTypeLabel == TradingDimensionTypeType.Employee.ToString() && salesReturnCreditDto.EmployeeCode != "")
        {
            dimensions = (await DimensionDetails(details.DimensionTypeLabel, salesReturnCreditDto.EmployeeCode, amount, exchangeRate, branchId, financialYearId, salesReturnCreditDto.SalesCreditDate)).ToList();
        }
        else
        {
            throw new ResourceNotFoundException($"{details.DimensionTypeLabel} Dimension is Mandatory for this account");
        }
        return dimensions;
    }

    private async Task<IEnumerable<CustomerTransactionDetailDimension>> DimensionDetails(string dimensionTypeLabel, string code, decimal amount, decimal exchangeRate, int branchId, int financialYearId,DateTime transactionDate)
    {

        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
        DimensionMaster dimensionMaster = await dimensionMasterRepository.GetDimensionMasterDetails(dimensionTypeLabel, code);
        string branchname = await branchService.GetBranchNameById(branchId);

        dimensions.Add(new()
        {
            DimensionTypeId = dimensionMaster.DimensionTypeId,
            DimensionTypeName = dimensionMaster.DimensionTypeName,
            DimensionDetailId = dimensionMaster.DimensionId,
            DimensionCode = dimensionMaster.Code,
            DimensionDetailName = dimensionMaster.Name,
            AllocatedAmount = amount,
            AmountInBaseCurrency = exchangeRate * amount,
            IsDebit = true,
            BranchName = branchname,
            BranchId = branchId,
            FinancialYearId = financialYearId,
            TransactionDate = transactionDate.Date
        });
        return dimensions;
    }
}