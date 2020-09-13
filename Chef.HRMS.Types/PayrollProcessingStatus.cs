using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds payroll processing status
    /// </summary>
    public enum PayrollProcessingStatus
    {
        [Description("Drafted")]
        Drafted = 1,

        [Description("Submitted")]
        Submitted = 2,
    }
}
