using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds leave status types
    /// </summary>
    public enum LeaveStatus
    {
        [Description("Applied")]
        Applied = 1,

        [Description("Pending")]
        Pending,

        [Description("Approved")]
        Approved,

        [Description("Cancelled")]
        Cancelled,

        [Description("Rejected")]
        Rejected
    }
}
