using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Dtos
{
    public class ItemCommodityCodeDto
    {
        public int Id { get; set; }
        public int ItemClassId { get; set; }
        public string ItemClassCode { get; set; }
        public string ItemCommodityCode { get; set; }
        public string ItemCommodityName { get; set; }
    }
}
