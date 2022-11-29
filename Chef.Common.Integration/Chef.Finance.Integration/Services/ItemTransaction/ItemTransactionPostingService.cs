using Chef.Finance.Configuration.Repositories;
using Chef.Finance.GL.Repositories;
using Chef.Finance.Integration.Models;
using Chef.Finance.Repositories;
using Newtonsoft.Json;
using System.Collections;
namespace Chef.Finance.Integration;

public class ItemTransactionPostingService : AsyncService<TradingIntegrationHeader>, IItemTransactionPostingService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly IIntegrationControlAccountRepository integrationControlAccountRepository;
    private readonly IPurchaseControlAccountRepository purchaseControlAccountRepository;
    private readonly IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly ITradingIntegrationRepository tradingIntegrationRepository;
    private readonly IIntegrationDetailsRepository integrationDetailsRepository;
  
    private readonly IDimensionRepository dimensionRepository;
    private readonly IDimensionMasterRepository dimensionMasterRepository;
    private readonly IIntegrationDetailDimensionRepository integrationDetailDimensionRepository;
    private readonly IReasonCodeControlAccountRepository reasonCodeControlAccountRepository;

    public ItemTransactionPostingService(IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository, IIntegrationControlAccountRepository integrationControlAccountRepository, IPurchaseControlAccountRepository purchaseControlAccountRepository, IJournalBookNumberingSchemeRepository journalBookNumberingSchemeRepository,
        ICompanyFinancialYearRepository companyFinancialYearRepository, ITradingIntegrationRepository tradingIntegrationRepository,
        IIntegrationDetailsRepository integrationDetailsRepository,
       IDimensionRepository dimensionRepository,
        IDimensionMasterRepository dimensionMasterRepository,
       IIntegrationDetailDimensionRepository integrationDetailDimensionRepository,
       IReasonCodeControlAccountRepository reasonCodeControlAccountRepository
        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.integrationControlAccountRepository = integrationControlAccountRepository;
        this.purchaseControlAccountRepository = purchaseControlAccountRepository;
        this.journalBookNumberingSchemeRepository = journalBookNumberingSchemeRepository;
        this.companyFinancialYearRepository = companyFinancialYearRepository;
        this.tradingIntegrationRepository = tradingIntegrationRepository;
        this.integrationDetailsRepository = integrationDetailsRepository;       
        this.dimensionRepository = dimensionRepository;
        this.dimensionMasterRepository = dimensionMasterRepository;
        this.integrationDetailDimensionRepository = integrationDetailDimensionRepository;
        this.reasonCodeControlAccountRepository = reasonCodeControlAccountRepository;
    }
    private List<IntegrationDetails> integrationDetailList = new List<IntegrationDetails>();
    // private List<TradingIntegrationHeader> tradingIntegrationHeaders =new List<TradingIntegrationHeader>();
    private List<GeneralLedger> generalLedgerlList = new List<GeneralLedger>();
    private int lineNumber = 0;
    private int IntegrationHeaderId = 0;
    private int financialYearId = 0;
    private string documentNumber = "";
    private ItemTransactionFinanceDTO? itemDto =null;

    public async Task<IEnumerable<ItemTransactionFinanceDetailsDto>> PostItems(List<ItemTransactionFinanceDTO> itemTransactionFinanceDTO)
    {
        try
        {

            IntegrationJournalBookConfiguration items = await integrationJournalBookConfigurationRepository.getJournalBookdetails(itemTransactionFinanceDTO.First().TransOrgin, itemTransactionFinanceDTO.First().TransType);
            if (items == null)
                //TODO:will change the exception type once latest changes got from SK
                throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and  type");

            //header detailsss maping insert
            TradingIntegrationHeader intHeader = Mapper.Map<TradingIntegrationHeader>(itemTransactionFinanceDTO);
            intHeader.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
            intHeader.journalbookid = items.JournalBookId;
            intHeader.journalbookcode = items.JournalBookCode;
            intHeader.ApproveStatus = ApproveStatus.Draft;
            intHeader.documentType = DocumentType.IntegrationHeader;
            intHeader.ApproveStatusName = ApproveStatus.Draft.ToString();

            // simpleUnitOfWork.BeginTransaction();
            intHeader.documentnumber = await journalBookNumberingSchemeRepository.GetJournalTransactionsDocNumber(intHeader.FinancialYearId, intHeader.BranchId, items.JournalBookCode);
            int intHeaderId = await tradingIntegrationRepository.InsertAsync(intHeader);

            IntegrationHeaderId = intHeaderId;
            financialYearId = intHeader.FinancialYearId;
            documentNumber = intHeader.documentnumber;

            foreach (ItemTransactionFinanceDTO details in itemTransactionFinanceDTO)
            {
                TransactionOrgin origin = (TransactionOrgin)details.TransOrginId;
                TransactionType type = (TransactionType)details.TransTypeId;
                switch (origin, type)
                {
                    case (TransactionOrgin.Purchase, TransactionType.Receipt):
                        await GetPurchaseReceiptTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Purchase, TransactionType.Return):
                        await GetPurchaseReturnTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.SalesOrder, TransactionType.GRN):
                        await GetSalesOrderGRNTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.SalesOrder, TransactionType.Delivery):
                        await GetSalesOrderDeliveryTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Warehouse, TransactionType.Transferorder):
                        await GetWarehouseTransferOrderTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Warehouse, TransactionType.Transfercommit):
                        await GetWarehouseTransfercommitTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;

                    case (TransactionOrgin.Warehouse, TransactionType.InventoryAdjustmentCost):
                        await GetWarehouseTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Warehouse, TransactionType.InventoryAdjustmentExistingQty):
                        await GetWarehouseTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Warehouse, TransactionType.InventoryAdjustmentNewQty):
                        await GetWarehouseTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;
                    case (TransactionOrgin.Warehouse, TransactionType.InventoryCyclecounting):
                        await GetWarehouseTransactionIntegrationDetails(details, intHeader.Id, intHeader.FinancialYearId, intHeader.documentnumber);
                        break;

                    default:
                        break;

                }
            }
            ///////   integrationDetailList = await integrationDetailsRepository.BulkInsertAsync(integrationDetailList);


            // fetch integration list and map to ItemTransactionFinanceDetailsDto
            //loop ItemTransactionFinanceDetailsDto
            //in that loop insert  fetch corresponding dimensions using detailid and insert into ItemTransactionFinanceDetailsDto

            IEnumerable<ItemTransactionFinanceDetailsDto> itemTransactionFinanceDetailsDtos = await tradingIntegrationRepository.GetItemtransactionFinanceDetails(intHeaderId);

            IEnumerable<IntegrationDetailDimension> itemTransactionFinanceDetailsDimensions = await integrationDetailDimensionRepository.GetDetailDimensionByHeaderId(intHeaderId);

            foreach (ItemTransactionFinanceDetailsDto itemTransactionFinanceDetailsDto in itemTransactionFinanceDetailsDtos)
            {
                // itemTransactionFinanceDetailsDto.itemTransactionFinanceDetailsDimensions.Add
                IEnumerable<IntegrationDetailDimension> dimension = itemTransactionFinanceDetailsDimensions.Where(x => x.Integrationdetailid == itemTransactionFinanceDetailsDto.ReferenceDocumentdetailid).ToList();
                if (dimension.Count() > 0)
                {
                    List<ItemTransactionFinanceDetailsDimension> DimensionMapping = Mapper.Map<List<ItemTransactionFinanceDetailsDimension>>(dimension);
                    itemTransactionFinanceDetailsDto.itemTransactionFinanceDetailsDimensions = DimensionMapping;
                }

            }

            string jsonTransaction = JsonConvert.SerializeObject(integrationDetailList);
            JsonConvert.DeserializeObject<IEnumerable<IntegrationDetails>>(jsonTransaction);

            //GeneralLedger generalLedger = new GeneralLedger();
            //await PostLedger(intHeader.Id);
            //generalLedgerlList = await generalLedgerRepository.BulkInsertAsync(generalLedgerlList);
            //// simpleUnitOfWork.Commit();
            return itemTransactionFinanceDetailsDtos;
        }
        catch (Exception ex)
        {
            //simpleUnitOfWork.Rollback();
            throw;
        }


    }
    private async Task GetPurchaseReceiptTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        try
        {
            itemDto = itemTransactionFinanceDTO;

            // Configuration->Inv Control AC Control --dbit
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            if (ledgerAccountViewModel == null)
                //TODO:will change the exception type once latest changes got from SK
                throw new ResourceNotFoundException("Ledger Account not configured for this control Account");

            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            // A/ C Config->invoice to be received credit
            LedgerAccountViewModel ledgerAccountViewModel1 = await GetInvoiceToBeRecevied();
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);



            // Configuration(With CostelementID)->Inv Control AC credit
            //decimal TotalAmount = 0;
            //decimal TotalHmAmount = 0;
            if (itemTransactionFinanceDTO.itemTransactionFinanceLineCosts != null)
            {
                foreach (ItemTransactionFinanceLineCost itemTransactionFinanceLineCost in itemTransactionFinanceDTO.itemTransactionFinanceLineCosts)
                {
                    ItemViewModel itemViewModel1 = Mapper.Map<ItemViewModel>(itemTransactionFinanceLineCost);
                    itemViewModel1.LandingCost = itemTransactionFinanceDTO.LandingCostId;

                    LedgerAccountViewModel ledgerAccountViewModel2 = await GetItemAndLandedCostLedgerDetails(itemViewModel1, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
                    if (ledgerAccountViewModel2 == null)
                        throw new ResourceNotFoundException("Ledger Account not configured for this control Account");

                    await InsertIntegrationDetailList(ledgerAccountViewModel2, itemTransactionFinanceLineCost, false, itemTransactionFinanceDTO.BranchId);

                    //TotalAmount = TotalAmount + itemTransactionFinanceLineCost.TransAmount;
                    //TotalHmAmount = TotalHmAmount + itemTransactionFinanceLineCost.HmAmount;

                    //Configuration->Inv Control AC debit
                    LedgerAccountViewModel ledgerAccountViewModel3 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

                    await InsertIntegrationDetailList(ledgerAccountViewModel3, itemTransactionFinanceLineCost.TransAmount, itemTransactionFinanceLineCost.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

                }

                //Configuration->Inv Control AC debit

                //LedgerAccountViewModel ledgerAccountViewModel3 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

                //await InsertIntegrationDetailList(ledgerAccountViewModel3, TotalAmount, TotalHmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private async Task GetPurchaseReturnTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        try
        {
            itemDto = itemTransactionFinanceDTO;
            // A/ C Config->invoice to be received - debit               
            LedgerAccountViewModel ledgerAccountViewModel = await GetInvoiceToBeRecevied();
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);


            //Configuration -> Inv Control AC - credit               
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel1 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);


            //Configuration(With CostelementID)->Inv Control AC - debit
            decimal TotalAmount = 0;
            decimal TotalHmAmount = 0;
            if (itemTransactionFinanceDTO.itemTransactionFinanceLineCosts != null)
            {
                foreach (ItemTransactionFinanceLineCost itemTransactionFinanceLineCost in itemTransactionFinanceDTO.itemTransactionFinanceLineCosts)
                {
                    ItemViewModel itemViewModel1 = Mapper.Map<ItemViewModel>(itemTransactionFinanceLineCost);
                    itemViewModel1.LandingCost = itemTransactionFinanceDTO.LandingCostId;

                    LedgerAccountViewModel ledgerAccountViewModel2 = await GetItemAndLandedCostLedgerDetails(itemViewModel1, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

                    await InsertIntegrationDetailList(ledgerAccountViewModel2, itemTransactionFinanceLineCost, true, itemTransactionFinanceDTO.BranchId);

                    TotalAmount = TotalAmount + itemTransactionFinanceLineCost.TransAmount;
                    TotalHmAmount = TotalHmAmount + itemTransactionFinanceLineCost.HmAmount;

                    //Configuration->Inv Control AC -credit

                    LedgerAccountViewModel ledgerAccountViewModel3 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

                    await InsertIntegrationDetailList(ledgerAccountViewModel3, itemTransactionFinanceLineCost.TransAmount, itemTransactionFinanceLineCost.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);


                    //integrationDetails.debitamount = itemTransactionFinanceLineCost.TransAmount;
                    //TotalAmount = TotalAmount + integrationDetails.debitamount;
                    //integrationDetails.debitamountinbasecurrency = itemTransactionFinanceLineCost.HmAmount;
                    //TotalHmAmount = TotalHmAmount + integrationDetails.debitamountinbasecurrency;

                }

                //Configuration->Inv Control AC -credit

                //LedgerAccountViewModel ledgerAccountViewModel3 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));

                //await InsertIntegrationDetailList(ledgerAccountViewModel3, TotalAmount, TotalHmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task GetSalesOrderDeliveryTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        try
        {
            itemDto = itemTransactionFinanceDTO;
            //Configuration -> Inv Control AC -credit

            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            //Configuration -> Cost of Sales a/c - debit

            LedgerAccountViewModel ledgerAccountViewModel1 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.CostOfSalesAccountType));
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task GetSalesOrderGRNTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        itemDto = itemTransactionFinanceDTO;

        //Configuration -> Inv Control AC-debit
        ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
        LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
        await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

        //Configuration -> Cost of Sales a/c - credit
        LedgerAccountViewModel ledgerAccountViewModel1 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.CostOfSalesAccountType));
        await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

    }

    private async Task GetWarehouseTransferOrderTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        try
        {
            itemDto = itemTransactionFinanceDTO;
            //Configuration -> 	Item in Transit Control a/c - debit
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.ItemInTransitControlAccountType));
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            //Configuration -> Inv Control AC - credit

            LedgerAccountViewModel ledgerAccountViewModel1 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task GetWarehouseTransfercommitTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        try
        {
            itemDto = itemTransactionFinanceDTO;
            //Configuration -> Inv Control AC- debit
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            //Configuration -> 	Item in Transit Control a/c - credit
            LedgerAccountViewModel ledgerAccountViewModel1 = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task GetWarehouseTransactionIntegrationDetails(ItemTransactionFinanceDTO itemTransactionFinanceDTO, int IntegrationHeaderId, int FinancialYearId, string DocumentNumber)
    {
        itemDto = itemTransactionFinanceDTO;
        if (itemTransactionFinanceDTO.TransQty > 0)
        {
            //Configuration -> Inv Control AC - debit
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            //Configuration->reson code cont A / C - credit
            LedgerAccountViewModel ledgerAccountViewModel1 = await GetReasonCode(itemTransactionFinanceDTO.ReasonCode);
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
        }
        else
        {
            //Configuration -> Inv Control AC - credit
            ItemViewModel itemViewModel = Mapper.Map<ItemViewModel>(itemTransactionFinanceDTO);
            LedgerAccountViewModel ledgerAccountViewModel = await GetItemAndLandedCostLedgerDetails(itemViewModel, EnumExtensions.GetDisplayName(IntegrationControlAccountType.InversionControlAccounttype));
            await InsertIntegrationDetailList(ledgerAccountViewModel, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, false, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);

            //Configuration->reson code cont A / C - debit
            LedgerAccountViewModel ledgerAccountViewModel1 = await GetReasonCode(itemTransactionFinanceDTO.ReasonCode);
            await InsertIntegrationDetailList(ledgerAccountViewModel1, itemTransactionFinanceDTO.TransAmount, itemTransactionFinanceDTO.HmAmount, true, itemTransactionFinanceDTO.BranchId, itemTransactionFinanceDTO.ItemTransactionFinanceId);
        }
    }


    private async Task PostLedger(int integerationHeaderId)
    {
        try
        {
            IEnumerable<TradingIntegrationHeaderDetailsViewModel> tradingIntegrationHeadersDetails = await tradingIntegrationRepository.GetIntegrationHeaderDetails(integerationHeaderId);
            foreach (TradingIntegrationHeaderDetailsViewModel tradingIntegration in tradingIntegrationHeadersDetails)
            {
                GeneralLedger generalLedger = Mapper.Map<GeneralLedger>(tradingIntegration);
                generalLedger.ApproveStatus = Convert.ToInt32(ApproveStatus.Approved);
                generalLedgerlList.Add(generalLedger);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<LedgerAccountViewModel> GetReasonCode(string ReasonCode)
    {
        try
        {
            LedgerAccountViewModel controlAccount = await reasonCodeControlAccountRepository.getReasonCodeControlAccount(ReasonCode);
            return controlAccount;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<LedgerAccountViewModel> GetItemAndLandedCostLedgerDetails(ItemViewModel itemViewModel, string ControlAccount)
    {
        try
        {
            LedgerAccountViewModel itemControlAccount = await integrationControlAccountRepository.getLedgerAccountDetails(itemViewModel, ControlAccount);
            return itemControlAccount;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private async Task<LedgerAccountViewModel> GetInvoiceToBeRecevied()
    {
        try
        {
            LedgerAccountViewModel itemControlAccount = await purchaseControlAccountRepository.GetInvoiceToBeRecevied();
            return itemControlAccount;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private async Task InsertIntegrationDetailList(LedgerAccountViewModel ledgerAccountViewModel, decimal amount, decimal amountInHomeCurrency, bool isDebit, int branchId, int ItemTransactionFinanceId)
    {
        IntegrationDetails integrationDetails = new IntegrationDetails();
        integrationDetails.ledgeraccountid = ledgerAccountViewModel.chartofaccountid;
        integrationDetails.ledgeraccountcode = ledgerAccountViewModel.chartofaccountcode;
        integrationDetails.ledgeraccountname = ledgerAccountViewModel.chartofaccountname;
        if (isDebit)
        {
            integrationDetails.debitamount = amount;
            integrationDetails.debitamountinbasecurrency = amountInHomeCurrency;
        }
        else
        {
            integrationDetails.creditamount = amount;
            integrationDetails.creditamountinbasecurrency = amountInHomeCurrency;
        }
        integrationDetails.linenumber = ++lineNumber;
        integrationDetails.integrationheaderid = IntegrationHeaderId;
        integrationDetails.ItemTransactionFinanceId = ItemTransactionFinanceId;
        integrationDetails.FinancialYearId = financialYearId;
        integrationDetails.DocumentNumber = documentNumber;
        integrationDetails.BranchId = branchId;
        // integrationDetailList.Add(integrationDetails);

        int integrationdetailid = await integrationDetailsRepository.InsertAsync(integrationDetails);
        integrationDetails.Id = integrationdetailid;
        if (ledgerAccountViewModel.isdimension1 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension1), isDebit, integrationDetails);
        }
        if (ledgerAccountViewModel.isdimension2 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension2), isDebit, integrationDetails);
        }
        if (ledgerAccountViewModel.isdimension3 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension3), isDebit, integrationDetails);
        }
        if (ledgerAccountViewModel.isdimension4 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension4), isDebit, integrationDetails);
        }
        if (ledgerAccountViewModel.isdimension5 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension5), isDebit, integrationDetails);
        }
        if (ledgerAccountViewModel.isdimension6 == true)
        {
            await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension6), isDebit, integrationDetails);
        }
    }
    private async Task InsertIntegrationDetailList(LedgerAccountViewModel ledgerAccountViewModel, ItemTransactionFinanceLineCost itemTransactionFinanceDTO, bool isDebit, int branchId)
    {
        try
        {
            IntegrationDetails integrationDetails = new IntegrationDetails();
            integrationDetails.ledgeraccountid = ledgerAccountViewModel.chartofaccountid;
            integrationDetails.ledgeraccountcode = ledgerAccountViewModel.chartofaccountcode;
            integrationDetails.ledgeraccountname = ledgerAccountViewModel.chartofaccountname;
            if (isDebit)
            {
                integrationDetails.debitamount = itemTransactionFinanceDTO.TransAmount;
                integrationDetails.debitamountinbasecurrency = itemTransactionFinanceDTO.HmAmount;
            }
            else
            {
                integrationDetails.creditamount = itemTransactionFinanceDTO.TransAmount;
                integrationDetails.creditamountinbasecurrency = itemTransactionFinanceDTO.HmAmount;
            }
            integrationDetails.linenumber = ++lineNumber;
            integrationDetails.integrationheaderid = IntegrationHeaderId;
            integrationDetails.ItemTransactionFinanceId = itemTransactionFinanceDTO.ItemTransactionFinanceId;
            integrationDetails.ItemTransactionFinanceLineCostId = itemTransactionFinanceDTO.Id;


            integrationDetails.FinancialYearId = financialYearId;
            integrationDetails.DocumentNumber = documentNumber;
            integrationDetails.BranchId = branchId;
            //   integrationDetailList.Add(integrationDetails);
            int integrationdetailid = await integrationDetailsRepository.InsertAsync(integrationDetails);
            integrationDetails.Id = integrationdetailid;
            if (ledgerAccountViewModel.isdimension1 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension1), isDebit, integrationDetails);
            }
            if (ledgerAccountViewModel.isdimension2 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension2), isDebit, integrationDetails);
            }
            if (ledgerAccountViewModel.isdimension3 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension3), isDebit, integrationDetails);
            }
            if (ledgerAccountViewModel.isdimension4 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension4), isDebit, integrationDetails);
            }
            if (ledgerAccountViewModel.isdimension5 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension5), isDebit, integrationDetails);
            }
            if (ledgerAccountViewModel.isdimension6 == true)
            {
                await IsDimensionCheck(EnumExtensions.GetDisplayName(DimensionType.Dimension5), isDebit, integrationDetails);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task IsDimensionCheck(string Dimension, bool isDebit, IntegrationDetails integrationDetails)
    {
        try
        {
            Dimension dimension = await dimensionRepository.GetByDimensionTypeName(Dimension);

            if (dimension.DimensionTypeLabel == TradingDimensionTypeType.Project.ToString() && itemDto.ProjectId != null)
            {
                await InsertDimension(dimension.DimensionTypeLabel, itemDto.ProjectId, isDebit, integrationDetails);
            }
            else if (dimension.DimensionTypeLabel == TradingDimensionTypeType.Employee.ToString() && itemDto.EmployeeId != null)
            {
                await InsertDimension(dimension.DimensionTypeLabel, itemDto.EmployeeId, isDebit, integrationDetails);
            }
            else if (dimension.DimensionTypeLabel == TradingDimensionTypeType.costcenter.ToString() && itemDto.CostCenterId != null)
            {
                await InsertDimension(dimension.DimensionTypeLabel, itemDto.CostCenterId, isDebit, integrationDetails);
            }
            else
            {
                //throw new ResourceNotFoundException($"{dimension.DimensionTypeLabel} is Mandatory");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private async Task InsertDimension(string DimensionTypeLabel, string Code, bool isDebit, IntegrationDetails integrationDetails)
    {
        try
        {
            IntegrationDetailDimension integrationDetailDimension = new IntegrationDetailDimension();
            DimensionMaster dimensionMaster = await dimensionMasterRepository.GetDimensionMasterDetails(DimensionTypeLabel, Code);
            integrationDetailDimension.Integrationdetailid = integrationDetails.Id;
            integrationDetailDimension.HeaderId = integrationDetails.integrationheaderid;
            integrationDetailDimension.BranchName = itemDto.BranchName;
            integrationDetailDimension.DimensionTypeId = dimensionMaster.DimensionTypeId;
            integrationDetailDimension.DimensionTypeName = dimensionMaster.DimensionTypeName;
            integrationDetailDimension.DimensionDetailId = dimensionMaster.Id;
            integrationDetailDimension.DimensionCode = dimensionMaster.Code;
            integrationDetailDimension.DimensionDetailName = dimensionMaster.Name;
            if (isDebit)
            {
                integrationDetailDimension.DebitAmount = integrationDetails.debitamount;
                integrationDetailDimension.DebitAmountInBaseCurrency = integrationDetails.debitamountinbasecurrency;
            }
            else
            {
                integrationDetailDimension.CreditAmount = integrationDetails.creditamount;
                integrationDetailDimension.CreditAmountInBaseCurrency = integrationDetails.creditamountinbasecurrency;
            }
            integrationDetailDimension.BranchId = integrationDetails.BranchId;
            integrationDetailDimension.FinancialYearId = integrationDetails.FinancialYearId;
            integrationDetailDimension.TransactionDate = integrationDetails.TransactionDate;
            await integrationDetailDimensionRepository.InsertAsync(integrationDetailDimension);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TradingIntegrationHeader>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TradingIntegrationHeader> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TradingIntegrationHeader> InsertAsync(TradingIntegrationHeader obj)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(TradingIntegrationHeader obj)
    {
        throw new NotImplementedException();
    }

    Task<int> IAsyncService<TradingIntegrationHeader>.InsertAsync(TradingIntegrationHeader obj)
    {
        throw new NotImplementedException();
    }
}
