using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum ItemCategory
    {
        [Description("None")]
        None = 0,

        [Description("Inventory")]
        Inventory = 1,

        [Description("NonInventory")]
        NonInventory = 2,

        [Description("Service")]
        Service = 3,

        [Description("Asset")]
        Asset = 4,

        [Description("Tools")]
        Tools = 5,

        [Description("Expenses with Inventory")]
        ExpenseswithInventory = 6

        //[Description("SubContract")]
        //SubContract = 7
    }
}
