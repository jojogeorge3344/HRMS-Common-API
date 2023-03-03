namespace Chef.Finance.Integration.Models;

public class SalesInvoiceItemDto
{
    public int SalesInvoiceItemId { get; set; }
    public int SalesInvoiceId { get; set; }
    public int ItemId { get; set; }
    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public string? ItemSpec { get; set; }
    public int ItemCategory { get; set; }
    public int ItemType { get; set; }
    public int ItemSegmentId { get; set; }
    public int ItemFamilyId { get; set; }
    public int ItemClassId { get; set; }
    public int ItemCommodityId { get; set; }
    public int TransUomId { get; set; }
    public int BaseUomId { get; set; }
    public decimal InvQuantity { get; set; }
    public decimal InvUnitRate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountPer { get; set; }
    public decimal DiscountAmt { get; set; }
    public decimal GrossAmt { get; set; }
    public decimal TotalTaxPer { get; set; }
    public decimal TotalTaxAmt { get; set; }
    public decimal NetAmount { get; set; }
    public decimal AdditionalMarginPer { get; set; }
    public decimal MarginAmount { get; set; }
    public decimal TransUnitRateWithMargin { get; set; }
    public List<SalesInvoiceItemTaxDto>? SalesInvoiceItemTaxDto { get; set; }
}
