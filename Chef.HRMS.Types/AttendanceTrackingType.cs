using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Attendance Tracking Policy
    /// </summary>
    public enum AttendanceTrackingType
    {
        [Description("Web Checkin")]
        WebCheckin = 1,

        [Description("Swipping")]
        Swiping,
    }
}
