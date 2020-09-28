using System.ComponentModel;

namespace Chef.Common.Models
{
    /// <summary>
    /// Holds leave status types
    /// </summary>
    public enum PaymentType
    {
        [Description("Along with Payroll")]
        AlongWithPayroll = 1,

        [Description("Individual Payment")]
        IndividualPayment
    }
}
