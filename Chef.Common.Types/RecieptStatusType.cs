﻿using System.ComponentModel;

namespace Chef.Common.Types;

public enum ReceiptStatusType
{
    [Description("Unprocessed")]
    Unprocessed = 1,

    [Description("Processed")]
    Processed = 2,

    [Description("Rejected")]
    Rejected = 3,

    [Description("Hold")]
    Hold = 4,

    [Description("Reversal")]
    Reversal = 5,
}
