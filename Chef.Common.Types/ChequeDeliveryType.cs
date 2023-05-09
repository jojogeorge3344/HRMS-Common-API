using System.ComponentModel;

namespace Chef.Common.Types;

public enum ChequeDeliveryType
{
    [Description("Courier")]
    Courier = 1,

    [Description("Person")]
    Person,

    [Description("Delivery")]
    Delivery
}
