﻿using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models;

public class BusinessPartnerDocumentAttachment:Model
{
    public int BusinessPartnerDocumentId { get; set; }
    public string FileName { get; set; }
    public byte[] AttachmentByte { get; set; }
}
