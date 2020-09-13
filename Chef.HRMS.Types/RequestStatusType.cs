using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds leave status types
    /// </summary>
    public enum RequestStatusType
    {
        [Description("Applied")]
        Applied = 1,

        [Description("Pending")]
        Pending = 2,

        [Description("Approved")]
        Approved = 3,

        [Description("Cancelled")]
        Cancelled = 4,

        [Description("Rejected")]
        Rejected = 5
    }
}
