
namespace Chef.Finance.Integration.Models;

public class SalesInvoiceItemTaxDto
{
    public int SalesInvoiceItemTaxId { get; set; }
    public int SalesInvoiceId { get; set; }
    public int SalesInvoiceItemId { get; set; }
    public int ItemId { get; set; }
    public int ItemSoLineNo { get; set; }
    public bool ItemTaxFlg { get; set; }
    public int? SalesInvoiceAdditionalCostId { get; set; }
    public decimal GrossAmount { get; set; }
    public int TaxId { get; set; }
    public int SurChargeId { get; set; }
    public int SurChargeType { get; set; }
    public decimal TaxPer { get; set; }
    public decimal TaxAmount { get; set; }
}
