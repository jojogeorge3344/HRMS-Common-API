using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Models;

public class ARCancelDto
{
    public string DocumentNo { get; set; }
    public string TransOrigin { get; set; }
    public string TransType { get; set; }
    public bool Status { get; set; }
}
