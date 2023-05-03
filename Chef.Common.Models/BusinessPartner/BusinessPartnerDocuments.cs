using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models;

public class BusinessPartnerDocuments : Model
{
    public string DocumentName { get; set; }
    public int DocumentTypeId { get; set; }

    public int BusinessPartnerId { get; set; }

    public DateTime ExpireDate { get; set; }

    public DateTime IssueDate { get; set; }

    public bool IsAttachment { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string DocumentType { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public List<BusinessPartnerDocumentAttachment> BusinessPartnerDocumentAttachments { get; set; }

}
