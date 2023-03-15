using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chef.Finance.Integration.Models;

public class SalesReturnCreditItemDto
{
    public int SalesReturnCreditSplitId { get; set; }
    public int SalesReturnCreditItemId { get; set; }
    public int SalesReturnId { get; set; }
    public int SalesReturnCreditId { get; set; }
    public int SalesInvoiceItemId { get; set; }
    public int ItemId { get; set; }
    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public string ItemSpec { get; set; }
    public int ItemCategory { get; set; }
    public int ItemType { get; set; }
    public int ItemSegmentId { get; set; }
    public int ItemFamilyId { get; set; }
    public int ItemClassId { get; set; }
    public int ItemCommodityId { get; set; }
    public int TransUomId { get; set; }
    public int BaseUomId { get; set; }
    public decimal ReturnQuantity { get; set; }
    public decimal ReturnUnitRate { get; set; }
    public decimal TotalItemAmount { get; set; }
    public decimal DiscountPer { get; set; }
    public decimal DiscountAmt { get; set; }
    public decimal GrossAmt { get; set; }
    public decimal TotalTaxPer { get; set; }
    public decimal TotalTaxAmt { get; set; }
    public decimal NetAmount { get; set; }
    //public decimal NetAmount { get; set; }
    public int SalesInvoiceId { get; set; }
    public string SalesInvoiceNo { get; set; }
    public decimal NetAmountInBaseCurrency { get; set; }
    public List<SalesReturnCreditItemTaxDto>? salesReturnCreditItemTaxDtos { get; set; }
}

