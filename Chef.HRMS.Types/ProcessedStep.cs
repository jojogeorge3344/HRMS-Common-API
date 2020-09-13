using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds payroll processing status
    /// </summary>
    public enum ProcessedStep
    {
        [Description("Leave and Attendance")]
        LeaveAndAttendance = 1,

        [Description("Salary Component")]
        SalaryComponent = 2,

        [Description("Bonus, Loans and Salary Revision")]
        BonusLoansAndSalaryRevision = 3,

        [Description("Adhoc Deductions")]
        AdhocDeductions = 4,

        [Description("Preview")]
        Preview = 5,
    }
}
