using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public class PayRollComponentViewModel
{
    public int ComponentId { get; set; }
    public string ComponentName { get; set; }
    public string ComponentCode { get; set; }
    public int ComponentTypeId { get; set; }
    public string ComponentTypeName { get; set; }
}
