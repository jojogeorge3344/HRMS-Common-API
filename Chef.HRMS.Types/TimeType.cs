using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Employee Contract Type
    /// </summary>
    public enum TimeType
    {
        [Description("Full Time")]
        FullTime = 1,

        [Description("Part Time")]
        PartTime,
    }
}
