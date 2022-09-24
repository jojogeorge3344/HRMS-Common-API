using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum PurchaseInvoiceStatusType
    {
        [Description("Open")]
        Open = 1,

        [Description("Match and Approved")]
        MatchAndApproved = 2,

        [Description("On Hold")]
        Hold = 3,

        [Description("Approved")]
        Approved = 4,
    }
}
