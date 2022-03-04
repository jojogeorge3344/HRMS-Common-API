using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class AttachmentDocument
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
    }
}
