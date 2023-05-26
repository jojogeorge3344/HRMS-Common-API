using System.ComponentModel;

namespace Chef.Common.Types;

public enum ChequeContactPersonIdentification
{
    [Description("Emirates ID")]
    EmiratesID = 1,
    Passport,

    [Description("AW Bill")]
    AWBill
}
