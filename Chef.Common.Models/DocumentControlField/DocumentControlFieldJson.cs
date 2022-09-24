using System;
using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class DocumentControlFieldJson
    {
        public string JsonData { get; set; }
        public string ColumnlabelJson { get; set; }
        public int NodeID { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Reportfilename { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<AttachmentDocument> Attachment { get; set; }
    }
}
