using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models;

public class CompanyDocuments:Model
{
    public string DocumentName { get; set; }
    public int DocumentTypeId { get; set; }

    public string DocumentTypeName { get; set; }

    public int CompanyId { get; set; }

    public DateTime? ExpireDate { get; set; }

    public DateTime? IssueDate { get; set; }

    public bool IsAttachment { get; set; }

    public string PhoneNo { get; set; }

    public string EmailId { get; set; }

    public bool Email { get; set; }

    public bool Sms { get; set; }
    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public List<CompanyDocumentAttachment> companyDocumentAttachments { get; set; }

}
