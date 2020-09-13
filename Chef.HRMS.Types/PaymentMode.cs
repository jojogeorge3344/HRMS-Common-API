using System.ComponentModel;

namespace Chef.HRMS.Types
{
    /// <summary>
    /// Holds Payment mode
    /// </summary>
    public enum PaymentMode
    {
        [Description("Bank Transfer")]
        BankTransfer = 1,

        [Description("Cheque Payment")]
        ChequePayment,

        [Description("Cash Payment")]
        CashPayment,
    }
}
