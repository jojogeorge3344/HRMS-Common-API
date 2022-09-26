using Chef.Common.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Document : Model
    {
        [Required]
        public string DocumentName { get; set; }

        public string DocumentType { get; set; }

        public int NoofWorkflowSteps { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public bool IsEnabled { get; set; }

        public int ModuleID { get; set; }

        public int SubmoduleID { get; set; }

        public int NodeDocumentID { get; set; }
    }
}
