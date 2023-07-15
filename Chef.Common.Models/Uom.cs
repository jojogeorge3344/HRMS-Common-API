using Chef.Common.Core;

namespace Chef.Common.Models;

public class Uom : Model
{
    public string UomCode { get; set; }
    public string UomName { get; set; }
    public int UomType { get; set; }
    public bool ActiveStatus { get; set; }
}
