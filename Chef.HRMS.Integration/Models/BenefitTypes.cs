using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public class BenefitTypes:Model
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}
