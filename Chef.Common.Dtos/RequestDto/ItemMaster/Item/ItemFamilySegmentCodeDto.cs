using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Dtos
{
    public class ItemFamilySegmentCodeDto
    {
        public int Id { get; set; }
        public int ItemSegmentId { get; set; }
        public string ItemSegmentCode { get; set; }
        public string ItemFamilyCode { get; set; }
        public string ItemFamilyName { get; set; }
    }
}
