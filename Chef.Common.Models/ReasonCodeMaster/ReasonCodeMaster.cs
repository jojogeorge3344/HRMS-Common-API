using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using Chef.Trading.Types;


namespace Chef.Trading.Models
{
    public class ReasonCodeMaster : Model
    {
        public string ReasonCode { get; set; }
        public string Remarks { get; set; }
        public bool isassigned { get; set; }
    }
}
