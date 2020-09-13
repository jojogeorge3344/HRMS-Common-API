using System.ComponentModel;

namespace Chef.Common.Models
{
    public enum AccountEntryType
    {
        [Description("Debit")]
        Debit = 1,

        [Description("Credit")]
        Credit
    }
}
