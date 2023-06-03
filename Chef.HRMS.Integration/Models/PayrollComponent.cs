using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public class PayrollComponent:Model
{   
    public string Name { get; set; }
    public string ShortCode { get; set; }
    public int PayrollComponentType { get; set; }

}
