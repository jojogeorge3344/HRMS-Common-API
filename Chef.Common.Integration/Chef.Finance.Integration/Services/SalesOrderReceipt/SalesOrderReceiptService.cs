using Chef.Common.Core;
using Chef.Common.Data.Repositories;
using Chef.Common.Models;
using Chef.Common.Types;
using Chef.Finance.Banking.Repositories;
using Chef.Finance.BP.Services;
using Chef.Finance.Configuration.Repositories;
using Chef.Finance.Configuration.Services;
using Chef.Finance.Customer.Repositories;
using Chef.Finance.Customer.Services;
using Chef.Finance.GL.Repositories;
using Chef.Finance.GL.Repositoriesr.Repositories;
using Chef.Finance.Integration.Models;
using Chef.Finance.Receipt.Services;
using Chef.Finance.Repositories;
using Chef.Finance.Services;
using Chef.Finance.Types;
using System.Collections.Generic;
using Chef.Finance.Models;
using Chef.Finance.Supplier.Repositories;

namespace Chef.Finance.Integration;


public class SalesOrderReceiptService : AsyncService<SalesOrderReceiptDto>, ISalesOrderReceiptService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly IReceiptRegisterService receiptRegisterService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly IPaymentMethodRepository paymentMethodRepository;
    private readonly IBankAccountRepository bankAccountRepository;
    private readonly IMasterDataRepository masterDataRepository;
    private readonly ISupplierPaymentAdviceRepository supplierPaymentAdviceRepository;
    private readonly IPaymentAdviceProcessingRepository paymentAdviceProcessingRepository;
    private readonly IBusinessPartnerPaymentDetailRepository businessPartnerConfigRepository;
    private readonly ITenantSimpleUnitOfWork tenantSimpleUnitOfWork;

    public SalesOrderReceiptService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        IReceiptRegisterService receiptRegisterService,
        ICompanyFinancialYearRepository companyFinancialYearRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IBankAccountRepository bankAccountRepository,
        IMasterDataRepository masterDataRepository,
        ISupplierPaymentAdviceRepository supplierPaymentAdviceRepository,
        IPaymentAdviceProcessingRepository paymentAdviceProcessingRepository,
        IBusinessPartnerPaymentDetailRepository businessPartnerConfigRepository,
         ITenantSimpleUnitOfWork tenantSimpleUnitOfWork

        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.receiptRegisterService = receiptRegisterService;
        this.companyFinancialYearRepository = companyFinancialYearRepository;
        this.paymentMethodRepository = paymentMethodRepository;
        this.bankAccountRepository = bankAccountRepository;
        this.masterDataRepository = masterDataRepository;
        this.supplierPaymentAdviceRepository = supplierPaymentAdviceRepository;
        this.paymentAdviceProcessingRepository = paymentAdviceProcessingRepository;
        this.businessPartnerConfigRepository = businessPartnerConfigRepository;
        this.tenantSimpleUnitOfWork = tenantSimpleUnitOfWork;
    }


    public async Task<SalesOrderReceiptResponse> PostAsync(SalesOrderReceiptDto salesOrderReceiptDto)
    {
        if(salesOrderReceiptDto.TotalAmount > 0)
        {
           return await Receipt(salesOrderReceiptDto);
        }
        else if(salesOrderReceiptDto.TotalAmount < 0)
        {
            salesOrderReceiptDto.TotalAmount = Math.Abs(salesOrderReceiptDto.TotalAmount);
            salesOrderReceiptDto.TotalAmountInBaseCurrency = Math.Abs(salesOrderReceiptDto.TotalAmountInBaseCurrency);
            return await PaymentAdvice(salesOrderReceiptDto);
        }
        else
        {
            throw new ResourceNotFoundException($"Total Amount is{salesOrderReceiptDto.TotalAmount}");
        }
        return null;
    }

    public async Task<SalesOrderReceiptResponse> Receipt(SalesOrderReceiptDto salesOrderReceiptDto)
    {
        try
        {
            NetBillIntegrationConfig bankaccountType = await bankAccountRepository.GetCashAccountDetails();
            if (bankaccountType == null)
                throw new ResourceNotFoundException("Cash Account Not available");

            PaymentMethod paymentMethod = await paymentMethodRepository.getPaymentMethodeDetails(Convert.ToInt32(PaymentMethodType.Cash));
            if (paymentMethod == null)
                throw new ResourceNotFoundException("Payment methode details not available");

            //var exchangeRate = await masterDataRepository.GetExchangeRates(salesOrderReceiptDto.baseCurrencyCode, salesOrderReceiptDto.TransactionCurrencyCode,
            //    salesOrderReceiptDto.transactionCurrencyDate);
            //if (exchangeRate == null)
            //{
            //    throw new ResourceNotFoundException("Exchange rate details not available");
            //}

            ReceiptRegister receiptRegister = Mapper.Map<ReceiptRegister>(salesOrderReceiptDto);

            receiptRegister.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;

            receiptRegister.ApproveStatus = ApproveStatus.Draft;
            receiptRegister.JournalBookCode = bankaccountType.jbcode;
            receiptRegister.JournalBookId = bankaccountType.jbid;
            receiptRegister.JournalBookCode = bankaccountType.jbcode;
            receiptRegister.JournalBookName = bankaccountType.name;
            receiptRegister.JournalBookTypeId = bankaccountType.journalbooktypeid;
            receiptRegister.JournalBookTypeCode = bankaccountType.journalbooktypecode;
            receiptRegister.PaymentMethodId = paymentMethod.Id;
            receiptRegister.PaymentMethodCode = paymentMethod.Code;
            receiptRegister.PaymentMethodName = paymentMethod.Name;
            receiptRegister.PaymentMethodType = paymentMethod.PaymentMethodType;
            receiptRegister.BankAccountId = bankaccountType.AccountId;
            receiptRegister.BankId = bankaccountType.BankId;
            receiptRegister.BankAccountNumber = bankaccountType.AccountNumber;
            receiptRegister.BankAccountName = bankaccountType.AccountName;


            receiptRegister.BankCurrencyCode = bankaccountType.CurrencyCode;
            receiptRegister.AmountInBankCurrency = salesOrderReceiptDto.TotalAmountInBaseCurrency;
            receiptRegister.TransactionDate = salesOrderReceiptDto.ReceiptDate;
            //PaymentMethodType paymentMethodType = new PaymentMethodType();
            //paymentMethodType = (PaymentMethodType)journalBookConfig.TransactionType;

            //TODO: Take paymentmethod detail from db

            //     "paymentMethodId": 9,
            //   "paymentMethodCode": "RCSH", 
            //  "paymentMethodName": "Receipt - CSH",


            //TODO:Take top1 cash account details from db (for temporary)
            //   "bankAccountId": 23,
            //"bankId": 6,
            //"bankAccountNumber": "4558989",
            //"bankAccountName": "rd",

            //  TODO:take exchange rate with bank currency and calculaste below

            // "bankCurrencyCode": "AED",
            //"amountInBankCurrency": 3000,
            //tenantSimpleUnitOfWork.BeginTransaction();
            CustomerCashReceipt customerCashReceipt = Mapper.Map<CustomerCashReceipt>(salesOrderReceiptDto);
            customerCashReceipt.CashAccountNumber = bankaccountType.AccountNumber;
            customerCashReceipt.TransactionReference = "NetBill";
            receiptRegister.CustomerCashReceipt = customerCashReceipt;
            receiptRegister = await receiptRegisterService.InsertAsync(receiptRegister);
            int status = await receiptRegisterService.UpdateStatus(receiptRegister.Id, ApproveStatus.Approved);
            if (status > 1)
                throw new ResourceNotFoundException("Receipt Cannot Approve");

            receiptRegister.ApproveStatus = ApproveStatus.Approved;
            receiptRegister.ProcessedStatus = ReceiptStatusType.Processed;
            List<ReceiptRegister> receiptRegisterList = new List<ReceiptRegister> { receiptRegister };
            await receiptRegisterService.ReceiptProcessing(receiptRegisterList);
           // tenantSimpleUnitOfWork.Commit();
            return new()
            {
                DocumentNumber = receiptRegister.ReceiptNumber
            };
        }
        catch(Exception ex)
        {
            //tenantSimpleUnitOfWork.Rollback();
            throw;
        }
    }
    public async Task<SalesOrderReceiptResponse> PaymentAdvice(SalesOrderReceiptDto salesOrderReceiptDto)
    {
        try
        {
          

            NetBillIntegrationConfig bankaccountType = await bankAccountRepository.GetCashAccountDetails();
            if (bankaccountType == null)
                throw new ResourceNotFoundException("Cash Account Not available");



            PaymentMethod paymentMethod = await paymentMethodRepository.getPaymentMethodeDetails(Convert.ToInt32(PaymentMethodType.Cash));
            if (paymentMethod == null)
                throw new ResourceNotFoundException("Payment methode details not available");

            PaymentAdvice paymentAdvice = Mapper.Map<PaymentAdvice>(salesOrderReceiptDto);

            paymentAdvice.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
            paymentAdvice.ApproveStatus = ApproveStatus.Draft;
            // paymentAdvice.BankAccountId = bankaccountType.AccountId;
            // paymentAdvice.BankAccountName = bankaccountType.AccountName;
            // paymentAdvice.BankId = bankaccountType.BankId;
            paymentAdvice.JournalBookCode = bankaccountType.RpJbCode;
            paymentAdvice.JournalBookId = bankaccountType.RpJbId;
            paymentAdvice.JournalBookTypeCode = bankaccountType.RpJbTypeCode;
            paymentAdvice.JournalBookTypeId = bankaccountType.RpJbTypeId;
            paymentAdvice.PaymentAdviceType = PaymentAdviceType.OnAccountRepaymentToCustomers;
            paymentAdvice.PaymentMethodCode = paymentMethod.Code;
            paymentAdvice.PaymentMethodId = paymentMethod.Id;
            paymentAdvice.PaymentMethodName = paymentMethod.Name;
            paymentAdvice.PaymentMethodType = PaymentMethodType.Cash;
            paymentAdvice.ProcessedStatus = PaymentAdviceProcessStatusType.Unprocessed;

            SupplierPaymentAdviceJournal supplierPaymentAdviceJournal = new SupplierPaymentAdviceJournal();

            supplierPaymentAdviceJournal.AdvanceAmount = salesOrderReceiptDto.TotalAmount;
            supplierPaymentAdviceJournal.AdvanceAmountInBaseCurrency = salesOrderReceiptDto.TotalAmountInBaseCurrency;
            supplierPaymentAdviceJournal.BranchId = salesOrderReceiptDto.BranchId;
            supplierPaymentAdviceJournal.Currency = salesOrderReceiptDto.TransactionCurrencyCode;
            supplierPaymentAdviceJournal.ExchangeDate = DateTime.UtcNow;
            supplierPaymentAdviceJournal.ExchangeRate = salesOrderReceiptDto.ExchangeRate;
            // paymentAdvice.SupplierPaymentAdviceJournal.Currency = salesOrderReceiptDto.TransactionCurrencyCode;
            supplierPaymentAdviceJournal.TransactionDate = salesOrderReceiptDto.ReceiptDate;
            supplierPaymentAdviceJournal.FinancialYearId = paymentAdvice.FinancialYearId;
            supplierPaymentAdviceJournal.Narration = salesOrderReceiptDto.narration;
            paymentAdvice.SupplierPaymentAdviceJournal = supplierPaymentAdviceJournal;

          //  tenantSimpleUnitOfWork.BeginTransaction();

            PaymentAdvice supplierPaymentAdvice = await supplierPaymentAdviceRepository.InsertAsync(paymentAdvice);
            // int payamentAdviceStatus = await supplierPaymentAdviceRepository.SendForApproveAsync(supplierPaymentAdvice);
            int payamentAdviceStatus = await paymentAdviceProcessingRepository.UpdateStatus(supplierPaymentAdvice.Id, ApproveStatus.Approved);
            if (payamentAdviceStatus > 1)
                throw new ResourceNotFoundException("Payment Cannot Processed");

            decimal bankBalance = await paymentAdviceProcessingRepository.GetBankBalanceAsync(bankaccountType.BankControlAccountId);
            if (bankBalance <= 0)
                throw new ResourceNotFoundException($"{bankaccountType.AccountName} - {bankaccountType.AccountNumber} is with insufficient balance");

            IEnumerable<BusinessPartnerPaymentDetail> businessPartners = await businessPartnerConfigRepository.GetPaymentDetailByBusinessPartnerIdAsync(salesOrderReceiptDto.BusinessPartnerId);
            if (businessPartners.Count() < 0)
                throw new ResourceNotFoundException($"Bank Account Does not exist for this {salesOrderReceiptDto.BusinessPartnerName} BusinessPartner");

            BusinessPartnerPaymentDetail bankDetails = businessPartners.Where(x => x.IsSupplier == true).FirstOrDefault();
            if (bankDetails == null)
                throw new ResourceNotFoundException($"Supplier Bank Account Does not exist for this {salesOrderReceiptDto.BusinessPartnerName} BusinessPartner");

            supplierPaymentAdvice.AccountNumber = bankaccountType.AccountNumber;
            supplierPaymentAdvice.AmountInBankCurrency = salesOrderReceiptDto.TotalAmountInBaseCurrency;
            supplierPaymentAdvice.BankAccountBalance = bankBalance;
            supplierPaymentAdvice.BankAccountId = bankaccountType.AccountId;
            supplierPaymentAdvice.BankAccountName = bankaccountType.AccountName;
            supplierPaymentAdvice.BankAccountNumber = bankDetails.AccountNumber;
            supplierPaymentAdvice.BankCurrencyCode = bankaccountType.CurrencyCode;
            //supplierPaymentAdvice.BankCurrencyExchangeRate =
            supplierPaymentAdvice.BankId = bankaccountType.BankId;
            supplierPaymentAdvice.BankLedgerAccountCode = bankaccountType.BankControlAccountCode;
            supplierPaymentAdvice.BankLedgerAccountId = bankaccountType.BankControlAccountId;
            supplierPaymentAdvice.BankLedgerAccountName = bankaccountType.BankControlAccountName;
            supplierPaymentAdvice.BankName = bankaccountType.BankName;
            supplierPaymentAdvice.BranchId = bankaccountType.BranchId;




            PaymentAdviceCashPaymentDetail cashDetails = new PaymentAdviceCashPaymentDetail();

            cashDetails.AccountNumber = bankDetails.AccountNumber;
            cashDetails.Amount = salesOrderReceiptDto.TotalAmount.ToString();
            cashDetails.BranchId = salesOrderReceiptDto.BranchId;
            cashDetails.Currency = salesOrderReceiptDto.TransactionCurrencyCode;
            cashDetails.FinancialYearId = paymentAdvice.FinancialYearId;
            cashDetails.IsPayment = false;
            cashDetails.Name = salesOrderReceiptDto.BusinessPartnerName;
            cashDetails.PaymentAdviceId = supplierPaymentAdvice.Id;
            cashDetails.PaymentType = FinancePaymentType.Payment;
            cashDetails.ReferenceDate = salesOrderReceiptDto.ReceiptDate.ToString();
            cashDetails.ReferenceNumber = "NetBill -" + supplierPaymentAdvice.Id;

            supplierPaymentAdvice.ApproveStatus = ApproveStatus.Approved;
            supplierPaymentAdvice.ProcessedStatus = PaymentAdviceProcessStatusType.Processed;
            supplierPaymentAdvice.PaymentAdviceCashPaymentDetail = cashDetails;




            int payamentDocumentStatus = await paymentAdviceProcessingRepository.SendForRequest(supplierPaymentAdvice);
            if (payamentDocumentStatus > 1)
                throw new ResourceNotFoundException("Payment Cannot Processed");

            supplierPaymentAdvice.DocumentStatus = DocumentStatus.Closed;
            supplierPaymentAdvice.ProcessedStatus = PaymentAdviceProcessStatusType.Completed;

            int payamentProcessingStatus = await paymentAdviceProcessingRepository.ProcessRequest(supplierPaymentAdvice);
            if (payamentProcessingStatus > 1)
                throw new ResourceNotFoundException("Payment Cannot Processed");
            //tenantSimpleUnitOfWork.Commit();
            return new()
            {
                DocumentNumber = supplierPaymentAdvice.PaymentProcessingNumber
            };
        }
        catch(Exception ex)
        {
            //tenantSimpleUnitOfWork.Rollback();
            throw;
        }

    }

}