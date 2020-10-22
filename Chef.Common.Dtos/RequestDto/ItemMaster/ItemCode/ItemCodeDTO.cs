using Chef.Common.Types;

namespace Chef.Common.Dtos
{
    public class ItemCodeDto
    {
        public ItemCodeType ItemCodeType { get; set; }
        public string CodeFormat { get; set; }
        public int Counter { get; set; }
    }
}
