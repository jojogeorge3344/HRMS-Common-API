namespace Chef.Finance.Integration.Models;

public class SalesInvoicePaymentTermsDto
{
    public int SalesInvoicePaymentTermId { get; set; }
    public int SalesInvoiceId { get; set; }
    public int SalesOrderId { get; set; }
    public bool IsRetentionApplicable { get; set; }
    public decimal? RetentionPercentage { get; set; }
    public decimal? RetentionLockPeriod { get; set; }
    public int? NumberOfInstallments { get; set; }
    public int? CreditPeriod { get; set; }
    public bool IsAdvancePaymentApplicable { get; set; } = false;
    public decimal? AdvancePaymentPercentage { get; set; }
    public int CreditTermId { get; set; }
    public string? CreditTermName { get; set; }
    public int Paymenttermsid { get; set; }
    public List<SalesInvoicePaymentTermLineDto>? salesInvoicePaymentTermLineDtos { get; set; }
}
