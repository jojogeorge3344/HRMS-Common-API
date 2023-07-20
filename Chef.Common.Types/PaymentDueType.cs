using System.ComponentModel;

namespace Chef.Common.Types;

public enum PaymentDueType
{
    [Description("Due from Date of Invoice")]
    DateOfInvoice = 1,

    [Description("Due from End of Month")]
    EndOfMonth = 2,
}
