using Chef.Common.Core;
using System;

namespace Chef.Common.Models
{
    public class DMSSample : IModel
    {
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string Remarks { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsArchived { get; set; } = true;
    }
}