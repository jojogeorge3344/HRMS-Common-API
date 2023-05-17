using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Common.Core;

namespace Chef.HRMS.Integration.Models;

public class PayGroup:Model
{
    public string Name { get; set; }
    public string Code { get; set; }
}
