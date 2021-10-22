using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum ApproveStatus
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
        Reversal = 5
    }
}