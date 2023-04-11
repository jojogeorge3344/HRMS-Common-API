namespace Chef.Finance.Integration.Models;

public class SalesInvoiceResponse : SalesResponseDto
{
   public List<SalesInvoiceViewDto> salesInvoices { get; set; } 
}
