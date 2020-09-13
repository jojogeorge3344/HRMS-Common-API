using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum DocumentStatusType
    {
        [Description("Open")]
        Open = 1,

        [Description("Closed")]
        Closed = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Hold")]
        Hold = 4,
    }
}
