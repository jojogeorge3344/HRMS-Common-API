using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Chef.Common.Core
{
    public class MailRequest : Model
    {
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string ApprovedBy { get; set; }
        public string TemplateFilepath { get; set; }

    }
}