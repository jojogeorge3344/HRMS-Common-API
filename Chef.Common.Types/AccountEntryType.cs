using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum AccountEntryType
    {
        [Description("Debit")]
        Debit = 1,

        [Description("Credit")]
        Credit
    }
}
