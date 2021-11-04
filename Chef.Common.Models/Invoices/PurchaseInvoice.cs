using System;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Finance.Models
{
    public class PurchaseInvoice :TransactionModel
    {
        [Required]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } 

       //public  DateTime TransactionDate { get; set; }

        public string Description { get; set; }

        [Required]
        public int BusinessPartnerId { get; set; }

        [Required]
        public string BusinessPartnerCode { get; set; }

        [Required]
        public string BusinessPartnerName { get; set; }

        public string TransactionCurrencyCode { get; set; }

        public int ExchangeRateId { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime ExchangeDate { get; set; }

        public string InvoiceCategory { get; set; }

        public bool IsAgainstPOContract { get; set; }

        public int POContractNumber { get; set; }

        public DateTime POContractDate { get; set; }

        public bool IsAgainstPO { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PONumber { get; set; }

        public DateTime? PODate { get; set; }

        public string QuotationReference { get; set; }

        public DateTime? QuotationDate { get; set; }

        public string DeliveryNoteNo { get; set; }

        public DateTime? DeliveryNoteDate { get; set; }

        public string GRNNumber { get; set; }

        public DateTime? GRNDate { get; set; }

        public bool IsSupplier { get; set; }

        [Required]
        public decimal TaxPercentage { get; set; }

        [Required]
        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal NetAmount { get; set; }

        public decimal TotalAmountInBaseCurrency { get; set; }

        public decimal TotalBalanceAmount { get; set; }

        public decimal TotalBalanceAmountInBasecurrency { get; set; }

        public decimal TotalAdjustedAmount { get; set; }

        public decimal TotalAdjustedAmountInBasecurrency { get; set; }

        public DocumentStatusType DocumentStatus { get; set; }

        public PurchaseInvoiceStatusType PurchaseInvoiceStatus { get; set; }
        public int LandingCostElementId { get; set; }
        public string  LandingCostElementCode { get; set; }
        public string LandingCostElementName { get; set; }

        public InvoiceStatus ApproveStatus { get; set; }

        public string ApproveStatusName { get; set; }

        [Write(false)]
        [Skip(true)]
        public PurchaseInvoicePaymentTerm PaymentTerm { get; set; }

        [Write(false)]
        [Skip(true)]
        public PurchaseInvoiceOtherDetail OtherDetail { get; set; }

        public string Remark { get; set; }
    }
}
