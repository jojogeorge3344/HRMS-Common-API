using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Dtos.ResponseDto
{
    public class ItemDetailsDto
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemCommodityId { get; set; }
        public string ItemCommodityCode { get; set; } = string.Empty;
        public string ItemCommodityName { get; set; }
        public int ItemClassId { get; set; }
        public string ItemClassCode { get; set; } = string.Empty;
        public string ItemClassName { get; set; }
        public int ItemFamilyId { get; set; }
        public string ItemFamilyCode { get; set; }
        public string ItemFamilyName { get; set; }
        public int ItemSegmentId { get; set; }
        public string ItemSegmentCode { get; set; }
        public string ItemSegmentName { get; set; }
    
       
    }
}
