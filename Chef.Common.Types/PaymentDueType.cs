using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum DueType
    {
        [Description("Due from Date of Invoice")]
        DueFromDateOfInvoice = 1,

        [Description("Due from End of Month")]
        DueFromEndOfMonth = 2,
    }
}
