using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class ApproveRejectDocViewModel
    {
        public int DocumentID { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }

    }
}
