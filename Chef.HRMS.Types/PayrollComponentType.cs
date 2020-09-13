using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Payroll Component Type
    /// </summary>
    public enum PayrollComponentType
    {


        [Description("Fixed")]
        Fixed = 1,

        [Description("Standard Earnings")]
        StandardEarning,

        [Description("Standard Deductions")]
        StandardDeduction,

        //[Description("Accruals")]
        //Accruals,

        [Description("Allowances")]
        Allowance,

        [Description("Reimbursable")]
        Reimbursable,

        //[Description("Others")]
        //Others,
    }
}