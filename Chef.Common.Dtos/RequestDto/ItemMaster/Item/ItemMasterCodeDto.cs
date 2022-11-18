using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Dtos
{
    public class ItemMasterCodeDto
    {
        public int Id { get; set; }
        public int ItemCommodityId { get; set; }
        public string ItemCommodityCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
