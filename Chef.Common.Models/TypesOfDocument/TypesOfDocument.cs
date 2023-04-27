using Chef.Common.Core;

namespace Chef.Common.Models;

public class TypesOfDocument : Model
{
    public string DocumentType { get; set; }
    public int WarningDays { get; set; }
}
