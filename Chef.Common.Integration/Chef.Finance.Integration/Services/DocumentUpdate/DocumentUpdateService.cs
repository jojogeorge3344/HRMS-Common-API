using Chef.Finance.Customer.Services;
using Chef.Finance.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration;
    public class DocumentUpdateService: AsyncService<ARCancelDto>, IDocumentUpdateService
    {
        private readonly ISalesInvoiceService salesInvoiceService;
        private readonly ICustomerCreditNoteService customerCreditNoteService;
        public DocumentUpdateService(ISalesInvoiceService salesInvoiceService,
            ICustomerCreditNoteService customerCreditNoteService) 
         {
             this.salesInvoiceService = salesInvoiceService;
             this.customerCreditNoteService = customerCreditNoteService;
         }

    public async Task<IntegrationResponseDto> CancelDocumentDetails(ARCancelDto aRCancelDto)
    {
        try
        {
            IntegrationResponseDto integrationResponseDto = new IntegrationResponseDto();
            integrationResponseDto.Response = 0;
            string type = aRCancelDto.TransOrigin + aRCancelDto.TransType;
            if (type == TransactionType.SalesOrderInvoice.ToString() || type == TransactionType.VanSalesOrderInvoice.ToString())
            {
                int result = await salesInvoiceService.InvoiceCancel(aRCancelDto.DocumentNo, aRCancelDto.Status);
                integrationResponseDto.Response = 1;
               
            }
            if(type == TransactionType.SalesOrderReturn.ToString() || type == TransactionType.VanSalesOrderReturn.ToString())
            {
                int result = await customerCreditNoteService.CreditNoteCancel(aRCancelDto.DocumentNo, aRCancelDto.Status);
                integrationResponseDto.Response = 1;
            }
            return integrationResponseDto;
        }
        catch
        {
            throw;
        }
    }
}

