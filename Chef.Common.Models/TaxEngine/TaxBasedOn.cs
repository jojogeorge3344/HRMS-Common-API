using System.ComponentModel;

namespace Chef.Common.Models;

public enum TaxBasedOn
{
    [Description("Shipping Address")]
    ShippingAddress = 1,

    [Description("Payment Address")]
    PaymentAddress = 2,

    [Description("Company Address")]
    CompanyAddress = 3
}

