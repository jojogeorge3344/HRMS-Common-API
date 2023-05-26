using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models;

public class CompanyDocumentAttachment:Model
{
    public int CompanyDocumentId { get; set; }
    public string FileName { get; set; }
    public byte[] AttachmentByte { get; set; }
}
