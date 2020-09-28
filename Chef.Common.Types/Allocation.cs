using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum Allocation
    {
        [Description("Allocate")]
        Allocate = 1,

        [Description("Later")]
        Later = 2,
    }
}
