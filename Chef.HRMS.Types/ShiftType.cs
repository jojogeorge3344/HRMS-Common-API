using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Shift Types
    /// </summary>
    public enum ShiftType
    {
        [Description("General Shift")]
        General = 1,

        [Description("Night Shift")]
        Night
    }
}
