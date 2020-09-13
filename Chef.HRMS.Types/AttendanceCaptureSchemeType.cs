using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Attendance Capture Scheme
    /// </summary>
    public enum AttendanceCaptureSchemeType
    {
        [Description("Web Checkin")]
        WebCheckin = 1,

        [Description("CXO Attendance")]
        CXOAttendance,
    }
}
