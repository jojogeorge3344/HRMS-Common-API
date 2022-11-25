namespace Chef.Finance.Integration.Models;

public class SalesInvoicePaymentInstallDto
{
    public int SalesInvoicePaymentInstallId { get; set; }
    public int SalesInvoicePaymentTermId { get; set; }
    public int SalesInvoiceId { get; set; }
    public int LineNumber { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAmount { get; set; }
    public decimal AdjustedAmount { get; set; }
    public decimal AmountInBaseCurrency { get; set; }
    public decimal BalanceAmountInBaseCurrency { get; set; }
    public decimal AdjustedAmountInBaseCurrency { get; set; }
    public decimal? CreditPeriodInDays { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal EarlyPaymentDiscountPercentage { get; set; }
    public decimal LatePaymentPenaltyPercentage { get; set; }
    public bool IsRetentionApplicable { get; set; }
}
