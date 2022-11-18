using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Dtos  
{
    public class ItemClassCodeDto
    {
        public int Id { get; set; }
        public int ItemFamilyId { get; set; }
        public string ItemFamilyCode { get; set; }
        public string ItemClassCode { get; set; }
        public string ItemClassName { get; set; }
    }
}
