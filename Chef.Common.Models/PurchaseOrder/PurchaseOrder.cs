using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Trading.Types;

namespace Chef.Trading.Models
{
    public class PurchaseOrder : Model, IWarehouseModel, ICurrencyModel, IRevisionModel, IBranchModel, IReviewModel, IExchangeRateModel, IHeaderPricingModel
    {
        [Required]
        public string BranchCode { get; set; }

        [Required]
        [ForeignKeyId(typeof(Warehouse))]
        [Field(Order = 2)]
        public int WarehouseId { get; set; }

        [Field(Order = 3)]
        [ForeignKeyCode(typeof(Warehouse))]
        public string WarehouseCode { get; set; }

        [Required]
        [Unique(true), Composite(Index = 2)]
        public string PurchaseOrderNumber { get; set; } = string.Empty;

        [Required]
        public PurchaseOrderType PurchaseOrderType { get; set; } = PurchaseOrderType.None;

        [Required]
        [Field(Order = 2)]
        [Unique(true), Composite(Index = 1)]
        public string Revision { get; set; }

        [Required]
        [Field(Order = 3)]
        [DefaultValue(true)]
        public bool IsCurrentRevision { get; set; } = true;

        [Required]
        public DateTime PurchaseOrderDate { get; set; } = DateTime.UtcNow;

        //[Unique(true)] TODO: We can add this later when we get more requirements for Planned PR
        //public int OriginId { get; set; }
        //[Unique(true)]
        //public string PrOriginReference { get; set; }
        //public string SupplierQuoteReference { get; set; }
        [ForeignKeyId(typeof(Buyer))]
        public int BuyerId { get; set; }

        [ForeignKeyCode(typeof(Buyer))]
        public string BuyerCode { get; set; }

        [Required]
        [ForeignKeyId(typeof(PurchaseOffice))]
        public int PurchaseOfficeId { get; set; }

        [Required]
        [ForeignKeyCode(typeof(PurchaseOffice))]
        public string PurchaseOfficeCode { get; set; }

        //TODO: During Purchase Contract 
        //public int PoContactNumber { get; set; }
        [ForeignKeyId(typeof(Address))]
        public int? ShiptoAddressId { get; set; }

        [ForeignKeyCode(typeof(Address))]
        public string ShiptoAddressCode { get; set; }

        [Required]
        public PurchaseOrderStatus PurchaseOrderStatus { get; set; } = PurchaseOrderStatus.Draft;

        public ExchangeType ExchangeType { get; set; } = ExchangeType.None;

        public PurchaseOrderCategory PurchaseOrderCategory { get; set; } = PurchaseOrderCategory.Local;

        [ForeignKeyId(typeof(PurchaseOrderGroup))]
        public int PoGroupId { get; set; }

        public string PurchaseOrderNotes { get; set; }

        public string TermsAndCondition { get; set; }

        public int PoContractNumber { get; set; }

        public int SubContractNumber { get; set; }

        public int ServiceOrderNumber { get; set; }

        public bool ShippingAdviseRequired { get; set; }

        public bool TransportAdviseRequired { get; set; }

        public bool AcknowledgementRequired { get; set; }

        //Supplier Details
        [Required]
        public string SupplierCode { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public string SupplierAddressLine1 { get; set; }

        public string SupplierAddressLine2 { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierState { get; set; }
        public string SupplierCountry { get; set; }
        public string SupplierZipCode { get; set; }
        public string SupplierCurrencyCode { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierFax { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierContact { get; set; }
        public string BaseCurrencyCode { get; set; }
        public string TxnCurrencyCode { get; set; }
        public float TxnCurrencyExchangeRate { get; set; }
        public string BaseCurrencySymbol { get; set; }
        public string TxnCurrencySymbol { get; set; }
        public DateTime TxnCurrencyExchangeDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Packaging { get; set; }
        public ShipmentMode ShipmentMode { get; set; } = ShipmentMode.None;
        [ForeignKeyId(typeof(DeliveryTerm))]
        public int? DeliveryTermId { get; set; }
        [ForeignKeyCode(typeof(DeliveryTerm))]
        public string DeliveryCode { get; set; }
        public int DeliveryDays { get; set; }
        public string DeliveryDescription { get; set; }

        public bool IsAcknowledged { get; set; }
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.None;
        public string ReviewComment { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime ReviewedDate { get; set; }

        [DefaultValue(false)]
        public bool IsExchangeRateFixed { get; set; } = false;

        //Calculated
        [DefaultValue(0)]
        public decimal TotalLineAmount { get; set; } = new decimal(0);
        //Calculated
        [DefaultValue(0)]
        public decimal TotalLineAmountExcludingtTax { get; set; } = new decimal(0);
        //Calculated
        [DefaultValue(0)]
        public decimal TotalLineAmountInBaseCurrency { get; set; } = new decimal(0);
        [DefaultValue(0)]
        public float TaxPercent { get; set; } = 0F;
        [DefaultValue(0)]
        public decimal TaxAmount { get; set; } = new decimal(0);
        //Calculated
        [DefaultValue(0)]
        public decimal TotalLandingCost { get; set; }
        [DefaultValue(0)]
        //Calculated
        public decimal TotalLandingCostInBaseCurrency { get; set; }
        [DefaultValue(0)]
        public int LandingCostCount { get; set; } = 0;
        [DefaultValue(true)]
        public bool CashPurchase { get; set; }
        [DefaultValue(true)]
        public bool DirectDelivery { get; set; }
        [ForeignKeyId(typeof(TermsAndConditionsMaster))]
        public int? TermsAndConditonId { get; set; }
        public bool ShipmentTransportActiveStatus { get; set; } = true;

        public string Remark { get; set; }
        public int PurchaseContractId { get; set; }

    }
}
