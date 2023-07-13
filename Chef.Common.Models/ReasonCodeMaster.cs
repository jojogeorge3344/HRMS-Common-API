using Chef.Common.Core;

namespace Chef.Common.Models;

public class ReasonCodeMaster : Model
{
    public string ReasonCode { get; set; }
    public string Remarks { get; set; }
    public bool isassigned { get; set; }
}
