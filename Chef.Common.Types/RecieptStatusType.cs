using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum ProcessedStatus
    {
        [Description("Un Processed")]
        UnProcessed = 1,

        [Description("Processed")]
        Processed = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Hold")]
        Hold = 4,
    }
}