using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum PaymentType
    {
        [Description("Along with Payroll")]
        AlongWithPayroll = 1,

        [Description("Individual Payment")]
        IndividualPayment
    }
}
