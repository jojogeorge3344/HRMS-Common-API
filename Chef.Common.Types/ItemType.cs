using System.ComponentModel;

namespace Chef.Common.Types;

public enum ItemType
{
    None = 0,
    [Description("Purchased")]
    Purchased = 1,

    [Description("Manufactured")]
    Manufactured = 2,

    [Description("Sub contracted")]
    SubContracted = 3,

    [Description("Others")]
    Others = 4
}
