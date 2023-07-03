using Chef.Common.Models;
using Chef.Common.Services;
using Chef.Common.Types;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Repositories;
using Chef.Finance.Customer.Services;
using Chef.Finance.GL.Repositories;
using Chef.Finance.GL.Services;
using Chef.Finance.Integration.Models;
using Chef.Finance.Models;
using Chef.Finance.Repositories;
using Chef.Finance.Services;
using Newtonsoft.Json;
using System.Collections;

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
    private readonly ISalesInvoiceRepository salesInvoiceRepository;
    private readonly IJournalBookRepository journalBookRepository;
    private readonly IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository;
    private readonly ITenantSimpleUnitOfWork tenantSimpleUnitOfWork;
    private readonly ICustomerTransactionDetailService customerTransactionDetailService;
    private readonly ISalesInvoiceLineItemService salesInvoiceLineItemService;
    private readonly IPurchaseControlAccountService purchaseControlAccountService;
    private readonly IDimensionRepository dimensionRepository;
    private readonly IDimensionMasterRepository dimensionMasterRepository;
    private readonly IBranchService branchService;



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
        IGeneralLedgerPostingService generalLedgerPostingService,
        ISalesInvoiceRepository salesInvoiceRepository,
        IJournalBookRepository journalBookRepository,
        IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository,
        ITenantSimpleUnitOfWork tenantSimpleUnitOfWork,
        ICustomerTransactionDetailService customerTransactionDetailService,
        ISalesInvoiceLineItemService salesInvoiceLineItemService,
        IPurchaseControlAccountService purchaseControlAccountService,
        IDimensionRepository dimensionRepository,
        IDimensionMasterRepository dimensionMasterRepository,
        IBranchService branchService

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
        this.salesInvoiceRepository = salesInvoiceRepository;
        this.journalBookRepository = journalBookRepository;
        this.journalBookNumberingSchemeRepository = journalBookNumberingSchemeRepository;
        this.tenantSimpleUnitOfWork =  tenantSimpleUnitOfWork;
        this.customerTransactionDetailService = customerTransactionDetailService;
        this.salesInvoiceLineItemService = salesInvoiceLineItemService;
        this.purchaseControlAccountService = purchaseControlAccountService;
        this.dimensionRepository = dimensionRepository;
        this.dimensionMasterRepository = dimensionMasterRepository;
        this.branchService = branchService;
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

    private IntegrationJournalBookConfiguration journalBookConfig = new IntegrationJournalBookConfiguration();
    public async Task<SalesInvoiceResponse> Insert(SalesInvoiceDto salesInvoiceDto)
    {
        try
        {
            tenantSimpleUnitOfWork.BeginTransaction();
            SalesInvoiceResponse salesInvoiceResponses = await InsertAsync(salesInvoiceDto);
            tenantSimpleUnitOfWork.Commit();
            return salesInvoiceResponses;
        }
        catch(Exception ex)
        {
            tenantSimpleUnitOfWork.Rollback();
            throw;
        }
    }


    public async Task<SalesInvoiceResponse> ViewSalesInvoice(SalesInvoiceDto salesInvoiceDto)
    {
        try
        {
            SalesInvoiceResponse details = await InsertAsync(salesInvoiceDto,false);
            return details;
        }
        catch(Exception ex)
        {
            throw;
        }
    }
    bool posting = true;


    private async Task<SalesInvoiceResponse> InsertAsync(SalesInvoiceDto salesInvoiceDto,bool IsPosting = true)
    {
        SalesInvoice details = new SalesInvoice();
        IEnumerable<BusinessPartnerControlAccountViewModel> businessPartnerControlAccount = new List<BusinessPartnerControlAccountViewModel>();
        LedgerAccountViewModel purchaseControlAccount = new LedgerAccountViewModel();
        int salesInvoiceId = 0;
        int orgin = 0;
        int type = 0;

        int financialYearId = await companyFinancialYearRepository.GetFinancialYearIdByDate(salesInvoiceDto.SalesInvoiceDate.Date);

        if (salesInvoiceDto.SalesOrderOrigin == 4)
            {
                string code = salesInvoiceDto.SalesInvoiceNo.Substring(0, 5);

                //int financialYearId = await companyFinancialYearRepository.GetFinancialYearIdByDate(salesInvoiceDto.SalesInvoiceDate.Date);
                //As per discussion For Vansales no need Po Group journal book checking
                int journalBookNumberingScheme = await journalBookNumberingSchemeRepository.GetJournalNumberingSchemeCount(financialYearId, salesInvoiceDto.BranchId, code);

                if (journalBookNumberingScheme == 0)
                    throw new ResourceNotFoundException($"DocumentSeries not configured for this VanSalesCode:{salesInvoiceDto.SalesInvoiceNo}");

                journalBookConfig = await journalBookRepository.getJournalBookdetailsByVanSalesCode(code);
                if (journalBookConfig ==
                    null)
                    throw new ResourceNotFoundException($"Journalbook not configured for this VanSalesCode:{salesInvoiceDto.SalesInvoiceNo}");

                if (!salesInvoiceDto.SalesInvoiceNo.Contains(".") && !salesInvoiceDto.SalesInvoiceNo.Contains("_"))
                {
                    int updateJournalBookNumberingScheme = await journalBookNumberingSchemeRepository.UpdateJournalBookNumberingScheme(code, salesInvoiceDto.BranchId, financialYearId, salesInvoiceDto.SalesInvoiceNo);
                }

            }
            if (salesInvoiceDto.TransOriginType == TransactionType.RetailSalesOrderInvoiceCredit)
            {
                orgin = (int)TransactionOrgin.RetailSalesOrder;
                type = (int)TransactionType.RetailSalesOrderInvoiceCredit;
            }
            else if (salesInvoiceDto.TransOriginType == TransactionType.RetailSalesOrderInvoiceCash)
            {
                orgin = (int)TransactionOrgin.RetailSalesOrder;
                type = (int)TransactionType.RetailSalesOrderInvoiceCash;
            }
            else if (salesInvoiceDto.TransOriginType == TransactionType.SalesOrderInvoice)
            {
                orgin = (int)TransactionOrgin.SalesOrder;
                type = (int)TransactionType.SalesOrderInvoice;
            }
            if(salesInvoiceDto.SalesOrderOrigin != 4)
                journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(orgin, type, salesInvoiceDto.PoGroupId);

            if (journalBookConfig == null)
                throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and type");

            SalesInvoice salesInvoice = Mapper.Map<SalesInvoice>(salesInvoiceDto);

            if (salesInvoiceDto.SalesOrderOrigin == 4)
            {
                salesInvoice.Narration = TransactionType.VanSalesOrderInvoice + "-" + salesInvoiceDto.SalesInvoiceNo;
            }
            else if (salesInvoiceDto.TransOriginType == TransactionType.RetailSalesOrderInvoiceCredit)
            {
                salesInvoice.Narration = TransactionType.RetailSalesOrderInvoiceCredit + "-" + salesInvoiceDto.SalesInvoiceNo;
            }
            else if (salesInvoiceDto.TransOriginType == TransactionType.RetailSalesOrderInvoiceCash)
            {
                salesInvoice.Narration = TransactionType.RetailSalesOrderInvoiceCash + "-" + salesInvoiceDto.SalesInvoiceNo;
            }
            else
            {
              salesInvoice.Narration = TransactionType.SalesOrderInvoice + "-" + salesInvoiceDto.SalesInvoiceNo;
            }

            salesInvoice.FinancialYearId = financialYearId;
            salesInvoice.ApproveStatus = ApproveStatus.Draft;
            salesInvoice.JournalBookCode = journalBookConfig.JournalBookCode;
            salesInvoice.JournalBookId = journalBookConfig.JournalBookId;
            salesInvoice.JournalBookName = journalBookConfig.JournalBookName;
            salesInvoice.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
            salesInvoice.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;
            salesInvoice.TransactionTypeId = type;

            salesInvoice.OtherDetail = new()
            {
                Narration = salesInvoice.Narration,
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

                if (salesInvoiceDto.IsCashSales != true)
                {
                        businessPartnerControlAccount = await businessPartnerGroupService.GetCustomerControlAccountsByBusinessPartnerIdAsync(salesInvoice.BusinessPartnerId);
                    if (businessPartnerControlAccount == null || businessPartnerControlAccount.Count() == 0)
                        throw new ResourceNotFoundException("Business partner control account not found for this business partner");
                }
                else
                {
                   purchaseControlAccount = await purchaseControlAccountService.GetCashSuspenseAccount();
                   if (purchaseControlAccount == null)
                        throw new ResourceNotFoundException("Cash Suspense control account not Configured");
                }

                var salesTaxAccount = await taxAccountSetupService.GetTaxAccountDimension();
                if (salesTaxAccount == null)
                    throw new ResourceNotFoundException("Sales tax account not found");

                var chartOfAccount = await glControlAccountService.GetSalesDiscountAccountDimension();
                if (chartOfAccount == null)
                    throw new ResourceNotFoundException("Control account not configured for sales invoice discount");

                foreach (var item in salesInvoice.LineItems)
                {
                    item.LineNumber = ++itemLineNumber;
                    item.BranchId = salesInvoice.BranchId;
                    item.FinancialYearId = salesInvoice.FinancialYearId;

                   //List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();

                    if (item.TotalAmount > 0)
                    {
                        if (salesInvoiceDto.IsCashSales != true)
                        {

                            List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                            BusinessPartnerControlAccountViewModel firstBusinessPartner = businessPartnerControlAccount.First();
                            if (firstBusinessPartner.isdimension1 || firstBusinessPartner.isdimension2 || firstBusinessPartner.isdimension3 || firstBusinessPartner.isdimension4 || firstBusinessPartner.isdimension5 || firstBusinessPartner.isdimension6) 
                            {
                                for (int i = 1; i <= 6; i++)
                                {
                                    bool isDimension = (bool)firstBusinessPartner.GetType().GetProperty($"isdimension{i}").GetValue(firstBusinessPartner);
                                    if (isDimension)
                                    {
                                        string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                        dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesInvoiceDto, item.TotalAmount, salesInvoice.ExchangeRate,salesInvoice.BranchId, salesInvoice.FinancialYearId,true));
                                    }
                                }
                            }
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
                                FinancialYearId = salesInvoice.FinancialYearId,
                                ItemId = item.ItemId,
                                DimensionAllocations = dimensions,
                                IsDimensionAllocation = dimensions.Count() > 0 ? true : false,
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
                                        dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesInvoiceDto, item.TotalAmount, salesInvoice.ExchangeRate,salesInvoice.BranchId, salesInvoice.FinancialYearId,true));
                                    }
                                }
                            }
                            salesInvoice.CustomerTransactionDetails.Add(new()
                            {
                                LineNumber = ++transactionDetailNumber,
                                LedgerAccountId = purchaseControlAccount.chartofaccountid,
                                LedgerAccountCode = purchaseControlAccount.chartofaccountcode,
                                LedgerAccountName = purchaseControlAccount.chartofaccountname,
                                DebitAmount = item.TotalAmount,
                                DebitAmountInBaseCurrency = item.TotalAmount * salesInvoice.ExchangeRate,
                                CostAllocationCode = "NA",
                                CostAllocationDescription = "No Cost Allocation",
                                IsControlAccount = true,
                                ControlAccountType = ControlAccountType.Integration,
                                BranchId = salesInvoice.BranchId,
                                FinancialYearId = salesInvoice.FinancialYearId,
                                ItemId = item.ItemId,
                                DimensionAllocations = dimensions,
                                IsDimensionAllocation = dimensions.Count() > 0 ? true : false,
                            });
                        }
                    }

                    if (item.TaxAmount > 0)
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
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesInvoiceDto, item.TaxAmount, salesInvoice.ExchangeRate,salesInvoice.BranchId, salesInvoice.FinancialYearId,false));
                                }
                            }
                        }
                        salesInvoice.CustomerTransactionDetails.Add(new()
                        {
                            LineNumber = ++transactionDetailNumber,
                            LedgerAccountId = salesTaxAccount.chartofaccountid,
                            LedgerAccountCode = salesTaxAccount.chartofaccountcode,
                            LedgerAccountName = salesTaxAccount.chartofaccountname,
                            CreditAmount = item.TaxAmount,
                            CreditAmountInBaseCurrency = item.TaxAmount * salesInvoice.ExchangeRate,
                            TotalAmount = item.TaxAmount,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            IsControlAccount = true,
                            ControlAccountType = ControlAccountType.Tax,
                            BranchId = salesInvoice.BranchId,
                            FinancialYearId = salesInvoice.FinancialYearId,
                            ItemId = item.ItemId,
                            DimensionAllocations = dimensions,
                            IsDimensionAllocation = dimensions.Count() > 0 ? true : false,
                        });
                    }

                    if (item.DiscountAmount > 0)
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
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesInvoiceDto, item.DiscountAmount, salesInvoice.ExchangeRate, salesInvoice.BranchId, salesInvoice.FinancialYearId,true));
                                }
                            }
                        }
                        salesInvoice.CustomerTransactionDetails.Add(new()
                        {
                            LineNumber = ++transactionDetailNumber,
                            LedgerAccountId = chartOfAccount.chartofaccountid,
                            LedgerAccountCode = chartOfAccount.chartofaccountcode,
                            LedgerAccountName = chartOfAccount.chartofaccountname,
                            DebitAmount = item.DiscountAmount,
                            DebitAmountInBaseCurrency = item.DiscountAmount * salesInvoice.ExchangeRate,
                            TotalAmount = item.DiscountAmount,
                            CostAllocationCode = "NA",
                            CostAllocationDescription = "No Cost Allocation",
                            IsControlAccount = true,
                            ControlAccountType = ControlAccountType.Discount,
                            BranchId = salesInvoice.BranchId,
                            FinancialYearId = salesInvoice.FinancialYearId,
                            ItemId = item.ItemId,
                            DimensionAllocations = dimensions,
                            IsDimensionAllocation = dimensions.Count() > 0 ? true : false,
                        });
                    }

                    var itemDto = salesInvoiceDto.SalesInvoiceItemDto[itemLineNumber - 1];
                    int transactionOrgin = (int)salesInvoiceDto.TransactionOriginName;
                    int transactionType = (int)salesInvoiceDto.TransOriginType;
                    if (item.Amount > 0)
                    {
                        List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
                        ItemViewModel viewModel = new()
                        {
                            ItemCategoryId = itemDto.ItemCategory,
                            TransOrginId = transactionOrgin,
                            TransTypeId = transactionType,
                            ItemTypeId = itemDto.ItemType,
                            ItemSegmentId = itemDto.ItemSegmentId,
                            ItemFamilyId = itemDto.ItemFamilyId,
                            ItemClassId = itemDto.ItemClassId,
                            ItemCommodityId = itemDto.ItemCommodityId,
                            GroupId = salesInvoiceDto.PoGroupId
                        };
                        var ledgeraccount = await integrationControlAccountRepository.getLedgerAccountDetails(viewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.SalesRevenueAccountType));
                        if (ledgeraccount == null)
                            throw new ResourceNotFoundException($"Ledger Account not configured for this item:{salesInvoiceDto.SalesInvoiceItemDto.First().ItemName}-{salesInvoiceDto.SalesInvoiceItemDto.First().ItemCode}");

                        if (ledgeraccount.isdimension1 || ledgeraccount.isdimension2 || ledgeraccount.isdimension3 || ledgeraccount.isdimension4 || ledgeraccount.isdimension5 || ledgeraccount.isdimension6)
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                bool isDimension = (bool)ledgeraccount.GetType().GetProperty($"isdimension{i}").GetValue(ledgeraccount);
                                if (isDimension)
                                {
                                    string dimensionName = EnumExtensions.GetDisplayName((DimensionType)Enum.Parse(typeof(DimensionType), $"Dimension{i}"));
                                    dimensions.AddRange(await LedgerDimensionInsert(dimensionName, salesInvoiceDto, item.Amount, salesInvoice.ExchangeRate,salesInvoice.BranchId,salesInvoice.FinancialYearId,false));
                                }
                            }
                        }


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
                            FinancialYearId = salesInvoice.FinancialYearId,
                            Narration = salesInvoice.Narration,
                            ControlAccountType = ControlAccountType.Integration,
                            ItemId = item.ItemId,
                            DimensionAllocations = dimensions,
                            IsDimensionAllocation = dimensions.Count() > 0 ? true : false,

                        }); 
                    }
                }
            }

            if (salesInvoiceDto.SalesOrderOrigin == 4)
            {
                salesInvoice.DocumentNumber = salesInvoiceDto.SalesInvoiceNo;
            }

        if (IsPosting == true)
        {
            string json = JsonConvert.SerializeObject(salesInvoice);

            if (salesInvoiceDto.TransactionOriginName == TransactionOrgin.RetailSalesOrder && salesInvoiceDto.SalesInvoiceNo != "")
            {
                int count = await salesInvoiceService.IsDocumentNoExist(salesInvoiceDto.SalesInvoiceNo);
                if (count == 0)
                    throw new ResourceNotFoundException($"DocumentNo Does Not Exist:-{salesInvoiceDto.SalesInvoiceNo}");
                SalesInvoice invoice = await salesInvoiceService.GetSalesInvoiceIdByDocumentNumber(salesInvoiceDto.SalesInvoiceNo);
                salesInvoice.Id = invoice.Id;
                salesInvoice.CustomerTransactionId = invoice.CustomerTransactionId;
                salesInvoiceId = salesInvoice.Id;
                salesInvoice.DocumentNumber = salesInvoiceDto.SalesInvoiceNo;
                salesInvoice.CustomerTransactionDetails.ForEach(x => x.CustomerTransactionId = invoice.CustomerTransactionId);
                salesInvoice.LineItems.ForEach(x => x.SalesInvoiceId = salesInvoiceId);
                int deleteCustomerTranDetail = await customerTransactionDetailService.DeleteDetailByCustomerTransactionId(invoice.CustomerTransactionId);
                int deleteLineItems = await salesInvoiceLineItemService.DeleteLineItemByInvoiceId(salesInvoiceId);
                int updatedid = await salesInvoiceService.UpdateInvoice(salesInvoice);
                details.DocumentNumber = salesInvoice.DocumentNumber;
            }
            else 
            {
                details = await salesInvoiceService.InsertInvoice(salesInvoice);
                salesInvoiceId = details.Id;
                
            }
            //string json = JsonConvert.SerializeObject(salesInvoiceResponse);
            if (salesInvoiceDto.IsProcess == true)
            {
                CustomerTransaction doc = await customerTransactionRepository.GetByInvoiceIdAsync(salesInvoiceId);
                if (doc != null)
                {
                    var GLPosting = await generalLedgerPostingRepository.GetGeneralLedgerBeforePostingEntries(doc.DocumentType, doc.Id);
                    var GLPostingGroup = generalLedgerPostingService.GroupGLPostingByLedgerAccountId(GLPosting);

                    await postDocumentViewModelRepository.PostGLAsync(GLPostingGroup);
                    await customerTransactionRepository.UpdateStatus(doc.Id, ApproveStatus.Approved);
                    await salesInvoiceRepository.UpdateStatus(salesInvoiceId, ApproveStatus.Approved);
                }
            }
            return new()
            {
                DocumentNumber = details.DocumentNumber
            };
            
        }
        else
        {
            SalesInvoiceResponse salesInvoiceResponse = new SalesInvoiceResponse();
            List<CustomerTransactionDetail> customerTransactionDetai = salesInvoice.CustomerTransactionDetails.ToList();
            List<SalesInvoiceViewDto> salesInvoiceViewDto = Mapper.Map<List<SalesInvoiceViewDto>>(customerTransactionDetai);
            salesInvoiceResponse.salesInvoices = salesInvoiceViewDto;
            return salesInvoiceResponse;
        }
        
    }

        private async Task<IEnumerable<CustomerTransactionDetailDimension>>LedgerDimensionInsert(string dimension, SalesInvoiceDto salesInvoiceDto,decimal amount,decimal exchangeRate,int branchId,int financialYearId,bool isDebit)
        {
            List<CustomerTransactionDetailDimension> dimensions = new List<CustomerTransactionDetailDimension>();
            Dimension details = await dimensionRepository.GetByDimensionTypeName(dimension);
            if(details.DimensionTypeLabel == TradingDimensionTypeType.Project.ToString() && salesInvoiceDto.ProjectCode != "")
            {
              dimensions = (await DimensionDetails(details.DimensionTypeLabel, salesInvoiceDto.ProjectCode,amount, exchangeRate, branchId, financialYearId, salesInvoiceDto.SalesInvoiceDate, isDebit)).ToList();
            }
            else if (details.DimensionTypeLabel == TradingDimensionTypeType.costcenter.ToString() && salesInvoiceDto.CostCenterCode != "")
            {
              dimensions= (await DimensionDetails(details.DimensionTypeLabel, salesInvoiceDto.CostCenterCode,amount, exchangeRate, branchId, financialYearId, salesInvoiceDto.SalesInvoiceDate, isDebit)).ToList();
            }
            else if (details.DimensionTypeLabel == TradingDimensionTypeType.Employee.ToString() && salesInvoiceDto.EmployeeCode != "")
            {
               dimensions= (await DimensionDetails(details.DimensionTypeLabel, salesInvoiceDto.EmployeeCode,amount, exchangeRate, branchId, financialYearId, salesInvoiceDto.SalesInvoiceDate, isDebit)).ToList();
            }
            else
            {
                throw new ResourceNotFoundException($"{details.DimensionTypeLabel} Dimension is Mandatory for this account");
            }
           return dimensions;
        }
        private async Task<IEnumerable<CustomerTransactionDetailDimension>> DimensionDetails(string dimensionTypeLabel, string code,decimal amount, decimal exchangeRate, int branchId, int financialYearId,DateTime transactionDate, bool isDebit)
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
                IsDebit = isDebit,
                BranchName = branchname,
                BranchId = branchId,
                FinancialYearId = financialYearId,
                TransactionDate = transactionDate.Date
            });
           return dimensions;
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
