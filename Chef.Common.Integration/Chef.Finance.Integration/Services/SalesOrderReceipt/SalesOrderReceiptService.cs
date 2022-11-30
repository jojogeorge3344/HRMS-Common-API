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

namespace Chef.Finance.Integration;


public class SalesOrderReceiptService : AsyncService<SalesOrderReceiptDto>, ISalesOrderReceiptService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly IReceiptRegisterService receiptRegisterService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;
    private readonly IPaymentMethodRepository paymentMethodRepository;
    private readonly IBankAccountRepository bankAccountRepository;
    private readonly IMasterDataRepository masterDataRepository;


    public SalesOrderReceiptService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        IReceiptRegisterService receiptRegisterService,
        ICompanyFinancialYearRepository companyFinancialYearRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IBankAccountRepository bankAccountRepository,
        IMasterDataRepository masterDataRepository

        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.receiptRegisterService = receiptRegisterService;
        this.companyFinancialYearRepository = companyFinancialYearRepository; 
        this.paymentMethodRepository = paymentMethodRepository;
        this.bankAccountRepository = bankAccountRepository;
        this.masterDataRepository = masterDataRepository;
    }

    public async Task<SalesOrderReceiptResponse> PostAsync(SalesOrderReceiptDto salesOrderReceiptDto)
    {

        IntegrationJournalBookConfiguration journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(TransactionOrgin.SalesOrder.ToString(), TransactionType.Receipt.ToString());

        if (journalBookConfig == null)
            throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and  type");
        
        PaymentMethod paymentMethod = await paymentMethodRepository.getPaymentMethodeDetails(PaymentMethodType.Cash, TransactionType.Receipt.ToString());        
        if (paymentMethod == null)
        {
            throw new ResourceNotFoundException("Payment methode details not available");
        }

        BankAccountReceipt bankaccountType = await bankAccountRepository.GetCashAccountDetails(6, "4558989");
        if (paymentMethod == null)
        {
            throw new ResourceNotFoundException("Bank account details not available");
        }

        var exchangeRate = await masterDataRepository.GetExchangeRates(salesOrderReceiptDto.baseCurrencyCode, salesOrderReceiptDto.TransactionCurrencyCode,
            salesOrderReceiptDto.transactionCurrencyDate);
        if (exchangeRate == null)
        {
            throw new ResourceNotFoundException("Exchange rate details not available");
        }

        ReceiptRegister receiptRegister = Mapper.Map<ReceiptRegister>(salesOrderReceiptDto);

        receiptRegister.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
        receiptRegister.ApproveStatus = ApproveStatus.Draft;
        receiptRegister.JournalBookCode = journalBookConfig.JournalBookCode;
        receiptRegister.JournalBookId = journalBookConfig.JournalBookId;
        receiptRegister.JournalBookName = journalBookConfig.JournalBookName;
        receiptRegister.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
        receiptRegister.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;
        receiptRegister.PaymentMethodCode = paymentMethod.Code;
        receiptRegister.PaymentMethodName = paymentMethod.Name;
        receiptRegister.BankAccountId=bankaccountType.Id;
        receiptRegister.BankId=bankaccountType.BankId;
        receiptRegister.BankAccountNumber = bankaccountType.AccountNumber;
        receiptRegister.BankAccountName = bankaccountType.AccountName;
        receiptRegister.BankCurrencyCode = salesOrderReceiptDto.TransactionCurrencyCode;
        receiptRegister.AmountInBankCurrency = salesOrderReceiptDto.ExchangeRate;
        receiptRegister.TransactionDate= salesOrderReceiptDto.ExchangeDate;
        
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

        CustomerCashReceipt customerCashReceipt = Mapper.Map<CustomerCashReceipt>(salesOrderReceiptDto);
        receiptRegister.CustomerCashReceipt = customerCashReceipt;
         int id=   await receiptRegisterService.InsertAsync(receiptRegister);
        receiptRegister.Id = id;
        await receiptRegisterService.UpdateStatus(id, ApproveStatus.Approved);
        receiptRegister.ApproveStatus = ApproveStatus.Approved;
        List<ReceiptRegister> receiptRegisterList=new List<ReceiptRegister> { receiptRegister };
        await receiptRegisterService.ReceiptProcessing(receiptRegisterList);
        return new()
        {
            DocumentNumber = receiptRegister.ReceiptNumber
        };
    }
}