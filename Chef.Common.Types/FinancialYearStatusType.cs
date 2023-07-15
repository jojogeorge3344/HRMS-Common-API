using System.ComponentModel;

namespace Chef.Common.Types;

public enum FinancialYearStatusType
{
    [Description("Open")]
    Open = 1,

    [Description("Closed")]
    Closed = 2,

    [Description("Final Closed")]
    FinalClosed = 3,
}
