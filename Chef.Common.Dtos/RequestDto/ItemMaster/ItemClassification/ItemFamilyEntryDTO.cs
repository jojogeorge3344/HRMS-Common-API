using Chef.Common.Models;

namespace Chef.Common.Dtos
{
    public class ItemFamilyEntryDto : ItemFamily
    {
        public string ItemSegmentCode { get; set; }

        public bool AutoGenerateCode { get; set; } = true;

        public string Host { get; set; }
    }
}
