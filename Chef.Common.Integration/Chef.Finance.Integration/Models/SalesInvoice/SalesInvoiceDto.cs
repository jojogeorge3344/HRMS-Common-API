using Chef.Common.Core;

namespace Chef.Finance.Integration.Models;

public class SalesInvoiceDto : Model
{
    public DateTime SalesInvoiceDate { get; set; }
    public string? SalesInvoiceNo { get; set; }
    public string? SalesOrderNo { get; set; }
    public DateTime? PoDate { get; set; }
    public string? SalesQuotationNo { get; set; }
    public DateTime? QuotationDate { get; set; }
    public string? Company { get; set; }
    public int BranchId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string SalesInvoiceCurrency { get; set; }
    public decimal ExRate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal GrossTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal NetAmount { get; set; }

    public decimal AdditionalMarginPer { get; set; }
    public decimal TotalMarginAmount { get; set; }
    public decimal AdditionalDiscount { get; set; }
    public decimal TotalInvoiceAmount { get; set; }

    public decimal TotalDiscountInBaseCurrency { get; set; }
    public decimal TotalAmountInBaseCurrency { get; set; }
    public decimal TaxAmountInBaseCurrency { get; set; }
    public decimal NetAmountInBaseCurrency { get; set; }

    public string Narration { get; set; }
    public int CostCenterId { get; set; }
    public int PoGroupId { get; set; }

    public int? SalesOrderOrigin { get; set; }

    public TransactionOrgin TransactionOriginName { get; set; }

    public string TransactionTypeName { get; set; }

    public TransactionType TransOriginType { get; set; }

    public DateTime? ExchangeDate { get; set; }

    public bool IsCashSales { get; set; }

    public int InvoiceType { get; set; }
    public bool IsProcess { get; set; }
    public bool IsCustomer { get; set; }
    public int ProspectId { get; set; }

    public int BaseQuantity { get; set; }

    //As per Discussion with Deepa and Sherin added the new fields for Dimension
    //Changes Start
    public int ProjectId { get; set; }
    public string ProjectCode { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public string GroupCode { get; set; } = string.Empty;

    //Changes End

    public List<SalesInvoiceItemDto> SalesInvoiceItemDto { get; set; }
    public List<SalesInvoicePaymentTermsDto> SalesInvoicePaymentTermsDto { get; set; }
}
