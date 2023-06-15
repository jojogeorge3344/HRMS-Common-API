using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Models;

public class SalesReturnCreditDto
{
    public int SalesReturnCreditId { get; set; }
    public int SalesReturnId { get; set; }
    public string? SalesCreditSeries { get; set; }
    public DateTime SalesCreditDate { get; set; }
    public string? Company { get; set; }
    public int BranchId { get; set; }
    public string? SalesCreditRefNo { get; set; }
    public string? SalesCreditRemark { get; set; }
    public int CustomerId { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string SalesCreditCurrency { get; set; }
    public decimal ExRate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal GrossTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal NetAmount { get; set; }
    public int CostCenterId { get; set; }
    public int PoGroupId { get; set; }
    public string? CreditNoteNumber { get; set; }

    public bool isVanSales { get; set; }
    public decimal totalDiscountInBaseCurrency { get; set; }
    public decimal totalAmountInBaseCurrency { get; set; }

    public decimal taxAmountInBaseCurrency { get; set; }

    public decimal netAmountInBaseCurrency { get; set; }

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
    public List<SalesReturnCreditItemDto>? salesReturnCreditItemDtos { get; set; }

}

