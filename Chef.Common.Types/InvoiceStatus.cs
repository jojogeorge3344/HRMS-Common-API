using System.ComponentModel;


namespace Chef.Common.Types
{
    public enum InvoiceStatus
    {
        [Description("Draft")]
        Draft = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Sent For Approval")]
        SentForApproval = 3,

        [Description("Rejected")]
        Rejected = 4,

        [Description("Reversal")]
        Reversal = 5,
        [Description("Deleted")]
        Deleted = 6
    }
}
