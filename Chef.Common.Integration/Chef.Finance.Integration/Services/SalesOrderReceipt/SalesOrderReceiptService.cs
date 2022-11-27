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
namespace Chef.Finance.Integration;


public class SalesOrderReceiptService : AsyncService<SalesOrderReceiptDto>, ISalesOrderReceiptService
{
    private readonly IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository;
    private readonly IReceiptRegisterService receiptRegisterService;
    private readonly ICompanyFinancialYearRepository companyFinancialYearRepository;


    public SalesOrderReceiptService(
        IIntegrationJournalBookConfigurationRepository integrationJournalBookConfigurationRepository,
        IReceiptRegisterService receiptRegisterService,
        ICompanyFinancialYearRepository companyFinancialYearRepository

        )
    {
        this.integrationJournalBookConfigurationRepository = integrationJournalBookConfigurationRepository;
        this.receiptRegisterService = receiptRegisterService;
        this.companyFinancialYearRepository = companyFinancialYearRepository;

      
      
     
      
    }
    public async Task<string> PostAsync(SalesOrderReceiptDto salesOrderReceiptDto)
    {

        IntegrationJournalBookConfiguration journalBookConfig = await integrationJournalBookConfigurationRepository.getJournalBookdetails(TransactionOrgin.SalesOrder.ToString(), TransactionType.Receipt.ToString());

        if (journalBookConfig == null)
            throw new ResourceNotFoundException("Journalbook not configured for this transaction origin and  type");

        ReceiptRegister receiptRegister = Mapper.Map<ReceiptRegister>(salesOrderReceiptDto);

        receiptRegister.FinancialYearId = (await companyFinancialYearRepository.GetCurrentFinancialYearAsync()).FinancialYearId;
        receiptRegister.ApproveStatus = ApproveStatus.Draft;
        receiptRegister.JournalBookCode = journalBookConfig.JournalBookCode;
        receiptRegister.JournalBookId = journalBookConfig.JournalBookId;
        receiptRegister.JournalBookName = journalBookConfig.JournalBookName;
        receiptRegister.JournalBookTypeId = journalBookConfig.JournalBookTypeId;
        receiptRegister.JournalBookTypeCode = journalBookConfig.JournalBookTypeCode;
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
        return receiptRegister.ReceiptNumber;
    }
}