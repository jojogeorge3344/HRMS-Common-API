using Chef.Common.Core;

namespace Chef.Trading.Models
{
    public interface IHeaderPricingModel : IModel, ICurrencyModel, IExchangeRateModel
    {
        public int LandingCostCount { get; set; }
        public decimal TotalLandingCost { get; set; }
        public decimal TotalLandingCostInBaseCurrency { get; set; }
        public float TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalLineAmountExcludingtTax { get; set; }
        public decimal TotalLineAmount { get; set; }
        public decimal TotalLineAmountInBaseCurrency { get; set; }

    }
}
