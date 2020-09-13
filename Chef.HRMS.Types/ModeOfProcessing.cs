using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Mode of Processing
    /// </summary>
    public enum ModeOfProcessing
    {
        [Description("Pay Group")]
        PayGroup = 1,

        [Description("Employee")]
        Employee,
    }
}
