using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
