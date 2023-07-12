using System.ComponentModel;

namespace Chef.Common.Types;

public enum CardTypes
{
    [Description("Visa")]
    Visa = 1,

    [Description("Master")]
    Master = 2,

    [Description("Amex")]
    Amex = 3,

    [Description("Diners")]
    Diners = 4,
}
