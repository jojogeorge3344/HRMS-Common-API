using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum AccountType
    {
        [Description("Balance Sheet")]
        BalanceSheet = 1,

        [Description("Profit and Loss")]
        ProfitAndLoss
    }
}
